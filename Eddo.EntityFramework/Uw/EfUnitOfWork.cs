using Eddo.Dependency;
using Eddo.Domain.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Eddo.MultiTenancy;
using System.Runtime.Remoting.Messaging;

namespace Eddo.EntityFramework.Uw
{
    public class EfUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        private readonly IDictionary<Type, DbContext> _activeDbContexts;
        private readonly IEfTransactionStrategy _transactionStrategy;
        private readonly IDbContextTypeMatcher _dbContextTypeMatcher;
        private readonly IDbContextResolver _dbContextResolver;
        protected IIocResolve IocResolver { get; private set; }

        public EfUnitOfWork(IIocResolve iocResolver, IUnitOfWorkDefaultOptions defaultOptions,                IConnectionStringResolver           connectionStringResolver,
            IEfTransactionStrategy transactionStrategy, IDbContextTypeMatcher dbContextTypeMatche,
            IDbContextResolver dbContextResolver):base(connectionStringResolver)
        {
            IocResolver = iocResolver;
            _transactionStrategy = transactionStrategy;
            _dbContextTypeMatcher = dbContextTypeMatche;
            _dbContextResolver = dbContextResolver;
            _activeDbContexts = new Dictionary<Type, DbContext>();
        }
        public override void SaveChanges()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                SaveChangesInDbContext(dbContext);
            }
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected override void BeginUow()
        {
            _transactionStrategy.InitOptions(Options);
        }
        protected override void CompleteUow()
        {
            SaveChanges();

            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();

            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Commit();
            }
        }

        protected override void DisposeUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Dispose(IocResolver);
            }
            else
            {
                foreach (var activeDbContext in GetAllActiveDbContexts())
                {
                    Release(activeDbContext);
                }
            }

            _activeDbContexts.Clear();
        }
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }
        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return _activeDbContexts.Values.ToImmutableList();
        }
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
 
        internal TDbContext GetOrCreateDbContext<TDbContext>()
    where TDbContext : DbContext
        {
            var concreteDbContextType = _dbContextTypeMatcher.GetConcreteType(typeof(TDbContext));

            var connectionStringResolveArgs = new ConnectionStringResolveArgs(MultiTenancySides.Host);
            connectionStringResolveArgs["DbContextType"] = typeof(TDbContext);
            connectionStringResolveArgs["DbContextConcreteType"] = concreteDbContextType;
            var connectionString = ResolveConnectionString(connectionStringResolveArgs);

            var dbContextKey = concreteDbContextType.FullName + "#" + connectionString;

            DbContext dbContext;
            
            if (!_activeDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {    


                if (Options.IsTransactional == true)
                {
                    dbContext = _transactionStrategy.CreateDbContext<TDbContext>(connectionString, _dbContextResolver);
                }
                else
                {
                    dbContext=CallContext.GetData("DbContext") as DbContext;
                    if (dbContext == null)
                    {
                        dbContext = Resolve<TDbContext>();
                        CallContext.SetData("DbContext", dbContext);
                    }
                }

                _activeDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }
        protected virtual string ResolveConnectionString(ConnectionStringResolveArgs args)
        {
            return ConnectionStringResolver.GetNameOrConnectionString(args);
        }
        protected virtual TDbContext Resolve<TDbContext>()
        {
            return IocResolver.Resolve<TDbContext>();
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            IocResolver.Release(dbContext);
        }

        
    }
}
