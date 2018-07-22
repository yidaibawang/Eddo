using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Eddo.WebApi.Dynamic.Selectors
{
    public  class EddoApiControllerActionSelector: ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            object controllerInfoObj;
            if (!controllerContext.ControllerDescriptor.Properties.TryGetValue("__EddoDynamicApiControllerInfo", out controllerInfoObj))
            {
                return base.SelectAction(controllerContext);
            }

            //Get controller information which is selected by AbpHttpControllerSelector.
            var controllerInfo = controllerInfoObj as DynamicApiControllerInfo;
            if (controllerInfo == null)
            {
                throw new Exception("__EddoDynamicApiControllerInfo in ControllerDescriptor.Properties is not a " + typeof(DynamicApiControllerInfo).FullName + " class.");
            }

            //No action name case
            var hasActionName = (bool)controllerContext.ControllerDescriptor.Properties["__EddoDynamicApiHasActionName"];
            if (!hasActionName)
            {
                return GetActionByCurrentHttpVerb(controllerContext, controllerInfo);
            }

            //Get action name from route
            var serviceNameWithAction = (controllerContext.RouteData.Values["serviceNameWithAction"] as string);
            if (serviceNameWithAction == null)
            {
                return base.SelectAction(controllerContext);
            }

            var actionName = DynamicApiServiceNameHelper.GetActionNameInServiceNameWithAction(serviceNameWithAction);

            return GetActionByActionName(
                controllerContext,
                controllerInfo,
                actionName
                );
        }

        private static HttpActionDescriptor GetActionByCurrentHttpVerb(HttpControllerContext controllerContext, DynamicApiControllerInfo controllerInfo)
        {
            //Check if there is only one action with the current http verb
            var actionsByVerb = controllerInfo.Actions.Values
                .Where(action => action.Verb.IsEqualTo(controllerContext.Request.Method))
                .ToArray();

            if (actionsByVerb.Length == 0)
            {
                throw new Exception(
                    "There is no action" +
                    " defined for api controller " + controllerInfo.ServiceName +
                    " with an http verb: " + controllerContext.Request.Method
                    );
            }

            if (actionsByVerb.Length > 1)
            {
                throw new Exception(
                    "There are more than one action" +
                    " defined for api controller " + controllerInfo.ServiceName +
                    " with an http verb: " + controllerContext.Request.Method
                    );
            }

            //Return the single action by the current http verb
            return new DynamicHttpActionDescriptor(controllerContext.ControllerDescriptor, actionsByVerb[0].Method, actionsByVerb[0].Filters);
        }

        private static HttpActionDescriptor GetActionByActionName(HttpControllerContext controllerContext, DynamicApiControllerInfo controllerInfo, string actionName)
        {
            //Get action information by action name
            DynamicApiActionInfo actionInfo;
            if (!controllerInfo.Actions.TryGetValue(actionName, out actionInfo))
            {
                throw new Exception("There is no action " + actionName + " defined for api controller " + controllerInfo.ServiceName);
            }

            if (!actionInfo.Verb.IsEqualTo(controllerContext.Request.Method))
            {
                throw new Exception(
                    "There is an action " + actionName +
                    " defined for api controller " + controllerInfo.ServiceName +
                    " but with a different HTTP Verb. Request verb is " + controllerContext.Request.Method +
                    ". It should be " + actionInfo.Verb);
            }

            return new DynamicHttpActionDescriptor(controllerContext.ControllerDescriptor, actionInfo.Method, actionInfo.Filters);
        }
    }
}
