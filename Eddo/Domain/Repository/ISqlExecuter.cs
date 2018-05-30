using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Repository
{
    public interface ISqlExecuter
    {    
        int Execute(string sql);
        int Execute(string sql, params object[] parameters);
        IQueryable<T> SqlQuery<T>(string sql);
        IQueryable<T> SqlQuery<T>(string sql, params object[] parameters);
        void BulkInsert<T>(IEnumerable<T> data);
        Task BulkInsertAsync<T>(IEnumerable<T> data);
    }
}
