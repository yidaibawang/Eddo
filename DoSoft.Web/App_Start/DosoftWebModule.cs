using Castle.MicroKernel.Registration;
using Dosoft.Web;
using DoSoft.Application;
using DoSoft.Core;
using Eddo.Caching;
using Eddo.Modules;
using Eddo.RedisCache;
using Eddo.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace DoSoft.Web
{
    [DependsOn(typeof(EddoWebMvcModule), typeof(DoSoftCoreModule), typeof(DoSoftApplicationModule))]
    public class DosoftWebModule:EddoModule
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
            CommonBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}