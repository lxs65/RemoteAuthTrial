using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using EolSim.Handlers;
using EolSim.RoutingSupport;
using Microsoft.AspNet.FriendlyUrls;

namespace EolSim
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapHttpHandler<RemoteAuthenticationHttpHandler>("RemoteAuthenticationHttpHandler", "handler/remoteAuth/{*pathInfo}");
        }
    }
}
