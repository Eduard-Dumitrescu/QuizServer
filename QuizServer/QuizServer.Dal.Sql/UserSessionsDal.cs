using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class UserSessionsDal : IUserSessionDal
    {
        public List<UserSession> GetAllUserSessions()
        {
            using (var context = new QuizEntities())
            {
                return context.UserSessions.Include("User").ToList();
            }
        }

        public UserSession GetUserSessionById(Guid id)
        {
            using (var context = new QuizEntities())
            {
                return context.UserSessions.Include("User").FirstOrDefault(c => c.Id == id);
            }
        }

        public void AddUserSession(Guid userId)
        {
            using (var context = new QuizEntities())
            {
                var userSession = new UserSession()
                {
                    Id = Guid.NewGuid(),
                    UserID = userId,
                    IsValid = true,
                    LoggedIn = DateTime.Now
                };

                context.UserSessions.Add(userSession);
                context.SaveChanges();
            }
        }

        public UserSession SetSessionInvalid(Guid authorization)
        {
            using (var context = new QuizEntities())
            {
                var userSession = context.UserSessions.FirstOrDefault(c => c.Id == authorization && c.IsValid);

                if (userSession == null)
                {
                    return null;
                }

                userSession.LoggedOut = DateTime.Now;
                userSession.IsValid = false;
                context.SaveChanges();
                return userSession;
            }
        }

        public void DeletePreviousSessions(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
