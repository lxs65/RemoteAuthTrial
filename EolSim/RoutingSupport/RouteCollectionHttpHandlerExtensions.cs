using EolSim.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EolSim.RoutingSupport
{
    public static class RouteCollectionHttpHandlerExtensions
    {
        public static void MapHttpHandler<THttpHandler>(this RouteCollection routes,
            string routeName, string routeUrl, 
            RouteValueDictionary defaults = null, 
            RouteValueDictionary contraints = null)
            where THttpHandler : IHttpHandler, new()
        {
            routes.Add(routeName, new Route(routeUrl, defaults, contraints, new HttpHandlerRoute<THttpHandler>()));
        }
    }
}