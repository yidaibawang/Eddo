using StackExchange.Redis;
namespace Eddo.RedisCache
{
    public  interface IEddoRedisCacheDatabaseProvider
    {
        IDatabase GetDatabase();
    }
}
