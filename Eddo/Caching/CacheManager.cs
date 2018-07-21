using System;
using Eddo.Dependency;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
namespace Eddo.Caching
{
    public class CacheManager : ICacheManager, ISingletonDependency
    {   
        
        private ICacheProvider _cacheProvide;
        private  readonly ConcurrentDictionary<string, ICache> Cachers;

        public CacheManager(ICacheProvider cacheProvide)
        {
            _cacheProvide = cacheProvide;
            Cachers = new ConcurrentDictionary<string, ICache>();
        }
        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Cachers.Values.ToImmutableList();
        }
        public ICache GetCacher(string name)
         {
            if (name == null)
            {
                throw new Exception("name 参数不能为空！");
            }
            ICache cache;
            if (Cachers.TryGetValue(name, out cache))
            {
                return cache;
            }
            cache = _cacheProvide.GetCach(name);
            Cachers[name] = cache;

            return cache;
        }
        public ICache GetCacher(Type type)
        {
            if (type == null)
            {
                throw new Exception("type 参数不能为空！");
            }

            return GetCacher(type.FullName);
        }
        public ICache GetCacher<T>()
        {
            return GetCacher(typeof(T));
        } 
    }
}
