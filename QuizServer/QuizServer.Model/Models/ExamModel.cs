using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Model.Models
{
    public class ExamModel
    {
        public Guid UserId { get; set; }
        public int TestId { get; set; }
    }
}
