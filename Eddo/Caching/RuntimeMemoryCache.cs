using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Collections;

namespace Eddo.Caching
{
    public class RuntimeMemoryCache : CachBase
    {
        private MemoryCache _memoryCache;
        public RuntimeMemoryCache(string name) : base(name)
        {
            _memoryCache = new MemoryCache(name);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        public override void Set(string key, object value, TimeSpan? slidingExpiration=null)
        {
            if (value == null)
            {
                throw new Exception("请填写缓存值!");
            }

            var cachePolicy = new CacheItemPolicy();

            if (slidingExpiration != null)
            {
                cachePolicy.SlidingExpiration = slidingExpiration.Value;
            }
            else
            {
                cachePolicy.SlidingExpiration = DefaultSlidingExpireTime;
            }

            _memoryCache.Set(key, value, cachePolicy);
        }

    
      

        public override IEnumerable<object> GetAll()
        {
            string token = string.Concat(Name, ":");
            return _memoryCache.Where(m => m.Key.StartsWith(token)).Select(m => m.Value).Cast<DictionaryEntry>().Select(m => m.Value);
        }

        public override IEnumerable<T> GetAll<T>()
        {
            return GetAll().Cast<T>();
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(Name);
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }

    }
}
