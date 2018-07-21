using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Controllers.Dynamic.Buiders
{
    public static class DynamicApiControllerBuilder
    {
        public static IApiControllerBuilder<T> For<T>(string serviceName)
        {
            return new ApiControllerBuilder<T>(serviceName);
        }

        /// <summary>
        /// Generates multiple dynamic api controllers.
        /// </summary>
        /// <typeparam name="T">Base type (class or interface) for services</typeparam>
        /// <param name="assembly">Assembly contains types</param>
        /// <param name="servicePrefix">Service prefix</param>
        public static IBatchApiControllerBuilder<T> ForAll<T>(Assembly assembly, string servicePrefix)
        {
            return new BatchApiControllerBuilder<T>(assembly, servicePrefix);
        }
    }
}
