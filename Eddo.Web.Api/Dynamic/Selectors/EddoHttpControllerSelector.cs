using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Eddo.Extensions;
namespace Eddo.WebApi.Dynamic.Selectors
{
    public class EddoHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;
        public EddoHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
            _configuration =configuration;
        }
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //Get request and route data
            if (request == null)
            {
                return base.SelectController(null);
            }

            var routeData = request.GetRouteData();
            if (routeData == null)
            {
                return base.SelectController(request);
            }

            //Get serviceNameWithAction from route
            string serviceNameWithAction;
            if (!routeData.Values.TryGetValue("serviceNameWithAction", out serviceNameWithAction))
            {
                return base.SelectController(request);
            }

            //Normalize serviceNameWithAction
            if (serviceNameWithAction.EndsWith("/"))
            {
                serviceNameWithAction = serviceNameWithAction.Substring(0, serviceNameWithAction.Length - 1);
                routeData.Values["serviceNameWithAction"] = serviceNameWithAction;
            }

            //Get the dynamic controller
            var hasActionName = false;
            var controllerInfo = DynamicApiControllerManager.FindOrNull(serviceNameWithAction);
            if (controllerInfo == null)
            {
                if (!DynamicApiServiceNameHelper.IsValidServiceNameWithAction(serviceNameWithAction))
                {
                    return base.SelectController(request);
                }

                var serviceName = DynamicApiServiceNameHelper.GetServiceNameInServiceNameWithAction(serviceNameWithAction);
                controllerInfo = DynamicApiControllerManager.FindOrNull(serviceName);
                if (controllerInfo == null)
                {
                    return base.SelectController(request);
                }

                hasActionName = true;
            }

            //Create the controller descriptor
            var controllerDescriptor = new DynamicHttpControllerDescriptor(_configuration, controllerInfo.ServiceName, controllerInfo.ApiControllerType, controllerInfo.Filters);
            controllerDescriptor.Properties["__EddoDynamicApiControllerInfo"] = controllerInfo;
            controllerDescriptor.Properties["__EddoDynamicApiHasActionName"] = hasActionName;
            return controllerDescriptor;
        }

    }
}
