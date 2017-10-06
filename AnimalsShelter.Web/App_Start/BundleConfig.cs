using System.Web;
using System.Web.Optimization;

namespace AnimalsShelter.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerygrid").Include(
                       "~/Scripts/jquery.jqGrid.js",
                       "~/Scripts/i18n/grid.locale-en.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css",
                      "~/Content/layout.css"));

            bundles.Add(new StyleBundle("~/Content/jquerygrid").Include(
                     "~/Content/themes/base/jquery-ui.css",
                     "~/Content/jquery.jqGrid/ui.jqgrid.css"));

            //bundles.Add(new StyleBundle("~/Content/advertisement").Include(
            //         "~/Content/PagedList.css"));
        }
    }
}
