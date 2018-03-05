using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GiftShop.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class GSAuthenticationFilter : ActionFilterAttribute
    {
        public GSAuthenticationFilter() { }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
                return;

            var identity = ParseAuthorizationHeader(actionContext);

            if (identity == null)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("", "Basic"), null);
                throw new HttpResponseException(actionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Identity not found!!"));
            }
            else
            {
                var principal = new GenericPrincipal(identity, null);
                Thread.CurrentPrincipal = principal;
            }
        }

        protected virtual GenericIdentity ParseAuthorizationHeader(HttpActionContext actionContext)
        {
            try
            {
                if (!actionContext.Request.Headers.Contains("Authorization"))
                    throw new AuthenticationException("Invalid token (Missing data)");

                string authToken = HttpContext.Current.Request.Headers["Authorization"];
                string[] parts = authToken.Split(' ');
                if (parts.Length != 2)
                    throw new AuthenticationException("Invalid token (Missing data: Authentication Parts)");

                if (parts[0] != "GS")
                    throw new AuthenticationException("Invalid token (Bad trailing)");

                byte[] bytes = Convert.FromBase64String(parts[1]);
                string decoded = Encoding.UTF8.GetString(bytes);

                string[] authParts = decoded.Split(':');
                string username = authParts[0];
                string password = authParts[1];

                if (authParts.Length != 2)
                    return new GenericIdentity("", "Basic");

                return new GenericIdentity(username, "Basic");
            }
            catch (AuthenticationException)
            {
                return null;
            }
        }
        private bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}