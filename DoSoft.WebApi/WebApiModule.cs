using DoSoft.Application;
using Eddo.Applications.Services;
using Eddo.Modules;
using Eddo.Web.Api;
using Eddo.Web.Api.Configuration;
using Eddo.Web.Api.Controllers.Dynamic.Buiders;
using System.Reflection;
using System.Web.Http;
namespace DoSoft.WebApi
{
    [DependsOn(typeof(EddoWebApiModule),typeof(DoSoftApplicationModule))]
    public class WebApiModule: EddoModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(DoSoftApplicationModule).Assembly, "app").WithConventionalVerbs()
                .Build();

            Configuration.Modules.EddoWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
