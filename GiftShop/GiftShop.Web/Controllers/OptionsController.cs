using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GiftShop.Core.Data;
using GiftShop.Core.Generic;

namespace GiftShop.Web.Controllers
{
    [RoutePrefix("api/OptionBase")]
    [AllowAnonymous]
    public class OptionsController : ApiSG
    {
        private readonly IOptionDataService _service;

        public OptionsController(IOptionDataService service)
        {
            _service = service;
        }

        [Route("categorylist")]
        [HttpGet]
        public async Task<HttpResponseMessage> categorylist(HttpRequestMessage request)
        {
            return await CreateHttpResponseAsync(request, async () =>
            {
                HttpResponseMessage response = null;
                List<object> list = _service.ListCategory();
                response = request.CreateResponse(HttpStatusCode.OK, list);
                return await Task.FromResult(response);
            });
        }

        [Route("prodlist")]
        [HttpGet]
        public async Task<HttpResponseMessage> prodlist(HttpRequestMessage request)
        {
            return await CreateHttpResponseAsync(request, async () =>
            {
                HttpResponseMessage response = null;
                List<object> list = _service.ListProduct();
                response = request.CreateResponse(HttpStatusCode.OK, list);
                return await Task.FromResult(response);
            });
        }
    }
}
