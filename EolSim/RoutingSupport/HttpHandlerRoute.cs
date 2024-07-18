using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace EolSim.RoutingSupport
{
    public class HttpHandlerRoute<T> : IRouteHandler where T : IHttpHandler, new()
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new T();
        }
    }
}