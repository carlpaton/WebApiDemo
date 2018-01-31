using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.CustomAttributes
{
    public class ApiKeyValidationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            try
            {
                string expectedApikey = "0000-0000-0000-0000";
                string receivedApikey = actionContext.Request.Headers.GetValues("apiPassKey").FirstOrDefault();
                if (String.IsNullOrWhiteSpace(receivedApikey) || receivedApikey != expectedApikey)
                    throw new Exception();
            }
            catch
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "ApiKeyValidation Plowed!");
                var source = new TaskCompletionSource<HttpResponseMessage>();
                source.SetResult(actionContext.Response);
                return source.Task;
            }
            return continuation();
        }
    }
}