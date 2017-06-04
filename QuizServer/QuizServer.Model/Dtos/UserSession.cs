using System;

namespace QuizServer.Model.Dtos
{
    public partial class UserSession
    {
        public Guid Id { get; set; }

        public DateTime LoggedIn { get; set; }

        public DateTime? LoggedOut { get; set; }

        public bool IsValid { get; set; }

        public Guid UserID { get; set; }

        public virtual User User { get; set; }
    }
}
