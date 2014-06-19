using System.Web;
using System.Web.Optimization;

namespace TDKT
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/metisMenu/jquery.metisMenu.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/Datatables/dataTables.js",
                        "~/Scripts/Datatables/dataTables.bootstrap.js",
                        "~/Scripts/Datatables/dataTables.editable.js"));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                        "~/Content/Datatables/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                      "~/Scripts/toastr.js"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                      "~/Content/toastr.css"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/Datepicker/bootstrap-datepicker.js",
                        "~/Scripts/Datepicker/locales/bootstrap-datepicker.vi.js"));

            //bundles.Add(new StyleBundle("~/Content/datepicker").Include(
            //          "~/Content/Datepicker/datepicker.css"));

            //bundles.Add(new ScriptBundle("~/bundles/GlobalizeValidate").Include(
            //            "~/Scripts/globalize/cultures/globalize.culture.vi-VN.js",
            //            "~/Scripts/globalize/globalize.js",
            //            "~/Scripts/jquery.validate.globalize.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
