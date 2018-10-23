using System.Web;
using System.Web.Optimization;

namespace Vp.Web
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


            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/scripts/url.js")
                .IncludeDirectory("~/app", "*.js", true));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/flot")
                .Include("~/Scripts/flot/jquery.flot.js")
                .Include("~/Scripts/flot/jquery.flot.time.js")
                .Include("~/Scripts/flot/jquery.flot.resize.js")
                .Include("~/Scripts/flot/jquery.flot.navigate.js")
                .Include("~/Scripts/flot/jquery.flot.tooltip.js")
                .Include("~/Scripts/flot/jquery.flot.pie.js")
                .Include("~/Scripts/angular-flot.js")
                );
        }
    }
}
