using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Eddo.Reflection;
using Eddo.Modules;
using Eddo.Configuration;
using Eddo.Domain.UnitOfWorks;

namespace Eddo.Dependency.Installer
{
    public class EddoCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
            Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
            Component.For<IEddoStartupConfiguration, EddoStartupConfiguration>().ImplementedBy<EddoStartupConfiguration>().LifestyleSingleton(),      Component.For<IEventBusConfiguration, EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
            Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
            Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
            Component.For<IModuleFinder>().ImplementedBy<DefaultModuleFinder>().LifestyleTransient(),
            Component.For<IEddoModuleManager, EddoModuleManager>().ImplementedBy<EddoModuleManager>().LifestyleSingleton()
            );
        }
    }
}
