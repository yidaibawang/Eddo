using Eddo.Modules;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;
using Eddo;
using Castle.MicroKernel.Registration;
using Eddo.Caching;
using Eddo.RedisCache;
using Eddo.Web.Mvc;
using web.ef;
using Web.Application;

namespace WebDemo.App_Start
{
    [DependsOn(typeof(EddoWebMvcModule), typeof(WebDataModule), typeof(WebApplicationModule))]
    public class WebAppModule:EddoModule
    {
        /// <summary>
        ///安装前处理函数
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.IocContainer.Register(
                  Component.For(typeof(ICacheProvider))
                      .ImplementedBy(typeof(EddoRedisCacheProvider))
                      .LifestyleTransient()
                  );
        }
        /// <summary>
        /// 安装处理函数
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //Areas
            BundleTable.Bundles.IgnoreList.Clear();
            AreaRegistration.RegisterAllAreas();
            //Routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Bundling
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}