using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.RedisCache
{
    public interface IRedisCacheSerializer
    {    
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="objbyte"></param>
        /// <returns></returns>
        object Deserialize(RedisValue objbyte);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string Serialize(object value, Type type);
    }
}
