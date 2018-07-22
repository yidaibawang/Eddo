using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace Eddo.WebApi.Dynamic
{
    /// <summary>
    /// DynamicApiControllerInfo
    /// </summary>
    internal class DynamicApiControllerInfo
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; private set; }

        /// <summary>
        /// 服务接口类型
        /// </summary>
        public Type ServiceInterfaceType { get; private set; }

        /// <summary>
        /// ApiController类型
        /// </summary>
        public Type ApiControllerType { get; private set; }

        /// <summary>
        /// 拦截器类型
        /// </summary>
        public Type InterceptorType { get; private set; }

        /// <summary>
        /// 动态Controller过滤器
        /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// 执行方法集合
        /// </summary>
        public IDictionary<string, DynamicApiActionInfo> Actions { get; private set; }

        /// <summary>
        /// 创建DynamicApiControllerInfo实例化
        /// </summary>
        /// <param name="serviceName">方法名</param>
        /// <param name="serviceInterfaceType">服务引用类型</param>
        /// <param name="apiControllerType">apiControllerType类型</param>
        /// <param name="interceptorType"> 拦截器类型</param>
        /// <param name="filters">过滤器</param>
        public DynamicApiControllerInfo(string serviceName, Type serviceInterfaceType, Type apiControllerType, Type interceptorType, IFilter[] filters = null)
        {
            ServiceName = serviceName;
            ServiceInterfaceType = serviceInterfaceType;
            ApiControllerType = apiControllerType;
            InterceptorType = interceptorType;
            Filters = filters ?? new IFilter[] { }; //Assigning or initialzing the action filters.
            Actions = new Dictionary<string, DynamicApiActionInfo>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
