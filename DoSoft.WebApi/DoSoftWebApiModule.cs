using DoSoft.Application;
using Eddo.Applications.Services;
using Eddo.Modules;
using Eddo.WebApi;
using Eddo.WebApi.Dynamic.Buider;
using System.Reflection;

namespace DoSoft.WebApi
{
    [DependsOn(typeof(EddoWebApiModule), typeof(DoSoftApplicationModule))]
    public class DoSoftWebApiModule:EddoModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //动态创建webapiController与action 
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(DoSoftApplicationModule).Assembly, "app")
                .Build();
            var httpConfiguration = IocManager.Resolve<IEddoWebApiModuleConfiguration>().HttpConfiguration;
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //cors.SupportsCredentials = true;
            //httpConfiguration.EnableCors(cors);


        }
    }
}
