using System;
using System.Threading.Tasks;

namespace Eddo.Caching
{
    public class TypedCache<TKey, TValue> :ITypedCache<TKey, TValue>
    {
         public string Name
        {
            get { return InternalCache.Name; }
        }

        public TimeSpan DefaultSlidingExpireTime
        {
            get { return InternalCache.DefaultSlidingExpireTime; }
            set { InternalCache.DefaultSlidingExpireTime = value; }
        }

        public ICache InternalCache { get; private set; }

        /// <summary>
        /// 获取缓存类型
        /// </summary>
        /// <param name="internalCache">The actual internal cache</param>
        public TypedCache(ICache internalCache)
        {
            InternalCache = internalCache;
        }

        public void Dispose()
        {
            InternalCache.Dispose();
        }

        public void Clear()
        {
            InternalCache.Clear();
        }

        public async Task ClearAsync()
        {
            InternalCache.Clear();
        }

        public TValue Get(TKey key, Func<TKey, TValue> factory)
        {
            return InternalCache.Get(key, factory);
        }

        public Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory)
        {
            return InternalCache.GetAsync(key, factory);
        }

        public TValue GetOrDefault(TKey key)
        {
            return InternalCache.GetOrDefault<TKey, TValue>(key);
        }

        public Task<TValue> GetOrDefaultAsync(TKey key)
        {
            return InternalCache.GetOrDefaultAsync<TKey, TValue>(key);
        }

        public void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            InternalCache.Set(key.ToString(), value, slidingExpireTime);
        }

        public Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null)
        {
            return InternalCache.SetAsync(key.ToString(), value, slidingExpireTime);
        }

        public void Remove(TKey key)
        {
            InternalCache.Remove(key.ToString());
        }

        public Task RemoveAsync(TKey key)
        {
            return InternalCache.RemoveAsync(key.ToString());
        }
    }
}
