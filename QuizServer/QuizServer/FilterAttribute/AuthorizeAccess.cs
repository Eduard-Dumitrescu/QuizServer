using System.Web.Http.Controllers;
using QuizServer.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace QuizServer.FilterAttribute
{
    public class AuthorizeAccess : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var baseController = filterContext.ControllerContext.Controller as BaseController;

            if (!baseController.Authorize())
            {
                filterContext.Response = baseController.UnauthorizedAccess();
            }
        }
    }
}