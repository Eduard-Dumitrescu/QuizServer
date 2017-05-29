using System.Collections.Generic;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal
{
    public interface IUserDal
    {
        List<User> GetAllUsers();
    }
}
