using Eddo.Auditing;
using Eddo.Dependency;
using Eddo.Modules;
using Eddo.Web.Auditing;
using System.Reflection;
using Eddo.Configuration.Startup;
namespace Eddo.Web
{
    [DependsOn(typeof(EddoKernelModule))]
     public class EddoWebModule:EddoModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IClientInfoProvider, WebClientInfoProvider>(DependencyLifeStyle.Transient);
        }

        /// <summary>
        /// 安装处理函数
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
