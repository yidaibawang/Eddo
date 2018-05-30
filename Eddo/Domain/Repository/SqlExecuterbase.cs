using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Repository
{
    public abstract class SqlExecuterbase : ISqlExecuter
    {
        /// <summary>
        /// 执行不带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Execute(string sql)
        {
            return Execute(sql, null);
        }

        public abstract int Execute(string sql, params object[] parameters);
       

        public IQueryable<T> SqlQuery<T>(string sql)
        {
            return SqlQuery<T>(sql,null);
        }

        public abstract IQueryable<T> SqlQuery<T>(string sql, params object[] parameters);

        public abstract void BulkInsert<T>(IEnumerable<T> data);
       
        public async Task BulkInsertAsync<T>(IEnumerable<T> data)
        {
            BulkInsert<T>(data);
            await Task.FromResult(0); 
        }

    }
}
