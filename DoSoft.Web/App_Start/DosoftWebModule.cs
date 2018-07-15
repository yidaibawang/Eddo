using Castle.MicroKernel.Registration;
using DoSoft.Application;
using DoSoft.Core;
using Eddo.Caching;
using Eddo.Modules;
using Eddo.RedisCache;
using Eddo.Web.Mvc;
using Eddo.Web.Mvc.Themes;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace DoSoft.Web
{
    [DependsOn(typeof(EddoWebMvcModule), typeof(DoSoftCoreModule), typeof(DoSoftApplicationModule))]
    public class DoSoftWebModule:EddoModule
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
            ViewEngines.Engines.Clear();
            //except the themeable razor view engine we use
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            //Areas
            AreaRegistration.RegisterAllAreas();
            //Routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Bundling
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}