using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eddo.Caching
{
    public interface ICache: IDisposable
    {
         string Name { get; }
        TimeSpan DefaultSlidingExpireTime { get; set; }
        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        object Get(string key,Func<string,object> factory);
        /// <summary>
        /// 从缓存中异步获取
        /// </summary>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);
        /// <summary>
        /// 从缓存中获取数据第一条数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        object GetOrDefault(string key);
        /// <summary>
        /// 异步获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<object> GetOrDefaultAsync(string key);
        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        T Get<T>(string key, Func<string, object> factory);

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        IEnumerable<object> GetAll();

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        void Set(string key, object value);

        ///// <summary>
        ///// 添加或替换缓存项并设置绝对过期时间
        ///// </summary>
        ///// <param name="key">缓存键</param>
        ///// <param name="value">缓存数据</param>
        ///// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        //void Set(string key, object value, DateTime? absoluteExpiration=null);
        //Task SetAsync(string key, object value, DateTime? absoluteExpiration = null);
        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        void Set(string key, object value, TimeSpan? slidingExpiration=null);
        Task SetAsync(string key, object value, TimeSpan? slidingExpiration = null);
        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);
        /// <summary>
        /// 异步移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task RemoveAsync(string key);
        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
    }
}
