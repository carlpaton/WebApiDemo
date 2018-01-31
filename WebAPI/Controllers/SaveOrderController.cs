using SharedModels;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.CustomAttributes;

namespace WebAPI.Controllers
{
    public class SaveOrderController : ApiController
    {
        [ApiKeyValidation]
        public HttpResponseMessage Post([FromBody]List<OrderModel> obj)
        {
            if (obj == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new List<OrderModel>());

            var response = obj;
            for (int i = 0; i < response.Count; i++)
            {
                response[i].Id = i + 1;
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
