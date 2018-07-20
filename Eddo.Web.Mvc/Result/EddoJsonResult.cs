using Eddo.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eddo.Web.Mvc.Result
{
    public class EddoJsonResult: JsonResult
    {
        /// <summary>
        /// 获取或设置一个值，该值指示此序列化的JSON结果是否为驼峰写法。
        /// 默认: true.
        /// </summary>
        public bool CamelCase { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示此JSON结果是否使用 <see cref="Formatting.Indented"/> on serialization.
        /// Default: false.
        /// </summary>
        public bool Indented { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EddoJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
            CamelCase = true;
        }

        /// <summary>
        /// Constructor with JSON data.
        /// </summary>
        /// <param name="data">JSON data</param>
        public EddoJsonResult(object data)
            : this()
        {
            Data = data;
        }

        /// <inheritdoc/>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var ignoreJsonRequestBehaviorDenyGet = false;
            if (context.HttpContext.Items.Contains("IgnoreJsonRequestBehaviorDenyGet"))
            {
                ignoreJsonRequestBehaviorDenyGet = String.Equals(context.HttpContext.Items["IgnoreJsonRequestBehaviorDenyGet"].ToString(), "true", StringComparison.OrdinalIgnoreCase);
            }

            if (!ignoreJsonRequestBehaviorDenyGet && JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("此请求已被阻止，因为在GET请求中使用此信息时，敏感信息可能会泄露给第三方网站。 要允许GET请求，请将JsonRequestBehavior设置为AllowGet。");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                response.Write(Data.ToJsonString(CamelCase, Indented));
            }
        }
    }
}
