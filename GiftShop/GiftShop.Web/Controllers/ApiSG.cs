using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using GiftShop.Web.Filters;

namespace GiftShop.Web.Controllers
{
    public class ApiSG : ApiController
    {
        public Authentication UserLogged { get; set; }

        protected HttpResponseMessage responseWithError(Exception ex)
        {
            //register errors into logs
            return responseWithError(HttpStatusCode.InternalServerError, ex);
        }
        
        protected HttpResponseMessage responseWithError(HttpStatusCode code, Exception ex)
        {
            //register errors into logs
            return this.Request.CreateErrorResponse(code, ex);
        }

        protected async Task<HttpResponseMessage> CreateHttpResponseAsync(HttpRequestMessage request, Func<Task<HttpResponseMessage>> function)
        {
            HttpResponseMessage response = null;

            try
            {
                if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    if (Thread.CurrentPrincipal.Identity is Authentication)
                    {
                        Authentication basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as Authentication;
                        UserLogged = basicAuthenticationIdentity;
                    }
                }
                response = await function.Invoke();
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return response;
        }
    }
}
