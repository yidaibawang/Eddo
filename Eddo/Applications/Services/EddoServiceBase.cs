using Castle.Core.Logging;
using Eddo.Domain.UnitOfWorks;
using Eddo.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Applications.Services
{
    public  abstract class EddoServiceBase
    {
  
         
        /// <summary>
        ///事务管理
        /// </summary>
        public IUnitOfWorkManage UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new Exception("需设置事务管理、事务管理为空！");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManage _unitOfWorkManager;

        /// <summary>
        /// 获取工作单元
        /// </summary>
        protected IUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }
        public ILogger Logger { protected get; set; }
        public IObjectMapper ObjectMapper { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        protected EddoServiceBase()
        {
            Logger = NullLogger.Instance;
            ObjectMapper = NullObjectMapper.Instance;
        }

    }
}
