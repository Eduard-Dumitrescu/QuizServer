using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace QuizServer.FilterAttribute
{
    public class SslRequired : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                HttpResponseMessage msg = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    ReasonPhrase = "SSL Required!"
                };
                actionContext.Response = msg;
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }
    }
}