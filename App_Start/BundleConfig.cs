using System.Web.Optimization;

namespace MercadoVentasTP
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            /* 
                SCRIPTS
            */

            // Jquery Validate
            string jqValCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.17.0/jquery.validate.min.js";
            Bundle jqValBundle = new ScriptBundle("~/bundles/jqueryval", jqValCdnPath).Include("~/Scripts/jquery-validate/jquery.validate.min.js");
            bundles.Add(jqValBundle);

            // Jquery Validate Unobtrusive
            string jqValUnobCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.10/jquery.validate.unobtrusive.min.js";
            Bundle jqValUnobBundle = new ScriptBundle("~/bundles/jqueryunob", jqValUnobCdnPath).Include("~/Scripts/jquery-validate/jquery.validate.unobtrusive.min.js");
            bundles.Add(jqValUnobBundle);

            // Bootstrap JS
            string btsCdnPath = "https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js";
            Bundle btsBundle = new ScriptBundle("~/bundles/bootstrap", btsCdnPath).Include("~/Scripts/bootstrap.min.js");
            bundles.Add(btsBundle);

            // JQuery JS
            string jqCdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js";
            Bundle jqBundle = new ScriptBundle("~/bundles/btsjquery", jqCdnPath).Include("~/Scripts/jquery.min.js");
            bundles.Add(jqBundle);

            // Popper JS
            string popperCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.6/umd/popper.min.js";
            Bundle popperBundle = new ScriptBundle("~/bundles/popper", popperCdnPath).Include("~/Scripts/popper.min.js");
            bundles.Add(popperBundle);

            // Popper JS
            Bundle customScript = new ScriptBundle("~/bundles/script").Include("~/Scripts/script.js");
            bundles.Add(customScript);

            // Jquery JS
            Bundle jqScript = new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-3.4.1.min.js");
            bundles.Add(jqScript);

            // Jquery JS
            Bundle mdbcript = new ScriptBundle("~/bundles/jquery").Include("~/Scripts/mdb.js");
            bundles.Add(mdbcript);

            // Bootstrap Select JS
            string btsSelectCdnPath = "https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.9/dist/js/bootstrap-select.min.js";
            Bundle btsSelectBundle = new ScriptBundle("~/bundles/bootstrapselect", btsSelectCdnPath).Include("~/Scripts/bootstrap-select.min.js");
            bundles.Add(btsSelectBundle);

            /*
                STYLES
            */

            // Bootstrap
            string btsMinCdnPath = "https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css";
            Bundle btsMinBundle = new StyleBundle("~/bundles/Content/Bootstrap", btsMinCdnPath).Include("~/Content/bootstrap.min.css");
            bundles.Add(btsMinBundle);

            // Bootstrap Select
            string btsMinSelect = "https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.9/dist/css/bootstrap-select.min.css";
            Bundle btsMinSelectBundle = new StyleBundle("~/bundles/Content/BootstrapSelect", btsMinSelect).Include("~/Content/bootstrap-select.min.css");
            bundles.Add(btsMinSelectBundle);

            // Custom           
            Bundle customBundle = new StyleBundle("~/bundles/Content/Custom").Include("~/Content/bootswatch.min.css", "~/Content/glyphicon.min.css", "~/Content/site.css");
            bundles.Add(customBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}