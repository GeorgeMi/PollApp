using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Entities;

namespace DataAccess.Mapping
{
    public class AnswerMap : EntityTypeConfiguration<Answer>
    {
        public AnswerMap()
        {
            // Primary Key
            this.HasKey(t => t.AnswerID);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Answer");
            this.Property(t => t.AnswerID).HasColumnName("AnswerID");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.NrVotes).HasColumnName("NrVotes");

            // Relationships
            this.HasRequired(t => t.Question)
                .WithMany(t => t.Answers)
                .HasForeignKey(d => d.QuestionID);

        }
    }
}
