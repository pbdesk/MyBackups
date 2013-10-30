using System.Web;
using System.Web.Optimization;

namespace WW.EnvConfigs.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js")
               .Include("~/Scripts/jquery-{version}.js")
               .Include("~/Scripts/bootstrap.js")
               .Include("~/Scripts/toastr.js")
               .Include("~/Scripts/toastr-custom.js")
               .Include("~/Scripts/PBDeskUtils.js")
               .Include("~/Scripts/Custom.js")
               );

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/scripts/angular.js")
                .Include("~/scripts/angular-*")

                );


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.cerulean.css",
                      "~/Content/fontawesome/font-awesome.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/bundles/NGApp")
                .Include("~/NGApp/scripts/app/WWEnvConfigsApp.js")
                .Include("~/NGApp/scripts/directives/ngModelOnblur.js")

                .Include("~/NGApp/scripts/services/DirtyFactory.js")
                .Include("~/NGApp/scripts/services/KeysFactory.js")
                .Include("~/NGApp/scripts/services/KeyValueFactory.js")
                .Include("~/NGApp/scripts/services/AppsAndFwksFactory.js")
                .Include("~/NGApp/scripts/services/LocalesFactory.js")
                .Include("~/NGApp/scripts/services/BuildsFactory.js")
                .Include("~/NGApp/scripts/services/ExportFactory.js")
               

                 .Include("~/NGApp/scripts/controllers/BuildsControllers.js")
                 .Include("~/NGApp/scripts/controllers/KeysControllers.js")
                 .Include("~/NGApp/scripts/controllers/LocalesControllers.js")
                 .Include("~/NGApp/scripts/controllers/ValueControllers.js")
                 .Include("~/NGApp/scripts/controllers/ExportObjectController.js")
                 


                );

            string strMyNGHelperAppBasePath = "~/NGApp/MyNGHelperApp/scripts/";
            bundles.Add(new ScriptBundle("~/bundles/MyNGHelperApp")
                .Include(strMyNGHelperAppBasePath + "app/MyNGHelperApp.js")

                .Include(strMyNGHelperAppBasePath + "services/CacheFactory.js")
        
                
               );


           

            string strKeyValuesAppBasePath = "~/NGApp/KeyValuesApp/scripts/";
            bundles.Add(new ScriptBundle("~/bundles/KeyValuesApp")
                .Include( strKeyValuesAppBasePath + "app/KeyValuesApp.js")
                .Include( strKeyValuesAppBasePath + "directives/ngModelOnblur.js")

                .Include(strKeyValuesAppBasePath + "services/DirtyFactory.js")
                .Include(strKeyValuesAppBasePath + "services/KeysFactory.js")
                .Include(strKeyValuesAppBasePath + "services/KeyValueFactory.js")

                .Include(strKeyValuesAppBasePath + "controllers/KeysControllers.js")
                .Include(strKeyValuesAppBasePath + "controllers/ValueControllers.js")
               
                );

            string strMasterAppBasePath = "~/NGApp/MasterApp/scripts/";
            bundles.Add(new ScriptBundle("~/bundles/MasterApp")
                .Include(strMasterAppBasePath + "app/MasterApp.js")

               // .Include(strMasterAppBasePath + "services/CacheFactory.js")
                .Include(strMasterAppBasePath + "services/FrameworkFactory.js")
                .Include(strMasterAppBasePath + "services/LocalesFactory.js")
                .Include(strMasterAppBasePath + "services/BuildsFactory.js")
                .Include(strMasterAppBasePath + "services/WWAppsFactory.js")
                .Include(strMasterAppBasePath + "services/ExportFactory.js")

                .Include(strMasterAppBasePath + "controllers/FrameworkController.js")
                .Include(strMasterAppBasePath + "controllers/LocalesControllers.js")
                .Include(strMasterAppBasePath + "controllers/BuildsControllers.js")
                .Include(strMasterAppBasePath + "controllers/WWAppsControllers.js")
                .Include(strMasterAppBasePath + "controllers/ExportController.js")
                );

        }
    }
}
