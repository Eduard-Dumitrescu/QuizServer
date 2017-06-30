using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal
{
    public interface IExamDal
    {
        List<Exam> GetAllExams();
        Exam AssignTestToUser(Guid userId, int testId);
        Exam GetExamById(int id);
        Exam GetExamByUserId(Guid userId);
        Exam UpdateUserTest(Guid userId, int testId);
    }
}
