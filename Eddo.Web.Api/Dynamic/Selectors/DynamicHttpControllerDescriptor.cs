using Eddo.Collections.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Eddo.WebApi.Dynamic.Selectors
{
    public  class DynamicHttpControllerDescriptor: HttpControllerDescriptor
    {
        /// <summary>
        /// 动态controller过滤
        /// </summary>
        private readonly IFilter[] _filters;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicHttpControllerDescriptor"/> class. Add the argument for action filters to the controller.
        /// </summary>
        /// <param name="configuration">The Http Configuration.</param>
        /// <param name="controllerName"> controllerName.</param>
        /// <param name="controllerType">The controller type.</param>
        /// <param name="filters">The Dynamic Controller action filters.</param>
        public DynamicHttpControllerDescriptor(HttpConfiguration configuration, string controllerName, Type controllerType, IFilter[] filters = null)
            : base(configuration, controllerName, controllerType)
        {
            _filters = filters;
        }

        /// <summary>
        /// 重写动态过滤GetFilters
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
