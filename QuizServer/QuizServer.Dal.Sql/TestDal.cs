using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class TestDal : ITestDal
    {
        public List<Test> GetAllTests()
        {
            using (var context = new QuizEntities())
            {
                return context.Tests.Include(c => c.Difficulty).ToList();
            }
        }

        public Test GetTestById(int id)
        {
            using (var context = new QuizEntities())
            {
                return context.Tests.Include(c => c.Difficulty).FirstOrDefault(c => c.Id == id);
            }
        }

        public Test AddTest(string name, int difficultyId,TimeSpan? duration)
        {
            using (var context = new QuizEntities())
            {
                var test = new Test()
                {
                    Name = name,
                    DifficultyID = difficultyId,
                    Duration = duration
                };

                var rez = context.Tests.Add(test);
                context.SaveChanges();

                return rez;
            }
        }

        public Test AddTestWithQuestions(string name, int difficultyId, TimeSpan? duration, List<Question> questionList)
        {
            using (var context = new QuizEntities())
            {
                var test = new Test()
                {
                    Name = name,
                    DifficultyID = difficultyId,
                    Duration = duration,
                    Questions = questionList
                };

                var rez = context.Tests.Add(test);
                context.SaveChanges();

                return rez;
            }
        }

        public Test GetTestWithQuestionsById(int id)
        {
            using (var context = new QuizEntities())
            {
                return context.Tests.Include(c => c.Questions).
                                     Include(c => c.Questions.Select(q => q.Category)).
                                     Include(c => c.Questions.Select(q => q.Difficulty)).
                                     Include(c => c.Questions.Select(q => q.Answers)).FirstOrDefault(f => f.Id == id);
            }
        }
    }
}
