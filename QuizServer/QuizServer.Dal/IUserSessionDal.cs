using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal
{
    public interface IUserSessionDal
    {
        List<UserSession> GetAllUserSessions();
        UserSession GetUserSessionById(Guid id);
        void AddUserSession(Guid userId);
        UserSession SetSessionInvalid(Guid userId);
        void DeletePreviousSessions(Guid userId);
    }
}
