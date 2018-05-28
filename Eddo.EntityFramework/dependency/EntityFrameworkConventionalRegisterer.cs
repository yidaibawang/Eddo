using Eddo.Dependency;
using Castle.MicroKernel.Registration;
using System.Configuration;
using Eddo.Configuration;

namespace Eddo.EntityFramework.dependency
{
    class EntityFrameworkConventionalRegisterer : IConventionalDependencyRegistrar
    {

        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<EddoDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(c => c.DynamicParameters(
                        (kernel, dynamicParams) =>
                        {
                            var connectionString = GetNameOrConnectionStringOrNull(context.IocManager);
                            if (!string.IsNullOrWhiteSpace(connectionString))
                            {
                                dynamicParams["nameOrConnectionString"] = connectionString;
                            }
                        })));
        }

        private static string GetNameOrConnectionStringOrNull(IIocResolve iocResolver)
        {
            if (iocResolver.IsRegistered<IEddoStartupConfiguration>())
            {
                var defaultConnectionString = iocResolver.Resolve<IEddoStartupConfiguration>().DefaultNameOrConnectionString;
                if (!string.IsNullOrWhiteSpace(defaultConnectionString))
                {
                    return defaultConnectionString;
                }
            }

            if (ConfigurationManager.ConnectionStrings.Count == 1)
            {
                return ConfigurationManager.ConnectionStrings[0].Name;
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            return null;
        }
    }
}
