using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Web.Culture
{
    public class RouteConstraintRegex : IRouteConstraint
    {
        private readonly string _defaultValue;
        private readonly string _pattern;

        public RouteConstraintRegex(string pattern, string defaultValue = "xx")
        {
            _defaultValue = defaultValue;
            _pattern = pattern;
        }

        public bool Match(HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var val = values[parameterName];
            if (val == null)
            {
                return false;
            }
            var countryCode = val.ToString();

            if (routeDirection == RouteDirection.UrlGeneration
                && _defaultValue.Equals(countryCode, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            var match = Regex.IsMatch(countryCode, _pattern);
            return match;
        }
    }
}
