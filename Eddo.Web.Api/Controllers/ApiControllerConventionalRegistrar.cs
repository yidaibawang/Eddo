using Castle.MicroKernel.Registration;

using Eddo.Dependency;
using System.Web.Http;
namespace Eddo.Web.Api.Controllers
{
    public class ApiControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
              Classes.FromAssembly(context.Assembly)
                    .BasedOn<ApiController>()
                    .LifestyleTransient()
                );
        }
    }
}
