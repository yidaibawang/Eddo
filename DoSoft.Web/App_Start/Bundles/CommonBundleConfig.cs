using System.Web.Optimization;

namespace DoSoft.Web
{
    public class CommonBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
             new StyleBundle("~/Bundles/Common/css")
              .IncludeDirectory("~/Common/Styles", "*.css", true)
              .ForceOrdered()
          );

            bundles.Add(
                new ScriptBundle("~/Bundles/Common/js")
                    .IncludeDirectory("~/Common/Scripts", "*.js", true)
                    .ForceOrdered()
                );
        }
    }
}