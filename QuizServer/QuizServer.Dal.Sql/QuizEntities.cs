using System.Data.Entity;
using QuizServer.Model.Dtos;

namespace QuizServer.Dal.Sql
{
    public partial class QuizEntities : DbContext
    {
        public QuizEntities()
            : base("name=QuizEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .HasMany(e => e.UserAnswers)
                .WithRequired(e => e.Answer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.UserAnswers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Tests)
                .WithMany(e => e.Questions)
                .Map(m => m.ToTable("TestsWithQuestions").MapLeftKey("QuestionID").MapRightKey("TestID"));

            modelBuilder.Entity<QuestionType>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.QuestionType)
                .HasForeignKey(e => e.TypeId);
        }
    }
}
