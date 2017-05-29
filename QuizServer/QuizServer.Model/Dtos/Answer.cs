

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizServer.Model.Dtos
{
   
    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionID { get; set; }

        public virtual Question Question { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
