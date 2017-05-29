using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class UserDal :IUserDal
    {
        public List<User> GetAllUsers()
        {
            using (var context = new QuizEntities())
            {
                return context.Users.Include("Exams").Include("UserSessions").ToList();
            }
        }
    }
}
