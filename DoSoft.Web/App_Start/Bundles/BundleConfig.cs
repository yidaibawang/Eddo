using DoSoft.Web;
using System.Web;
using System.Web.Optimization;

namespace Dosoft.Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //VENDOR RESOURCES

            //~/Bundles/App/vendor/css
            bundles.Add(
                new StyleBundle("~/bundles/App/vendor/css")
                    .Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform())
                );

            //~/Bundles/App/vendor/js
            bundles.Add(
                new ScriptBundle("~/bundles/App/vendor/js")
                    .Include(
                        "~/libs/jquery/jquery.min.js",
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/libs/json2/json2.min.js",
                        "~/Scripts/modernizr-2.6.2.js",
                        "~/Scripts/bootstrap.min.js",
                         ScriptPaths.JQuery_Ajax_Form,
                        "~/libs/moment/moment-with-locales.min.js",
                        "~/libs/jquery-validation/jquery.validate.min.js",
                        "~/libs/jquery.blockUI/jquery.blockui.min.js",
                        "~/libs/toastr2.1.1/toastr.min.js",

                        "~/libs/sweetalert/sweet-alert.min.js",
                        "~/libs/others/spinjs/spin.js",
                        "~/libs/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js"
                  
                    )
                );
        }
    }
}
