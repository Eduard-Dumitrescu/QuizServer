using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class TestWithQuestionDal : ITestWithQuestionsDal
    {
        public TestsWithQuestion AddTestWithQuestion(int testId, int questionId)
        {
            using (var context = new QuizEntities())
            {
                var testWithQuestion = new TestsWithQuestion()
                {
                    TestID = testId,
                    QuestionID = questionId
                };

               // var rez = context.TestsWithQuestions.Add(testWithQuestion);
                context.SaveChanges();
                return new TestsWithQuestion();
            }
        }
    }
}
