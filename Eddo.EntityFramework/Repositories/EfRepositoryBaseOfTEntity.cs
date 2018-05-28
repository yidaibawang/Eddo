using Eddo.Domain.Entities;
using Eddo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext,TEntity> : EfRepositoryBase<TDbContext,TEntity, int>, IRepository<TEntity>
      where TEntity : class, IEntity<int>
        where TDbContext:DbContext
    {
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
