using System.Web;
using System.Web.Optimization;

namespace KoloLos
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css", "~/Content/ribbon.css", "~/Content/table.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }

        /*
         
         * You could write a custom bundle orderer (IBundleOrderer) that will ensure bundles are included in the order you register them:

public class AsIsBundleOrderer : IBundleOrderer
{
    public virtual IEnumerable<FileInfo> OrderFiles(BundleContext context, IEnumerable<FileInfo> files)
    {
        return files;
    }
}
and then:

public class BundleConfig
{
    public static void RegisterBundles(BundleCollection bundles)
    {
        var bundle = new Bundle("~/bundles/scripts/canvas");
        bundle.Orderer = new AsIsBundleOrderer();
        bundle
            .Include("~/Scripts/modernizr-*")
            .Include("~/Scripts/json2.js")
            .Include("~/Scripts/columnizer.js")
            .Include("~/Scripts/jquery.ui.message.min.js")
            .Include("~/Scripts/Shared/achievements.js")
            .Include("~/Scripts/Shared/canvas.js");
        bundles.Add(bundle);
    }
}
and in your view:

@Scripts.Render("~/bundles/scripts/canvas")
         
         */
    }
}