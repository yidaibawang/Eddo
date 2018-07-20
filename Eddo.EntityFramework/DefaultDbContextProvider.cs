using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.EntityFramework
{
    public sealed class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
       where TDbContext : DbContext
    {
        public TDbContext DbContext { get; private set; }

        public DefaultDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
