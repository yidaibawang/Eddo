
using Eddo.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Core.Entity;

namespace Web.Application.caching
{
    public interface IUserCache:IEntityCache<UserCacheItem,long>
    {

    }
}
