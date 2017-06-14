using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizServer.Model.Models
{
    public class UserDataModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string CurrentTest { get; set; }
        public int FinishedTests { get; set; }
    }
}
