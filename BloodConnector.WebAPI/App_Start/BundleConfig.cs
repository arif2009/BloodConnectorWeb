using System.Web.Optimization;

namespace BloodConnector.WebAPI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                      "~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularComponents").Include(
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-local-storage.min.js",
                        "~/Scripts/loading-bar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularScripts").Include(
                // main app script
                "~/app/app.js",
                // services
                "~/app/services/authInterceptorService.js",
                "~/app/services/authService.js",
                "~/app/services/ordersService.js",
                "~/app/services/dataService.js",
                // controllers
                "~/app/controllers/indexController.js",
                "~/app/controllers/homeController.js",
                "~/app/controllers/loginController.js",
                "~/app/controllers/signupController.js",
                "~/app/controllers/ordersController.js",
                // custom filters
                // custom directives
                "~/app/directives/commonDirectives.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/loading-bar.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/site.css"));
        }
    }
}
