using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Model.Models
{
    public class QuestionDataModel
    {
        public string Text { get; set; }
        public int DifficultyID { get; set; }
        public int SubjectId { get; set; }
        public int TypeId { get; set; }
        public List<AnswerDataModel> Answers { get; set; }
    }
}
