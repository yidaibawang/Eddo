using Castle.Core.Logging;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Caching
{
    public abstract class CachBase:ICache
    {   
        /// <summary>
        /// 日志记录
        /// </summary>
        public ILogger Logger { get; set; }

        public string Name { get; }
        /// <summary>
        /// 默认时间间隔
        /// </summary>
        public TimeSpan DefaultSlidingExpireTime { get; set; }

        protected readonly object SyncObj = new object();

        private readonly AsyncLock _asyncLock = new AsyncLock();

        protected CachBase(string name)
        {
            Name = name;
            Logger = NullLogger.Instance;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }

        public virtual object Get(string key, Func<string,object> factory)
        {
            object item = null;

            try
            {
                item = GetOrDefault(key);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), ex);
            }

            if (item == null)
            {
                lock (SyncObj)
                {
                    try
                    {
                        item = GetOrDefault(key);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.ToString(), ex);
                    }
                    if (item == null)
                    {
                        item = factory(key);

                        if (item == null)
                        {
                            return null;
                        }

                        try
                        {
                            Set(key, item);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex.ToString(), ex);
                        }
                    }
                }
            }

            return item;
        }

        public virtual async Task<object> GetAsync(string key, Func<string, Task<object>> factory)
        {
            object item = null;

            try
            {
                item = await GetOrDefaultAsync(key);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), ex);
            }

            if (item == null)
            {
                using (await _asyncLock.LockAsync())
                {
                    try
                    {
                        item = await GetOrDefaultAsync(key);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.ToString(), ex);
                    }
                    if (item == null)
                    {
                        item = await factory(key);

                        if (item == null)
                        {
                            return null;
                        }

                        try
                        {
                            await SetAsync(key, item);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex.ToString(), ex);
                        }
                    }
                }
            }

            return item;
        }

        public abstract object GetOrDefault(string key);
      

        public virtual async Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }

        public T Get<T>(string key,Func<string,object> foctry)
        {
            return (T)Get(key, foctry);
        }

        public abstract IEnumerable<object> GetAll();
      

        public abstract IEnumerable<T> GetAll<T>();
    

        public void Set(string key, object value)
        {
            Set(key, value, DefaultSlidingExpireTime);
        }


    

        public abstract void Set(string key, object value, TimeSpan?slidingExpiration=null);

        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpiration = null)
        {
            Set(key, value, slidingExpiration);
            return Task.FromResult(0);
        }
        public abstract void Remove(string key);
    

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        public abstract void Clear();
     

        public virtual void Dispose()
        {

        }
    }
}
