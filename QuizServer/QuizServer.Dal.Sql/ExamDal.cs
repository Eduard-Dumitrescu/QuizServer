using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class ExamDal : IExamDal
    {
        public Exam AssignTestToUser(Guid userId, int testId)
        {
            using (var context = new QuizEntities())
            {
                var exam = new Exam()
                {
                    UserID = userId,
                    TestID = testId,
                };
                var savedExam = context.Exams.Add(exam);
                context.SaveChanges();
                return savedExam;
            }
        }

        public List<Exam> GetAllExams()
        {
            using (var context = new QuizEntities())
            {
                return context.Exams.Include(c => c.Test).Include(c => c.User).ToList();
            }
        }

        public Exam GetExamById(int id)
        {
            using (var context = new QuizEntities())
            {
                return context.Exams.Include(c => c.Test).Include(c => c.User).FirstOrDefault(c => c.Id == id);
            }
        }

        public Exam GetExamByUserId(Guid userId)
        {
            using (var context = new QuizEntities())
            {
                return context.Exams.Include(c => c.Test).Include(c => c.User).FirstOrDefault(c => c.UserID == userId);
            }
        }
    }
}
