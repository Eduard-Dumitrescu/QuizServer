using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public class QuestionTypeDal : IQuestionTypeDal
    {
        public List<QuestionType> GetAllQuestionTypes()
        {
            using (var context = new QuizEntities())
            {
                return context.QuestionTypes.ToList();
            }
        }
    }
}
