using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Serialization;
using Eddo.Json;
using Eddo.Modules;
using Eddo.Web;
using Eddo.WebApi.Dynamic;
using Eddo.WebApi.Dynamic.Formatters;
using Eddo.WebApi.Dynamic.Selectors;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.ModelBinding;

namespace Eddo.WebApi
{
    [DependsOn(typeof(EddoWebModule))]
    public class EddoWebApiModule:EddoModule
    {   
        public override void Initialize()
        {  
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            var httpConfiguration = IocManager.Resolve<IEddoWebApiModuleConfiguration>().HttpConfiguration;
   
            InitializeAspNetServices(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitRoute(httpConfiguration);

        }
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
            IocManager.Register<IEddoWebApiModuleConfiguration, EddoWebApiModuleConfiguration>();
          
        }
        public override void PostInitialize()
        {    
            //api服务注入
            foreach (var controllerInfo in DynamicApiControllerManager.GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.InterceptorType).LifestyleTransient(),
                    Component.For(controllerInfo.ApiControllerType)
                        .Proxy.AdditionalInterfaces(controllerInfo.ServiceInterfaceType)
                        .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                    );

               // LogHelper.Logger.DebugFormat("Dynamic web api controller is created for type '{0}' with service name '{1}'.", controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }
        }
        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new EddoHttpControllerSelector(httpConfiguration));
            httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new EddoApiControllerActionSelector());
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new EddoApiControllerActivator(IocManager));
        }
        /// <summary>
        /// 配置json格式
        /// </summary>
        /// <param name="httpConfiguration"></param>
        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            {
                if (!(currentFormatter is JsonMediaTypeFormatter ||
                    currentFormatter is JQueryMvcFormUrlEncodedFormatter))
                {
                    httpConfiguration.Formatters.Remove(currentFormatter);
                }
            }

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(0, new EddoDateTimeConverter());
            httpConfiguration.Formatters.Add(new PlainTextFormatter());
        }
 
        public void InitRoute(HttpConfiguration httpConfiguration)
        {   
            //设置api服务路由
            httpConfiguration.Routes.MapHttpRoute(
                name: "EddoDynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
                );
            
            httpConfiguration.Routes.MapHttpRoute(
                name: "EddoCacheController_Clear",
                routeTemplate: "api/EddoCache/Clear",
                defaults: new { controller = "AbpCache", action = "Clear" }
                );
       
            httpConfiguration.Routes.MapHttpRoute(
                name: "EddoCacheController_ClearAll",
                routeTemplate: "api/EddoCache/ClearAll",
                defaults: new { controller = "EddoCache", action = "ClearAll" }
                );
        }
    }
}
