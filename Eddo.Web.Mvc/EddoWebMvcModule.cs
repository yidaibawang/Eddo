using Eddo.Web.Mvc.Controllers;
using System.Reflection;
using System.Web.Mvc;
using Eddo.Modules;

namespace Eddo.Web.Mvc
{
    [DependsOn(typeof(EddoWebModule))]
    public class EddoWebMvcModule: EddoModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());


        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
        }
    }
}
