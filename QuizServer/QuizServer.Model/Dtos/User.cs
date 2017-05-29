using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizServer.Model.Dtos
{

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Exams = new HashSet<Exam>();
            UserSessions = new HashSet<UserSession>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(320)]
        public string Email { get; set; }

        [Required]
        [StringLength(130)]
        public string Password { get; set; }

        public Guid Salt { get; set; }

        public bool IsAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exam> Exams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSession> UserSessions { get; set; }
    }
}
