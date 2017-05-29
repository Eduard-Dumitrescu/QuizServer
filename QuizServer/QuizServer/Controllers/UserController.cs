using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QuizServer.Dal.Sql;
using QuizServer.Model.Dtos;

namespace QuizServer.Controllers
{
    public class UserController : ApiController
    {
        public ICollection<User> Get()
        {
            var _userDal = new UserDal();
            return _userDal.GetAllUsers();
        }
    }
}
