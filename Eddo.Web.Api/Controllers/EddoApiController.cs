using Castle.Core.Logging;
using Eddo.Domain.UnitOfWorks;
using Eddo.Runtime.Session;
using System;
using System.Web.Http;

namespace Eddo.Web.Api.Controllers
{
    /// <summary>
    /// EddoApiController 基类
    /// </summary>
    public abstract class EddoApiController: ApiController
    {

        public IEddoSession EddoSession { get; set; }
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets current session information.
        /// </summary>
        [Obsolete("Use AbpSession property instead. CurrentSetting will be removed in future releases.")]
        protected IEddoSession CurrentSession { get { return EddoSession; } }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManage UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new EddoException("单元管理器为空");
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
        public EddoApiController()
        {
            EddoSession = NullEddoSession.Instance;
            Logger = NullLogger.Instance;
        }
    }
}
