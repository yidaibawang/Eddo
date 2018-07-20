using Eddo.Dependency;
using Eddo.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.RedisCache
{
    public class DefaultRedisCacheSerializer: IRedisCacheSerializer,ITransientDependency
    {
        public virtual object Deserialize(RedisValue objbyte)
        {
            return JsonSerializationHelper.DeserializeWithType(objbyte);
        }


        public virtual string Serialize(object value, Type type)
        {
            return JsonSerializationHelper.SerializeWithType(value, type);
        }
    }
}
