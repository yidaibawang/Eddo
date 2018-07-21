using Eddo.Web.Api.Controllers.Dynamic.Interceptors;
using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace Eddo.Web.Api.Controllers.Dynamic.Buiders
{
    internal class ApiControllerBuilder<T> : IApiControllerBuilder<T>
    {
        /// <summary>
        /// controller 名称
        /// </summary>
        private readonly string _serviceName;

        /// <summary>
        /// 控制器的所有操作构建器的列表
        /// </summary>
        private readonly IDictionary<string, ApiControllerActionBuilder<T>> _actionBuilders;

        /// <summary>
        /// Action Filters to apply to the whole Dynamic Controller.
        /// </summary>
        private IFilter[] _filters;

        private bool _conventionalVerbs;

        /// <summary>
        /// 创建ApiControllerInfoBuilder的新实例.
        /// </summary>
        /// <param name="serviceName">Name of the controller</param>
        public ApiControllerBuilder(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new ArgumentException("serviceName null or empty!", "serviceName");
            }

            if (!DynamicApiServiceNameHelper.IsValidServiceName(serviceName))
            {
                throw new ArgumentException("serviceName格式不正确！ 它至少必须包含一个单深度命名空间！ 例如：'myapplication / myservice'。“，”serviceName");
            }

            _serviceName = serviceName;

            _actionBuilders = new Dictionary<string, ApiControllerActionBuilder<T>>();
            foreach (var methodInfo in DynamicApiControllerActionHelper.GetMethodsOfType(typeof(T)))
            {
                _actionBuilders[methodInfo.Name] = new ApiControllerActionBuilder<T>(this, methodInfo);
            }
        }

        /// <summary>
        /// 为整个动态控制器添加Action过滤器
        /// </summary>
        /// <param name="filters"> The filters. </param>
        /// <returns>The current Controller Builder </returns>
        public IApiControllerBuilder<T> WithFilters(params IFilter[] filters)
        {
            _filters = filters;
            return this;
        }

        /// <summary>
        /// 用于指定方法定义.
        /// </summary>
        /// <param name="methodName">Name of the method in proxied type</param>
        /// <returns>Action builder</returns>
        public IApiControllerActionBuilder<T> ForMethod(string methodName)
        {
            if (!_actionBuilders.ContainsKey(methodName))
            {
                throw new EddoException("There is no method with name " + methodName + " in type " + typeof(T).Name);
            }

            return _actionBuilders[methodName];
        }

        public IApiControllerBuilder<T> WithConventionalVerbs()
        {
            _conventionalVerbs = true;
            return this;
        }

        /// <summary>
        /// 绑定控制器.
        /// 必须在构建操作的最后调用此方法.
        /// </summary>
        public void Build()
        {
            var controllerInfo = new DynamicApiControllerInfo(
                _serviceName,
                typeof(T),
                typeof(DynamicApiController<T>),
                typeof(EddoDynamicApiControllerInterceptor<T>),
                _filters
                );

            foreach (var actionBuilder in _actionBuilders.Values)
            {
                if (actionBuilder.DontCreate)
                {
                    continue;
                }

                controllerInfo.Actions[actionBuilder.ActionName] = actionBuilder.BuildActionInfo(_conventionalVerbs);
            }

            DynamicApiControllerManager.Register(controllerInfo);
        }
    }
}
