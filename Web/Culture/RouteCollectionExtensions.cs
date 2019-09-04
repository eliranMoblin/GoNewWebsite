using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using Common.ExtentionMethods;

namespace Web.Culture
{
    public static class RouteCollectionExtensions
    {
        public static void MapLocalizedMvcAttributeRoutes(this RouteCollection routes, string urlPrefix, object constraints)
        {
            MapLocalizedMvcAttributeRoutes(routes, urlPrefix, new RouteValueDictionary(constraints));
        }

        private static void MapLocalizedMvcAttributeRoutes(this RouteCollection routes, string urlPrefix, RouteValueDictionary constraints)
        {
            var routeCollectionRouteType = Type.GetType("System.Web.Mvc.Routing.RouteCollectionRoute, System.Web.Mvc");
            var subRouteCollectionType = Type.GetType("System.Web.Mvc.Routing.SubRouteCollection, System.Web.Mvc");
            FieldInfo subRoutesInfo = routeCollectionRouteType.GetField("_subRoutes", BindingFlags.NonPublic | BindingFlags.Instance);

            var subRoutes = Activator.CreateInstance(subRouteCollectionType);
            var routeEntries = Activator.CreateInstance(routeCollectionRouteType, subRoutes);

            // Add the route entries collection first to the route collection
            routes.Add((RouteBase)routeEntries);

            var localizedRouteTable = new RouteCollection();

            // Get a copy of the attribute routes
            localizedRouteTable.MapMvcAttributeRoutes();
            foreach (var routeBase in localizedRouteTable)
            {
                if (routeBase.GetType() == routeCollectionRouteType)
                {
                    // Get the value of the _subRoutes field
                    var tempSubRoutes = subRoutesInfo.GetValue(routeBase);

                    // Get the PropertyInfo for the Entries property
                    PropertyInfo entriesInfo = subRouteCollectionType.GetProperty("Entries");

                    if (entriesInfo.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                    {
                        foreach (RouteEntry routeEntry in (IEnumerable)entriesInfo.GetValue(tempSubRoutes))
                        {
                            var route = routeEntry.Route;
                            if (routeEntry.Name.Contains("categor", StringComparison.CurrentCultureIgnoreCase))
                            {

                            }
                            // Create the localized route
                            var localizedRoute = CreateLocalizedRoute(route, urlPrefix, constraints);

                            // Add the localized route entry
                            var localizedRouteEntry = CreateLocalizedRouteEntry(routeEntry.Name, localizedRoute);
                            AddRouteEntry(subRouteCollectionType, subRoutes, localizedRouteEntry);

                            // Add the localized link generation route
                            var localizedLinkGenerationRoute = CreateLinkGenerationRoute(localizedRoute);
                            if (localizedRouteEntry.Name == null)
                            {
                                routes.Add(localizedLinkGenerationRoute);
                            }
                            else
                            {
                                routes.Add($"{localizedRouteEntry.Name}", localizedLinkGenerationRoute);
                            }

                            // Add the default route entry
                            AddRouteEntry(subRouteCollectionType, subRoutes, routeEntry);

                            // Add the default link generation route
                            var linkGenerationRoute = CreateLinkGenerationRoute(route);
                            if (routeEntry.Name == null)
                            {
                                routes.Add(linkGenerationRoute);
                            }
                            else
                            {
                                routes.Add($"{routeEntry.Name}", linkGenerationRoute);
                            }
                        }
                    }
                }
            }

        }

        private static Route CreateLocalizedRoute(Route route, string urlPrefix, RouteValueDictionary constraints)
        {
            // Add the URL prefix
            var routeUrl = urlPrefix + route.Url;

            // Combine the constraints
            var routeConstraints = new RouteValueDictionary(constraints);
            foreach (var constraint in route.Constraints)
            {
                routeConstraints.Add(constraint.Key, constraint.Value);
            }
            var retRoute = new Route(routeUrl, route.Defaults, routeConstraints, route.DataTokens, route.RouteHandler);
            return retRoute;
        }

        private static RouteEntry CreateLocalizedRouteEntry(string name, Route route)
        {
            var localizedRouteEntryName = string.IsNullOrEmpty(name) ? null : name + "_Localized";
            var routeEntry = new RouteEntry(localizedRouteEntryName, route);
            return routeEntry;
        }

        private static void AddRouteEntry(Type subRouteCollectionType, object subRoutes, RouteEntry newEntry)
        {
            var addMethodInfo = subRouteCollectionType.GetMethod("Add");
            addMethodInfo.Invoke(subRoutes, new[] { newEntry });
        }

        private static RouteBase CreateLinkGenerationRoute(Route innerRoute)
        {
            var linkGenerationRouteType = Type.GetType("System.Web.Mvc.Routing.LinkGenerationRoute, System.Web.Mvc");
            var routeBase = (RouteBase)Activator.CreateInstance(linkGenerationRouteType, innerRoute);

            return routeBase;
        }

        public static void MapPageRouteWithName(this RouteCollection routes, string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess = true,
            RouteValueDictionary defaults = default(RouteValueDictionary), RouteValueDictionary constraints = default(RouteValueDictionary), RouteValueDictionary dataTokens = default(RouteValueDictionary))
        {
            if (dataTokens == null)
                dataTokens = new RouteValueDictionary();

            dataTokens.Add("route-name", routeName);
            routes.MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, constraints, dataTokens);
        }

        public static string GetRouteName(this RouteData routeData)
        {
            if (routeData.DataTokens["route-name"] != null)
                return routeData.DataTokens["route-name"].ToString();
            else return String.Empty;
        }

    }
}