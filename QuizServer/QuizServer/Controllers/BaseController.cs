using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizServer.Dal;
using QuizServer.Dal.Sql;

namespace QuizServer.Controllers
{
    public class BaseController : ApiController
    {
        private IUserSessionDal _userSessionDal;

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
    }
}
