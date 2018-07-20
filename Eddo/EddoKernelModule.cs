using Eddo.Configuration;
using Eddo.Dependency;
using Eddo.Events;
using Eddo.Modules;
using System.Reflection;

namespace Eddo
{
    public class EddoKernelModule:EddoModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }
        public override void Initialize()
        {
            foreach (var replaceAction in ((EddoStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }

            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }
    }
}
