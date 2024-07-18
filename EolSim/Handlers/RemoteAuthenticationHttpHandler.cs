using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EolSim.Handlers
{
    public class RemoteAuthenticationHttpHandler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            if (!HasValidApiKey(context))
            {
                context.Response.StatusCode = 401;
                context.Response.End();
                return;
            }

            if (!IsContextualUserAuthenticated(context))
            {
                context.Response.StatusCode = 403;
                context.Response.End();
                return;
            }

            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/octet-stream";
            InjectClaimsPrincipalInto(context);
            context.Response.End();
        }

        private bool IsContextualUserAuthenticated(HttpContext context)
        {
            return context.User.Identity.IsAuthenticated;
        }

        private bool HasValidApiKey(HttpContext context)
        {
            var providedApiKey = context.Request.Headers["X-SystemWebAdapter-RemoteAppAuthentication-Key"];
            var expectedApiKey = ConfigurationManager.AppSettings["RemoteAppApiKey"];
            return string.Equals(providedApiKey, expectedApiKey, StringComparison.Ordinal);
        }

        private void InjectClaimsPrincipalInto(HttpContext context)
        {
            var identity = new ClaimsIdentity(context.User.Identity);
            var principal = new ClaimsPrincipal(identity);

            using (var writer = new BinaryWriter(context.Response.OutputStream))
            {
                principal.WriteTo(writer);
            }
        }
    }
}