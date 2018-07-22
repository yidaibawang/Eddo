using Eddo.Extensions;
using Eddo.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.WebApi.Dynamic.Scripting
{
    internal static class ActionScriptingHelper
    {
        public static string GenerateUrlWithParameters(DynamicApiControllerInfo controllerInfo, DynamicApiActionInfo actionInfo)
        {
            var baseUrl = "api/services/" + controllerInfo.ServiceName + "/" + actionInfo.ActionName;

            var primitiveParameters = actionInfo.Method
                .GetParameters()
                .Where(p => TypeHelper.IsPrimitiveExtendedIncludingNullable(p.ParameterType))
                .ToArray();

            if (!primitiveParameters.Any())
            {
                return baseUrl;
            }

            var urlBuilder = new StringBuilder(baseUrl);
            if (!baseUrl.Contains("?"))
            {
                urlBuilder.Append("?");
            }

            for (var i = 0; i < primitiveParameters.Length; i++)
            {
                var parameterInfo = primitiveParameters[i];

                if (i > 0)
                {
                    urlBuilder.Append("&");
                }

                urlBuilder.Append(parameterInfo.Name.ToCamelCase() + "=' + escape(" + parameterInfo.Name.ToCamelCase() + ") + '");
            }

            return urlBuilder.ToString();
        }

        public static string GenerateJsMethodParameterList(MethodInfo methodInfo, string ajaxParametersName)
        {
            var paramNames = methodInfo.GetParameters().Select(prm => prm.Name.ToCamelCase()).ToList();
            paramNames.Add(ajaxParametersName);
            return string.Join(", ", paramNames);
        }

        public static string GenerateBody(DynamicApiActionInfo actionInfo)
        {
            var parameters = actionInfo.Method
                .GetParameters()
                .Where(p => !TypeHelper.IsPrimitiveExtendedIncludingNullable(p.ParameterType))
                .ToArray();

            if (parameters.Length <= 0)
            {
                return "{}";
            }

            if (parameters.Length > 1)
            {
                throw new EddoException("只有一个复杂类型被允许作为web api控制器操作的参数。 但" + actionInfo.ActionName + "包含多个！");
            }

            return parameters[0].Name.ToCamelCase();
        }
    }
}
