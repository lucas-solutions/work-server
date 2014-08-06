using System.Web;
using System.Web.Optimization;

namespace Lucas.Solutions
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //
            // Require JS modules
            bundles.Add(new ScriptBundle("~/require.js").Include(
                        "~/Scripts/require.js"));

            // Require JS main for My Account
            bundles.Add(new ScriptBundle("~/account.js").Include(
                        "~/Scripts/account.js"));

            // Require JS main for Dashboard
            bundles.Add(new ScriptBundle("~/dashboard.js").Include(
                        "~/Scripts/dashboard.js"));

            // Require JS main for My Status
            bundles.Add(new ScriptBundle("~/status.js").Include(
                        "~/Scripts/status.js"));

            //
            // Vendors
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.event.drag.js",
                        "~/Scripts/jquery.event.drag.live.js",
                        "~/Scripts/q.js"));

            bundles.Add(new ScriptBundle("~/scripts/slick-grid").Include(
                        "~/Scripts/slick.core.js",
                        "~/Scripts/slick.grid.js",
                        "~/Scripts/slick.dataview.js",
                        "~/Scripts/slick.formatters.js",
                        "~/Scripts/slick.editor.js",
                        "~/Scripts/slick.remotemodel.js",
                        "~/Scripts/slick.groupitemmetadataprovider.js",
                        "~/Scripts/controls/slick.columnpicker.js",
                        "~/Scripts/controls/slick.pager.js",
                        "~/Scripts/plugins/slick.autotooltips.js",
                        "~/Scripts/plugins/slick.cellcopymanager.js",
                        "~/Scripts/plugins/slick.cellrangedecorator.js",
                        "~/Scripts/plugins/slick.cellrangeselector.js",
                        "~/Scripts/plugins/slick.cellselectionmodel.js",
                        "~/Scripts/plugins/slick.checkboxselectcolumn.js",
                        "~/Scripts/plugins/slick.headermenu.js",
                        "~/Scripts/plugins/slick.rowselectionmanager.js",
                        "~/Scripts/plugins/slick.rowselectionmodel.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/scripts/knockout").Include(
                      "~/Scripts/knockout-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Styles/grid.css"));

            bundles.Add(new StyleBundle("~/styles/slick-grid").Include(
                      "~/Styles/slick.grid.css",
                      "~/Styles/slick.pager.css",
                      "~/Styles/slick.columnpicker.css",
                      "~/Styles/slick.headerbuttons.css",
                      "~/Styles/slick.headermenu.css",
                      "~/Styles/slick-default-theme.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
