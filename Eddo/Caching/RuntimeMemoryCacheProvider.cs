using Eddo.Caching;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Caching
{
    public class RuntimeMemoryCacheProvider : ICacheProvider
    {
        private static readonly ConcurrentDictionary<string, ICache> Caches;

        static RuntimeMemoryCacheProvider()
        {
            Caches = new ConcurrentDictionary<string, ICache>();
        }
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICache GetCach(string name)
        {
            ICache cache;
            if (Caches.TryGetValue(name, out cache))
            {
                return cache;
            }
            cache = new RuntimeMemoryCache(name);
            Caches[name] = cache;
            return cache;
        }
    }
}
