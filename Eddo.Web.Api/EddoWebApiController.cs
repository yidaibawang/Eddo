using Castle.Core.Logging;
using Eddo.Domain.UnitOfWorks;
using System;
using System.Web.Http;

namespace Eddo.WebApi
{
    public abstract class EddoWebApiController: ApiController
    {
        public IUnitOfWorkManage UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new Exception("工作单元管理器为空！");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManage _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }
        public ILogger Logger { get; set; }
        protected EddoWebApiController()
        {
            Logger = NullLogger.Instance;
        }

    }
}
