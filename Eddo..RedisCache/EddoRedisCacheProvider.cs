using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Caching;
using System.Collections.Concurrent;
using Eddo.Dependency;

namespace Eddo.RedisCache
{
    public class EddoRedisCacheProvider : CacheProviderBase
    {
        private static readonly ConcurrentDictionary<string, ICache> Caches;
        static EddoRedisCacheProvider()
        {
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        public EddoRedisCacheProvider(IIocManager iocManager) : base(iocManager)
        {
       
        }

        public override ICache GetCach(string name)
        {
            ICache cache;
            if (Caches.TryGetValue(name, out cache))
            {
                return cache;
            }
            cache =IocManager.Resolve<RedisMemoryCache>(new { name });
            Caches[name] = cache;
            return cache;
        }

    }
}
