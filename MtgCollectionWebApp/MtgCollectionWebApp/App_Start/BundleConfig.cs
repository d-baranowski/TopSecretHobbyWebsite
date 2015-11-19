using System.Web;
using System.Web.Optimization;

namespace MtgCollectionWebApp
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/templates/bs-ecosave/css").Include(
                      "~/templates/bs-ecosave/dark/assets/css/animate.css",
                      "~/templates/bs-ecosave/dark/assets/css/font-awesome.min.css",
                      "~/templates/bs-ecosave/dark/assets/css/prettyPhoto.css",
                      "~/templates/bs-ecosave/dark/assets/css/style.css"));

            bundles.Add(new ScriptBundle("~/templates/bs-ecosave/").Include(
                      "~/templates/bs-ecosave/dark/assets/js/custom.js",
                      "~/templates/bs-ecosave/dark/assets/js/jquery.easing.min.js",
                      "~/templates/bs-ecosave/dark/assets/js/jquery.prettyPhoto.js",
                      "~/templates/bs-ecosave/dark/assets/js/wow.min.js"));
        }
    }
}
