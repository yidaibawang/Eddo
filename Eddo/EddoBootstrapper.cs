using Eddo.Configuration;
using Eddo.Dependency;
using Eddo.Dependency.Installer;
using System;
namespace Eddo
{
    public  class EddoBootstrapper: IDisposable
    {
        /// <summary>
        /// 获取Ioc注入
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// 是否已回收
        /// </summary>
        protected bool IsDisposed;
        /// <summary>
        /// 目录管理器
        /// </summary>
        private IEddoModuleManager _moduleManager;

        /// <summary>
        /// 创建EddoBootstrapper启动实例
        /// </summary>
        public EddoBootstrapper()
            : this(Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// 建EddoBootstrapper启动实例
        /// </summary>
        /// <param name="iocManager"></param>
        public EddoBootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// 安装系统服务
        /// </summary>
        public virtual void Initialize()
        {
            IocManager.IocContainer.Install(new EddoCoreInstaller());

            IocManager.Resolve<EddoStartupConfiguration>().Initialize();

            _moduleManager = IocManager.Resolve<IEddoModuleManager>();
            _moduleManager.InitializeModules();
        }

        /// <summary>
        /// 回收
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (_moduleManager != null)
            {
                _moduleManager.ShutdownModules();
            }
        }
    }
}
