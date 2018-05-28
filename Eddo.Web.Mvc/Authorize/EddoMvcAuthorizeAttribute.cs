using Eddo.Authorization;
using System;
using System.Web.Mvc;

namespace Eddo.Web.Mvc.Authorize
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class EddoMvcAuthorizeAttribute : AuthorizeAttribute, IEddoAuthorizeAttribute
    {
        public string[] Permissions { get; set; }

        /// <inheritdoc/>
        public bool RequireAllPermissions { get; set; }

        /// <summary>
        /// 创建权限实例
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public EddoMvcAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            if (!httpContext.Request.IsAjaxRequest())
            {
                base.HandleUnauthorizedRequest(filterContext);
                return;
            }

            httpContext.Response.StatusCode = httpContext.User.Identity.IsAuthenticated == false
                                      ? (int)System.Net.HttpStatusCode.Unauthorized
                                      : (int)System.Net.HttpStatusCode.Forbidden;

            httpContext.Response.SuppressFormsAuthenticationRedirect = true;
            httpContext.Response.End();
        }
    }
}
