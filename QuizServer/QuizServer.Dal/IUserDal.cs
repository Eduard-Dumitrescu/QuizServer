using System;
using System.Collections.Generic;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal
{
    public interface IUserDal
    {
        List<User> GetAllUsers();
        User GetUserByUsername(string username);
        User AddUser(Guid id, string email, Guid salt, string password, bool isAdmin);
    }
}
