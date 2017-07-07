using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class AnswerDal : IAnswerDal
    {
        public Answer AddAnswer(string text, bool value, int questionId)
        {
            using (var context = new QuizEntities())
            {
                var answer = new Answer()
                {
                    AnswerText = text,
                    IsCorrect = value,
                    QuestionID = questionId
                };

                var rez = context.Answers.Add(answer);
                context.SaveChanges();

                return rez;
            }
        }
    }
}
