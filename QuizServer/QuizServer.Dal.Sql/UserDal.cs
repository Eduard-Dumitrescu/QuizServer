using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                return context.Users.Include(c => c.UserSessions).Include(c => c.Exams.Select(e => e.Test)).ToList();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var context = new QuizEntities())
            {
                return context.Users.FirstOrDefault(c => c.Email == username);
            }
        }

        public User AddUser(Guid id,string email,Guid salt,string password,bool isAdmin)
        {
            using (var context = new QuizEntities())
            {
                var user = new User()
                {
                    Id = id,
                    Email = email,
                    Salt = salt,
                    IsAdmin = isAdmin,
                    Password = password
                };
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }
    }
}
