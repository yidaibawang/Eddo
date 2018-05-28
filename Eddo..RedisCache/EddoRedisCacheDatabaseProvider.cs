using System;
using Eddo.Dependency;
using StackExchange.Redis;

namespace Eddo.RedisCache
{
    public class EddoRedisCacheDatabaseProvider : IEddoRedisCacheDatabaseProvider,ISingletonDependency
    {
        private EddoRedisCacheOptions _options;
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        public EddoRedisCacheDatabaseProvider(EddoRedisCacheOptions option)
        {
            _options = option;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }
        /// <summary>
        /// 获取Database
        /// </summary>
        /// <returns></returns>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_options.ConnectionString);
        }
    }
}
