using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Extensions;
namespace Eddo.Json
{    
    /// <summary>
    /// json序列化
    /// </summary>
     public static  class JsonSerializationHelper
    {
        private const char TypeSeperator = '|';

        /// <summary>
        /// Serializes an object with a type information included.
        /// So, it can be deserialized using <see cref="DeserializeWithType"/> method later.
        /// </summary>
        public static string SerializeWithType(object obj)
        {
            return obj.ToJsonString();
        }

        /// <summary>
        /// Serializes an object with a type information included.
        /// So, it can be deserialized using <see cref="DeserializeWithType"/> method later.
        /// </summary>
        public static string SerializeWithType(object obj, Type type)
        {
            var serialized = obj.ToJsonString();

            return string.Format(
                "{0}{1}{2}",
                type.AssemblyQualifiedName,
                TypeSeperator,
                serialized
                );
        }

        /// <summary>
        /// Deserializes an object serialized with <see cref="SerializeWithType(object)"/> methods.
        /// </summary>
        public static T DeserializeWithType<T>(string serializedObj)
        {
            var options = new JsonSerializerSettings();
            options.Converters.Insert(0, new EddoDateTimeConverter());
            return JsonConvert.DeserializeObject<T>(serializedObj, options);
        }

        /// <summary>
        /// Deserializes an object serialized with <see cref="SerializeWithType(object)"/> methods.
        /// </summary>
        public static object DeserializeWithType(string serializedObj)
        {

            var typeSeperatorIndex = serializedObj.IndexOf(TypeSeperator);
            var options = new JsonSerializerSettings();
            options.Converters.Insert(0, new EddoDateTimeConverter());
            if (typeSeperatorIndex > 0)
            {
                var type = Type.GetType(serializedObj.Substring(0, typeSeperatorIndex));
                var serialized = serializedObj.Substring(typeSeperatorIndex + 1);
                return JsonConvert.DeserializeObject(serialized, type, options);
            }
            else
            {
                return JsonConvert.DeserializeObject(serializedObj, options);
            }
        }
    }
}
