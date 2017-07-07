using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal
{
    public interface ITestDal
    {
        List<Test> GetAllTests();
        Test GetTestById(int id);
        Test AddTest(string name,int difficultyId,TimeSpan? duration);
        Test AddTestWithQuestions(string name, int difficultyId, TimeSpan? duration, List<Question> questionList);
        Test GetTestWithQuestionsById(int id);
    }
}
