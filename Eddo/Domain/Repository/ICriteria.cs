using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Repository
{
     public  interface ICriteria<TEntity>
    {
        Expression<Func<TEntity, bool>> getpredicate();
    }
}
