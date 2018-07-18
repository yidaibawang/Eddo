using Eddo.Web.Mvc.Controllers;
using System.Reflection;
using System.Web.Mvc;
using Eddo.Modules;
using Eddo.Web.Mvc.Views;

namespace Eddo.Web.Mvc
{
    [DependsOn(typeof(EddoWebModule))]
    public class EddoWebMvcModule: EddoModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            IocManager.Register<IPageHeadBuilder, PageHeadBuilder>(Dependency.DependencyLifeStyle.Singleton);

        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
            GlobalFilters.Filters.Add(IocManager.Resolve<EddoHandleErrorAttribute>());
        }
    }
}
