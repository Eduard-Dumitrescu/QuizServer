using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizServer.Model.Dtos
{
    public partial class Exam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exam()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }

        public int TestID { get; set; }

        public Guid UserID { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [StringLength(10)]
        public string Grade { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
