using Castle.Core.Logging;
using Eddo.Dependency;
using Eddo.Events;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
namespace Eddo.Web.Api.Controllers.Filters
{
    public class EddoExceptionFilterAttribute:ExceptionFilterAttribute, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public IEventBus EventBus { get; set; }

        public EddoExceptionFilterAttribute()
        {
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            Logging.LogHelper.LogException(Logger, context.Exception);

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.OK,
                new Models.AjaxResponse(
                    Models.ErrorInfoBuilder.Instance.BuildForException(context.Exception),
                    context.Exception is Eddo.Authorization.EddoAuthorizationException)
                );

            EventBus.Trigger(new EddoHandledExceptionData(context.Exception));
        }
    }
}
