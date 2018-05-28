using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Eddo.Dependency;
using Eddo.Configuration;
using Castle.MicroKernel;
using System.Reflection;

namespace Eddo.Events
{
    public class EventBusInstaller : IWindsorInstaller
    {
        private readonly IIocResolve _iocResolver;
        private readonly IEventBusConfiguration _eventBusConfiguration;
        private IEventBus _eventBus;
        public EventBusInstaller(IIocResolve iocResolver)
        {
            _iocResolver = iocResolver;
            _eventBusConfiguration = iocResolver.Resolve<IEventBusConfiguration>();
        }
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_eventBusConfiguration.UseDefaultEventBus)
            {
                container.Register(
                    Component.For<IEventBus>().UsingFactoryMethod(() => EventBus.Default).LifestyleSingleton()
                    );
            }
            else
            {
                container.Register(
                    Component.For<IEventBus>().ImplementedBy<EventBus>().LifestyleSingleton()
                    );
            }

            _eventBus = container.Resolve<IEventBus>();

            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

        }
        private void Kernel_ComponentRegistered(string key, IHandler handler)
        {
       
            if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                return;
            }

            var interfaces = handler.ComponentModel.Implementation.GetTypeInfo().GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    _eventBus.Register(genericArgs[0], handler.ComponentModel.Implementation);
                }
            }
        }

    }
}
