using Eddo.Dependency;
using Eddo.Domain.Repository;
using EntityFramework.BulkInsert.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Eddo.EntityFramework.Repositories
{
    public class EfSqlExecuter : SqlExecuterbase, ITransientDependency
    {
        private readonly IDbContextProvider<EddoDbContext> _dbContextProvider;
        public EfSqlExecuter(IDbContextProvider<EddoDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override int Execute(string sql, params object[] parameters)
        {
            return _dbContextProvider.DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public override IQueryable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return _dbContextProvider.DbContext.Database.SqlQuery<T>(sql, parameters).AsQueryable<T>();
        }
        public override void BulkInsert<T>(IEnumerable<T> data)
        {
            _dbContextProvider.DbContext.BulkInsert<T>(data);
        }

    }
}
