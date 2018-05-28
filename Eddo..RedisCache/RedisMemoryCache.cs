using System;
using System.Collections.Generic;
using Eddo.Caching;
using StackExchange.Redis;
using Eddo.Domain.Entities;
using System.Reflection;
using System.Linq;

namespace Eddo.RedisCache
{
    public class RedisMemoryCache : CachBase
    {
        private readonly IDatabase _database;
        private readonly IRedisCacheSerializer _serializer;
        private readonly IEddoRedisCacheDatabaseProvider _redisCacheDatabaseProvider;
        public RedisMemoryCache(string name, IEddoRedisCacheDatabaseProvider redisCacheDatabaseProvider, IRedisCacheSerializer serializer) : base(name)
        {
            _redisCacheDatabaseProvider = redisCacheDatabaseProvider;
            _database = _redisCacheDatabaseProvider.GetDatabase();
            _serializer = serializer;

        }
        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override object GetOrDefault(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue ? Deserialize(objbyte) : null;
        }

        public override IEnumerable<object> GetAll()
        {    
            Dictionary<string,string> caches = _database.HashGetAll("*").ToStringDictionary();

            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> kv in caches)
            {
                 list.Add(kv.Value);
            }
            return list;
        }

        public override IEnumerable<T> GetAll<T>()
        {
             return GetAll().Cast<T>();
        }

        public override void Set(string key, object value, TimeSpan? slidingExpiration = default(TimeSpan?))
        {
            if (value == null)
            {
                throw new Exception("缓存值不能为空!");
            }

            //TODO: This is a workaround for serialization problems of entities.
            //TODO: Normally, entities should not be stored in the cache, but currently .Zero packages does it. It will be fixed in the future.
            var type = value.GetType();
            if (EntityHelper.IsEntity(type) && type.GetTypeInfo().Assembly.FullName.Contains("EntityFrameworkDynamicProxies"))
            {
                type = type.GetTypeInfo().BaseType;
            }
            _database.StringSet(
                GetLocalizedKey(key),
                Serialize(value, type),
                 slidingExpiration ?? DefaultSlidingExpireTime
                );
        }
        public override void Remove(string key)
        {
            _database.KeyDelete(GetLocalizedKey(key));
        }
        public override void Clear()
        {
            _database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }

        protected virtual string Serialize(object value, Type type)
        {
            return _serializer.Serialize(value, type);
        }

        protected virtual object Deserialize(RedisValue objbyte)
        {
            return _serializer.Deserialize(objbyte);
        }

        protected virtual string GetLocalizedKey(string key)
        {
            return "n:" + Name + ",c:" + key;
        }
    }
}
