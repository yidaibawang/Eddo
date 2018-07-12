using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Dosoft.Web.App_Start.Bundles
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