using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Model.Models
{
    public class TestDataModel
    {
        public string TestName { get; set; }
        public int TestLevelId { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<SubjectDataModel> SubjectList { get; set; }
    }
}
