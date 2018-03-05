using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GiftShop.Core.Data;
using GiftShop.Core.Exceptions;
using GiftShop.Core.Generic;
using GiftShop.Web.Models;

namespace GiftShop.Web.Controllers
{
    [RoutePrefix("api/Login")]
    [AllowAnonymous]
    public class LoginController : ApiSG
    {
        private readonly IAuthenticationService _service;

        public LoginController(IAuthenticationService service)
        {
            _service = service;
        }

        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage authenticate(HttpRequestMessage request, LoginViewModel user)
        {
            HttpResponseMessage response = null;
            try
            {
                User userl = null;
                try
                {

                    userl = _service.Authenticate(user.Username, user.Password);
                }
                catch (LoginException logex)
                {
                    return request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        Status = "ERROR",
                        Message = logex.Message
                    });
                }

                return request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        Status = "OK",
                        Message = "Autentificación Satisfactoria",
                        userl.ID,
                        userl.Username,
                        userl.Email,
                        userl.IsLocked,
                        userl.IsAdmin
                    });

            }
            catch (Exception ex)
            {
                return this.responseWithError(ex);
            }
        }
    }
}
