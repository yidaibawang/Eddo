using Eddo.Authorization;
using Eddo.Dependency;
using Eddo.Events;
using Eddo.Web.Models;
using Eddo.Web.Mvc.Helpers;
using Eddo.Web.Mvc.Result;
using System;
using System.Net;
using System.Reflection;
using System.Web.Mvc;

namespace Eddo.Web.Mvc.Authorize
{
    public class EddoMvcAuthorizeFilter: IAuthorizationFilter, ITransientDependency
    {
        private readonly IAuthorizationHelper _authorizationHelper;

        private readonly IEventBus _eventBus;

        public EddoMvcAuthorizeFilter(
            IAuthorizationHelper authorizationHelper,

            IEventBus eventBus)
        {
            _authorizationHelper = authorizationHelper;

            _eventBus = eventBus;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }   
        }

        protected virtual void HandleUnauthorizedRequest(
            AuthorizationContext filterContext,
            MethodInfo methodInfo,
           Exception ex)
        {
            filterContext.HttpContext.Response.StatusCode =
                filterContext.RequestContext.HttpContext.User?.Identity?.IsAuthenticated ?? false
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;

            var isJsonResult = MethodInfoHelper.IsJsonResult(methodInfo);

            if (isJsonResult)
            {
                filterContext.Result = CreateUnAuthorizedJsonResult(ex);
            }
            else
            {
                filterContext.Result = CreateUnAuthorizedNonJsonResult(filterContext, ex);
            }

            if (isJsonResult || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
        }
        protected virtual EddoJsonResult CreateUnAuthorizedJsonResult(Exception ex)
        {
            return new EddoJsonResult(
                new AjaxResponse(new ErrorInfo(ex.Message), true))
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected virtual HttpStatusCodeResult CreateUnAuthorizedNonJsonResult(AuthorizationContext filterContext,Exception ex)
        {
            return new HttpStatusCodeResult(filterContext.HttpContext.Response.StatusCode, ex.Message);
        }
    }
}
