using Castle.Core.Logging;
using Eddo.Domain.UnitOfWorks;
using Eddo.Events;
using Eddo.ObjectMapper;
using Eddo.Reflection;
using Eddo.Runtime.Session;
using Eddo.Web.Models;
using Eddo.Web.Mvc.Result;
using System;
using System.Text;
using System.Web.Mvc;

namespace Eddo.Web.Mvc.Controllers
{
    public class EddoController: Controller
    {   

        public IEddoSession EddoSession { get; set; }

        /// <summary>
        ///事件
        /// </summary>
        public IEventBus EventBus { get; set; }
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// 对象映射
        /// </summary>
        public IObjectMapper ObjectMapper { get; set; }

        public IUnitOfWorkManage UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new Exception("在使用之前必须设置UnitOfWorkManager。");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManage _unitOfWorkManager;

        /// <summary>
        /// 获取事务单元
        /// </summary>
        protected IUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        protected EddoController()
        {
            EddoSession = NullEddoSession.Instance;
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (data == null)
            {
                data = new AjaxResponse();
            }
            else if (!ReflectionHelper.IsAssignableToGenericType(data.GetType(), typeof(AjaxResponse<>)))
            {
                data = new AjaxResponse(data);
            }

            return new EddoJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HandleAuditingBeforeAction(filterContext);

            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

          //  HandleAuditingAfterAction(filterContext);
        }

    }
}
