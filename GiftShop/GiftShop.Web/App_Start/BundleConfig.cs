using System.Web;
using System.Web.Optimization;

namespace GiftShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/angular-busy.css",
                      "~/Content/font-awesome.css",
                      "~/Scripts/plugins/sweetalert/sweetalert.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                     "~/Scripts/angular.js",
                     "~/Scripts/angular-animate.js",
                     "~/Scripts/angular-aria.js",
                     "~/Scripts/angular-cookies.min.js",
                     "~/Scripts/angular-route.min.js",
                     "~/Scripts/angular-ui-router.min.js",
                     "~/Scripts/angular-sanitize.min.js",
                     "~/Scripts/angular-messages.min.js",
                     "~/Scripts/angular-ui/ui-bootstrap.js",
                     "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                     "~/Scripts/angular-base64.js",
                     "~/Scripts/angular-local-storage.js",
                     "~/Scripts/angular-busy.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                     "~/Scripts/plugins/bootstrap-notify/bootstrap-notify.js",
                     "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/App/modules/*.js",
                    "~/App/app.js",
                    "~/App/services/*.js",
                    "~/App/directives/*.js")
                    .IncludeDirectory("~/App/controllers", "*.js", true));
        }
    }
}
