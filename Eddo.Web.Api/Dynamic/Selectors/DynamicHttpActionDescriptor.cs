using Eddo.Collections.Extensions;
using Eddo.Extensions;
using Eddo.Web.Models;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eddo.WebApi.Dynamic.Selectors
{
    public   class DynamicHttpActionDescriptor: ReflectedHttpActionDescriptor
    {   

        private readonly IFilter[] _filters;
        //返回数据类型
        public override Type ReturnType
        {
            get
            {
                return typeof(AjaxResponse);
            }
        }

        public DynamicHttpActionDescriptor(HttpControllerDescriptor controllerDescriptor, MethodInfo methodInfo, IFilter[] filters = null)
            : base(controllerDescriptor, methodInfo)
        {
            _filters = filters;
        }

        public override System.Threading.Tasks.Task<object> ExecuteAsync(HttpControllerContext controllerContext, System.Collections.Generic.IDictionary<string, object> arguments, System.Threading.CancellationToken cancellationToken)
        {
            return base
                .ExecuteAsync(controllerContext, arguments, cancellationToken)
                .ContinueWith(task =>
                {
                    try
                    {
                        if (task.Result == null)
                        {
                            return new AjaxResponse();
                        }

                        if (task.Result is AjaxResponse)
                        {
                            return task.Result;
                        }

                        return new AjaxResponse(task.Result);
                    }
                    catch (AggregateException ex)
                    {
                        ex.InnerException.ReThrow();
                        throw; // The previous line will throw, but we need this to make compiler happy
                    }
                }, cancellationToken);
        }

        /// <summary>
        /// 获取action过滤器
        /// </summary>
        /// <returns> The Collection of filters.</returns>
        public override Collection<IFilter> GetFilters()
        {
            var actionFilters = new Collection<IFilter>();

            if (!_filters.IsNullOrEmpty())
            {
                foreach (var filter in _filters)
                {
                    actionFilters.Add(filter);
                }
            }

            foreach (var baseFilter in base.GetFilters())
            {
                actionFilters.Add(baseFilter);
            }
            return actionFilters;
        }
    }
}
