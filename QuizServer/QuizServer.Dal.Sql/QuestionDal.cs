using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class QuestionDal : IQuestionDal
    {
        public Question AddQueston(string text, int difficultyId, int categoryId, int typeId)
        {
            using (var context = new QuizEntities())
            {
                var question = new Question()
                {
                    QuestionText = text,
                    DifficultyId = difficultyId,
                    CategoryId = categoryId,
                    TypeId = typeId
                };

                var rez = context.Questions.Add(question);
                context.SaveChanges();

                return rez;
            }
        }

        public List<Question> GetAllQuestions()
        {
            using (var context = new QuizEntities())
            {
                return context.Questions.Include(c => c.Answers).ToList();
            }
        }
    }
}
