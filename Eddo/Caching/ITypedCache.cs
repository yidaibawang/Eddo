using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Caching
{
    public interface ITypedCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 默认间隔时间
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// 获取缓存提供
        /// </summary>
        ICache InternalCache { get; }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存健值</param>
        /// <param name="factory">判断缓存是否存在</param>
        /// <returns>返回缓存信息</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);

        /// <summary>
        /// 异步获取缓存
        /// </summary>

        Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">健值</param>
    
        TValue GetOrDefault(TKey key);

        /// <summary>
        /// 获取异步缓存
        /// </summary>
        /// <param name="key">健值</param>
        Task<TValue> GetOrDefaultAsync(TKey key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// 异步设置
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(TKey key);

        /// <summary>
        /// 异步移除
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(TKey key);

        /// <summary>
        /// 清理缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 异步清理缓存
        /// </summary>
        Task ClearAsync();
    }
}
