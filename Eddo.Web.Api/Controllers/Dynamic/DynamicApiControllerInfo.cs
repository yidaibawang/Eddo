using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Eddo.Web.Api.Controllers.Dynamic
{
    internal  class DynamicApiControllerInfo
    {
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; private set; }

        /// <summary>
        /// 服务引用类型
        /// </summary>
        public Type ServiceInterfaceType { get; private set; }

        /// <summary>
        /// ApiController类型.
        /// </summary>
        public Type ApiControllerType { get; private set; }

        /// <summary>
        /// 拦截器类型.
        /// </summary>
        public Type InterceptorType { get; private set; }

        /// <summary>
        /// controller 动态方法 过滤
        /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// 所有方法.
        /// </summary>
        public IDictionary<string, DynamicApiActionInfo> Actions { get; private set; }

        /// <summary>
        /// 创建DynamicApiControllerInfo
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <param name="serviceInterfaceType">服务引用类型</param>
        /// <param name="apiControllerType"></param>
        /// <param name="interceptorType">拦截器类型</param>
        /// <param name="filters">过滤器</param>
        public DynamicApiControllerInfo(string serviceName, Type serviceInterfaceType, Type apiControllerType, Type interceptorType, IFilter[] filters = null)
        {
            ServiceName = serviceName;
            ServiceInterfaceType = serviceInterfaceType;
            ApiControllerType = apiControllerType;
            InterceptorType = interceptorType;
            Filters = filters ?? new IFilter[] { }; //分配和初始化操作过滤器。

            Actions = new Dictionary<string, DynamicApiActionInfo>(StringComparer.InvariantCultureIgnoreCase);
            
        }
    }
}
