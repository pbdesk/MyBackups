using System.Linq;
using System.Web.Routing;

namespace WW.EnvConfigs.ApiControllers.AppStart
{
    public class FromValuesListConstraint : IRouteConstraint
    {
        public string[] Values { get; set; }

        public FromValuesListConstraint(params string[] values)
        {
            this.Values = values;
            Values = Values.Select(s => s.ToLowerInvariant()).ToArray();
        }


        public bool Match(System.Web.HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = values[parameterName].ToString().ToLower();
            return Values.Contains(value);
        }
    }
}
