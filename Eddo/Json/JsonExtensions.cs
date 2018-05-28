using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Eddo.Json
{
    public static  class JsonExtensions
    {
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            options.Converters.Insert(0, new EddoDateTimeConverter());

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}
