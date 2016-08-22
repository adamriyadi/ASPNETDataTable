using System.Web;
using System.Web.Optimization;

namespace ASPNETDataTable.Demo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Assets/js/jquery").Include(
                        "~/Assets/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Assets/js/jqueryval").Include(
                        "~/Assets/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Assets/js/modernizr").Include(
                        "~/Assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Assets/js/bootstrap").Include(
                      "~/Assets/js/bootstrap.js",
                      "~/Assets/js/respond.js"));

            bundles.Add(new StyleBundle("~/Assets/css/style").Include(
                      "~/Assets/css/bootstrap.css",
                      "~/Assets/css/site.css"));
        }
    }
}
