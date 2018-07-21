using Eddo.Modules;
using Eddo.Web.Api.Controllers;
using Eddo.Web.Api.Controllers.Dynamic;
using Eddo.Web.Api.Controllers.Dynamic.Selectors;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Net.Http.Formatting;
using Castle.MicroKernel.Registration;
using Eddo.Logging;
using Eddo.Web.Api.Configuration;
using Eddo.Web.Api.Controllers.Runtime.Caching;
using Eddo.Web.Api.Controllers.Filters;
namespace Eddo.Web.Api
{
    [DependsOn(typeof(EddoWebModule))]
    public class EddoWebApiModule : EddoModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
            IocManager.Register<IEddoWebApiModuleConfiguration, EddoWebApiModuleConfiguration>();

            Configuration.Settings.Providers.Add<ClearCacheSettingProvider>();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            var httpConfiguration = IocManager.Resolve<IEddoWebApiModuleConfiguration>().HttpConfiguration;

            InitializeAspNetServices(httpConfiguration);
            InitializeFilters(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitializeRoutes(httpConfiguration);
        }

        public override void PostInitialize()
        {
            foreach (var controllerInfo in DynamicApiControllerManager.GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.InterceptorType).LifestyleTransient(),
                    Component.For(controllerInfo.ApiControllerType)
                        .Proxy.AdditionalInterfaces(controllerInfo.ServiceInterfaceType)
                        .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                    );

                  LogHelper.Logger.DebugFormat("Dynamic web api controller is created for type '{0}' with service name '{1}'.", controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }
        }

        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new EddoHttpControllerSelector(httpConfiguration));
            httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new EddoApiControllerActionSelector());
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new EddoApiControllerActivator(IocManager));
        }

        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<EddoExceptionFilterAttribute>());
        }

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Formatters.Clear();
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.Add(formatter);
            httpConfiguration.Formatters.Add(new Controllers.Dynamic.Formatters.PlainTextFormatter());
        }

        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            //Dynamic Web APIs

            httpConfiguration.Routes.MapHttpRoute(
                name: "EddoDynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
                );

            //Other routes

            httpConfiguration.Routes.MapHttpRoute(
                name: "EddoCacheController_Clear",
                routeTemplate: "api/AbpCache/Clear",
                defaults: new { controller = "AbpCache", action = "Clear" }
                );

            httpConfiguration.Routes.MapHttpRoute(
                name: "EddoCacheController_ClearAll",
                routeTemplate: "api/AbpCache/ClearAll",
                defaults: new { controller = "AbpCache", action = "ClearAll" }
                );
        }
    }
}
