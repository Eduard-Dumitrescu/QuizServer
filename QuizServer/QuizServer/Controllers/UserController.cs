using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Http.Cors;
using QuizServer.Dal;
using QuizServer.Dal.Sql;
using QuizServer.FilterAttribute;
using QuizServer.Model.Dtos;
using QuizServer.Model.Models;
using QuizServer.Security;

namespace QuizServer.Controllers
{
    [EnableCors("http://localhost:41093", "*", "*")]
    public class UserController : BaseController
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

        [Route("api/User/Login")]
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
                var checkForSession = _userSessionDal.GetAllUserSessions().FirstOrDefault(u => u.UserID == foundUser.Id && u.IsValid) != null;
                if(checkForSession)
                    _userSessionDal.SetSessionInvalid(foundUser.Id);
             
                _userSessionDal.AddUserSession(foundUser.Id);

                messageModel.Authorization =
                    _userSessionDal.GetAllUserSessions()
                        .FirstOrDefault(u => u.UserID == foundUser.Id && u.IsValid).Id.ToString();

                messageModel.Message = foundUser.IsAdmin ? "Admin" : "User";
                return Request.CreateResponse(HttpStatusCode.OK, messageModel);
            }

            messageModel.Message = "Password is incorrect";
            return Request.CreateResponse(HttpStatusCode.BadRequest, messageModel);
        }

        [AuthorizeAccess]
        public HttpResponseMessage GetUsers()
        {
            var userList = new List<UserDataModel>();

            foreach (var user in _userDal.GetAllUsers().Where(u => u.IsAdmin == false))
            {
                var testName = user.Exams.FirstOrDefault(u => u.UserID == user.Id && u.Grade == null); 
                    userList.Add(new UserDataModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FinishedTests = user.Exams.Count(u => u.UserID == user.Id && u.Grade != null),
                        CurrentTest = testName != null ? testName.Test.Name : "No test assigned"
                    });
            }

            return Request.CreateResponse(HttpStatusCode.OK,userList);
        }

        [AuthorizeAccess]
        public HttpResponseMessage PostUser([FromBody]UserDataModel userEmail)
        {
            var isRegistered = _userDal.GetUserByUsername(userEmail.Email) != null;
            if (isRegistered)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "User already exists" });
            var id = Guid.NewGuid();
            var password = SHA512Encrypter.Encrypt(id + "1234");
            _userDal.AddUser(Guid.NewGuid(), userEmail.Email,Guid.NewGuid(), SHA512Encrypter.Encrypt(password),false);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
