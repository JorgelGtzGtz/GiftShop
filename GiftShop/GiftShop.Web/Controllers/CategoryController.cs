using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GiftShop.Core.Data;
using GiftShop.Core.Generic;
using GiftShop.Web.Infrastructure;
using GiftShop.Web.Models;

namespace GiftShop.Web.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiSG
    {
        private readonly ICategoryDataService _service;

        public CategoryController(ICategoryDataService service)
        {
            _service = service;
        }

        [Route("list/{page:int=0}/{pageSize=50}/{Description?}")]
        [HttpGet]
        public async Task<HttpResponseMessage> List(HttpRequestMessage request, int? page, int? pageSize, string Description = null)
        {
            return await CreateHttpResponseAsync(request, async () =>
            {
                int currentPage = page.Value;
                int currentPageSize = pageSize.Value;
                int totalRecords = 0;
                HttpResponseMessage response = null;
                List<object> list = _service.ListCategoryByFilter(Description);
                totalRecords = list.Count;
                PaginationSet<object> pagedSet = list.ToPagedList(currentPage, totalRecords, currentPageSize);
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                return await Task.FromResult(response);
            });
        }

        [Route("save")]
        [HttpPost]
        public async Task<HttpResponseMessage> Guardar(HttpRequestMessage request, Category model)
        {
            return await CreateHttpResponseAsync(request, async () =>
            {
                HttpResponseMessage response = null;
                string error = String.Empty;
                bool result = false;
                try
                {
                    if (model != null && model.ID == -1)
                    {
                        result = _service.AddUpdateCategory(model, out error);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Status = result ? "OK" : "ERROR", Message = error, Sucess = result });
                    }
                    else
                    {
                        result = _service.AddUpdateCategory(model, out error);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Status = result ? "OK" : "ERROR", Message = error, Sucess = result });
                    }
                }
                catch (Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        Status = "ERROR",
                        Message = ex.Message
                    });
                }

                return await Task.FromResult(response);
            });

        }

        [Route("delete")]
        [HttpPost]
        public async Task<HttpResponseMessage> Eliminar(HttpRequestMessage request, Category model)
        {
            return await CreateHttpResponseAsync(request, async () =>
            {
                HttpResponseMessage response = null;
                string error = String.Empty;
                bool result = false;
                try
                {
                    if (model != null)
                    {
                        result = _service.DeleteCategory(model.ID, out error);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Status = result ? "OK" : "ERROR", Message = error, Sucess = result });
                    }
                    else
                    {
                        error = "Null model...";
                        response = request.CreateResponse(HttpStatusCode.OK, new { Status = result ? "OK" : "ERROR", Message = error, Sucess = result });
                    }

                }
                catch (Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        Status = "ERROR",
                        Message = ex.Message
                    });
                }

                return await Task.FromResult(response);
            });

        }
    }
}
