using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.Model.Models;

namespace QuizServer.Controllers
{
    public class UserSessionController : ApiController
    {
        private IUserSessionDal _userSessionDal;

        public UserSessionController() :this(new UserSessionsDal())
        {
            
        }

        public UserSessionController(IUserSessionDal userSessionDal)
        {
            _userSessionDal = userSessionDal;
        }


        public SessionData PostUserSession()
        {
            var authorization = Request.Headers.Authorization.ToString();
            var userSession = _userSessionDal.GetUserSessionById(new Guid(authorization));

            var sessionData = new SessionData()
            {
                IsAdmin = userSession.User.IsAdmin,
                IsActive = userSession.IsValid
            };

            return sessionData;
        }
        [Route("api/EndSession")]
        public HttpResponseMessage PostEndSession()
        {
            var authorization = Request.Headers.Authorization.ToString();
            var userSession = _userSessionDal.SetSessionInvalid(new Guid(authorization));

            if (userSession == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

    }
}
