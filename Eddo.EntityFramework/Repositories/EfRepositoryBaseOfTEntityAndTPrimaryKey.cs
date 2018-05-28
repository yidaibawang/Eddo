using System.Linq;
using System.Threading.Tasks;
using Eddo.Domain.Entities;
using Eddo.Domain.Repository;
using System.Data.Entity;
using System.Collections.Generic;

namespace Eddo.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext,TEntity, TKey> : EddoRepositoryBase<TEntity, TKey> where TEntity : class, IEntity<TKey>
    where TDbContext:DbContext
    {
        protected virtual DbContext Context { get { return _dbContextProvider.DbContext; } }

        /// <summary>
        /// 获取Dbset
        /// </summary>
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }
        private readonly IDbContextProvider<TDbContext> _dbContextProvider;
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override TEntity Add(TEntity entity)
        {
            TEntity _entity= Table.Add(entity);
            Context.SaveChanges();
            return _entity;
        }

        public override async Task<TEntity> AddAsync(TEntity entity)
        {
            return await Task<TEntity>.FromResult(Add(entity));
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
             
            }

        }
        public override void Remove(TKey id)
        {
            var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TKey>.Default.Equals(ent.Id, id));
            if (entity == null)
            {
                entity = FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }

            Remove(entity);
          
        }

        public override void Remove(TEntity entity)
        {
            AttachIfNot(entity);

            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                Update(entity);
            }
            else
            {
                Table.Remove(entity);
                Context.SaveChanges();
            }
        }
        public override IQueryable<TEntity> FindAll()
        {
            return Table;
        }
        public override IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return Table.AsNoTracking();
            }
        }
    }
}
