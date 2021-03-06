﻿using Castle.Core.Logging;
using Eddo.Dependency;
using Eddo.Events;
using Eddo.Logging;
using Eddo.Web.Mvc.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eddo.Web.Mvc.Models;
using Eddo.Web.Models;

namespace Eddo.Web.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class EddoHandleErrorAttribute : HandleErrorAttribute, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public IEventBus EventBus { get; set; }

        public EddoHandleErrorAttribute()
        {
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="context">The exception context.</param>
        public override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            //If exception handled before, do nothing.
            //If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                return;
            }

            //Always log exception
            LogHelper.LogException(Logger, context.Exception);

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (!context.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, context.Exception).GetHttpCode() != 500)
            {
                return;
            }

            //Do not handle exceptions for attributes configured for special exception types and this exceptiod does not fit condition.
            if (!ExceptionType.IsInstanceOfType(context.Exception))
            {
                return;
            }

            //We handled the exception!
   

            // 判断是否ajax请求
    
            context.Result = IsAjaxRequest(context)
                ? GenerateAjaxResult(context)
                : GenerateNonAjaxResult(context);
            context.HttpContext.Response.Clear();
            context.ExceptionHandled = true;
            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            // context.HttpContext.Response.TrySkipIisCustomErrors = true;
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
            //Trigger an event, so we can register it.
            EventBus.Trigger(new EddoHandledExceptionData(context.Exception));
        }

        private static bool IsAjaxRequest(ExceptionContext context)
        {
            return context.HttpContext.Request.IsAjaxRequest();
        }

        private static ActionResult GenerateAjaxResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 200;
            return new EddoJsonResult(
                new MvcAjaxResponse(
                    ErrorInfoBuilder.Instance.BuildForException(context.Exception),
                    context.Exception is Eddo.Authorization.EddoAuthorizationException
                    )
                );
        }

        private ActionResult GenerateNonAjaxResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
           // return new ContentResult { Content = context.Exception.Message };
            return new ViewResult
            {
                ViewName = this.View,
                MasterName =this.Master,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(context.Exception)),
                TempData = context.Controller.TempData
            };
        }
    }
}
