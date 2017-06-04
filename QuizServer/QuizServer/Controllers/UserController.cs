using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.Model.Dtos;
using QuizServer.Model.Models;
using QuizServer.Security;

namespace QuizServer.Controllers
{
    public class UserController : ApiController
    {
        private IUserDal _userDal;
        private IUserSessionDal _userSessionDal;

        public UserController() : this(new UserDal(),new UserSessionsDal())
        {
                
        }
        public UserController(IUserDal userDal,IUserSessionDal userSessionDal)
        {
            _userDal = userDal;
            _userSessionDal = userSessionDal;
        }

        public ICollection<User> Get()
        {
            var _userDal = new UserDal();
            return _userDal.GetAllUsers();
        }

        public HttpResponseMessage PostLogin([FromBody]UserModel user)
        {
            var messageModel = new MessageModel();
            var foundUser = _userDal.GetUserByUsername(user.Username);

            if (foundUser == null)
            {
                messageModel.Message = "User with given email does not exist";
                return Request.CreateResponse(HttpStatusCode.BadRequest, messageModel);
            }

            var password = foundUser.Id + user.Password;

            if (foundUser.Password.Equals(SHA512Encrypter.Encrypt(password), StringComparison.OrdinalIgnoreCase))
            {
                var checkForSession = _userSessionDal.GetAllUserSessions().FirstOrDefault(u => u.UserID == foundUser.Id && u.IsValid == true) != null;
                if(checkForSession)
                    _userSessionDal.SetSessionInvalid(foundUser.Id);
             
                _userSessionDal.AddUserSession(foundUser.Id);

                messageModel.Authorization =
                    _userSessionDal.GetAllUserSessions()
                        .FirstOrDefault(u => u.UserID == foundUser.Id && u.IsValid == true).Id.ToString();

                messageModel.Message = foundUser.IsAdmin ? "Admin" : "User";
                return Request.CreateResponse(HttpStatusCode.OK, messageModel);
            }

            messageModel.Message = "Password is incorrect";
            return Request.CreateResponse(HttpStatusCode.BadRequest, messageModel);
        }
    }
}
