using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.WebApi.Dynamic.Buider
{
     public static class DynamicApiControllerBuilder
    {
        /// <summary>
        /// 单个创建web服务
        /// </summary>
        /// <param name="serviceName">Name of the Api controller service. For example: 'myapplication/myservice'.</param>
        /// <typeparam name="T">Type of the proxied object</typeparam>
        public static IApiControllerBuilder<T> For<T>(string serviceName)
        {
            return new ApiControllerBuilder<T>(serviceName);
        }

        /// <summary>
        /// 批量动态绑定生成
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
