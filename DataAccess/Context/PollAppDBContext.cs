using System.Data.Entity;
using DataAccess.Mapping;
using Entities;

namespace DataAccess.Context
{
    public partial class PollAppDBContext : DbContext
    {
        static PollAppDBContext()
        {
            Database.SetInitializer<PollAppDBContext>(null);
        }

        public PollAppDBContext()
            : base("Name=PollAppDBContext")
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VotedForms> VotedForms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnswerMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new FormMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new VotedFormMap());
        }
    }
}
