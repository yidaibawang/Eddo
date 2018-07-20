using Eddo.Dependency;
using Web.Core;
using Web.Core.Entity;
using Eddo.Caching;
using Eddo.Domain.Repository;
using Eddo.Domain.Entities.Caching;

namespace Web.Application.caching
{
    public class UserCache : EntityCache<User, UserCacheItem,long>, IUserCache, ITransientDependency
    {
        public UserCache(ICacheManager cacheManager, IRepository<User, long> repository, string cacheName = null) : base(cacheManager, repository, cacheName)
        {

        }
    }
}
