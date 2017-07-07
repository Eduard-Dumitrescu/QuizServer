using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.FilterAttribute;

namespace QuizServer.Controllers
{ 
    [EnableCors("http://localhost:41093", "*", "*")]
    [SslRequired]
    public class BaseController : ApiController
    {
        protected IUserSessionDal _userSessionDal;

        public BaseController() : this(new UserSessionsDal())
        {

        }
        public BaseController(IUserSessionDal userSessionDal)
        {
            _userSessionDal = userSessionDal;
        }
        [NonAction]
        public HttpResponseMessage UnauthorizedAccess()
        {
            return Request.CreateResponse(HttpStatusCode.Forbidden);
        }
        [NonAction]
        public bool Authorize()
        {
            var authorize = Request.Headers.Authorization;
            if (authorize == null)
                return false;
            var userSession = _userSessionDal.GetUserSessionById(new Guid(authorize.ToString()));
            return userSession != null && (userSession.User.IsAdmin && userSession.IsValid);
        }

        [NonAction]
        public bool AuthorizeUser()
        {
            var authorize = Request.Headers.Authorization;
            if (authorize == null)
                return false;
            var userSession = _userSessionDal.GetUserSessionById(new Guid(authorize.ToString()));
            return userSession != null && (!userSession.User.IsAdmin && userSession.IsValid);
        }
    }
}
