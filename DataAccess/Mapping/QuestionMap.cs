using System.Data.Entity.ModelConfiguration;
using Entities;

namespace DataAccess.Mapping
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.QuestionID);

            // Properties
            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Question");
            this.Property(t => t.QuestionID).HasColumnName("QuestionID");
            this.Property(t => t.FormID).HasColumnName("FormID");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.NrVotes).HasColumnName("NrVotes");

            // Relationships
            this.HasRequired(t => t.Form)
                .WithMany(t => t.Questions)
                .HasForeignKey(d => d.FormID);

        }
    }
}
