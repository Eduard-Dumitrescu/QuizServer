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

        public Exam UpdateUserTest(Guid userId, int testId)
        {
            using (var context = new QuizEntities())
            {
                var exam = context.Exams.FirstOrDefault(c => c.UserID == userId && String.IsNullOrEmpty(c.Grade));
                exam.TestID = testId;
                context.SaveChanges();
                return exam;
            }
        }

        public void StartTimerById(int id)
        {
            using (var context = new QuizEntities())
            {
                var exam = context.Exams.FirstOrDefault(c => c.Id == id);
                exam.StartTime = DateTime.Now;
                context.SaveChanges();
            }
        }
    }
}
