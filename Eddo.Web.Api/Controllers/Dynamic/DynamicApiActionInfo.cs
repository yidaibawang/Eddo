using System.Reflection;
using System.Web.Http.Filters;

namespace Eddo.Web.Api.Controllers.Dynamic
{
    internal class DynamicApiActionInfo
    {
        /// <summary>
        /// 操作名
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// Http 请求类型
        /// </summary>
        public HttpVerb Verb { get; private set; }

        /// <summary>
        /// 动态action 方法 过滤
        /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// 创建DynamicApiActionInfo
        /// </summary>
        /// <param name="actionName">操作名</param>
        /// <param name="verb"></param>
        /// <param name="method"></param>
        public DynamicApiActionInfo(string actionName, HttpVerb verb, MethodInfo method, IFilter[] filters = null)
        {
            ActionName = actionName;
            Verb = verb;
            Method = method;
            Filters = filters ?? new IFilter[] { }; //分配或初始化操作过滤
        }
    }
}
