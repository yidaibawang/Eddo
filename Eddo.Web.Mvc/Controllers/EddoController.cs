using Castle.Core.Logging;
using Eddo.Domain.UnitOfWorks;
using Eddo.Events;
using Eddo.ObjectMapper;
using Eddo.Runtime.Session;
using System;
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
    }
}
