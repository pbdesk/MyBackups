using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(
    typeof(WW.EnvConfigs.ApiControllers.AppStart.WebApiConfig), "RegisterWebApiRoutes")]
namespace WW.EnvConfigs.ApiControllers.AppStart
{
    public static class WebApiConfig
    {
        public static void RegisterWebApiRoutes()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional },
                constraints: new { controller = new FromValuesListConstraint("Locales", "Keys", "Builds", "Frameworks", "Export") }
            );
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
               name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { controller = new FromValuesListConstraint("Values") }
           );
        }
    }
}
