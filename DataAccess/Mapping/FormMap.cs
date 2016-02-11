using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Entities;

namespace DataAccess.Mapping
{
    public class FormMap : EntityTypeConfiguration<Form>
    {
        public FormMap()
        {
            // Primary Key
            this.HasKey(t => t.FormID);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.State)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Form");
            this.Property(t => t.FormID).HasColumnName("FormID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Deadline).HasColumnName("Deadline");

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Forms)
                .HasForeignKey(d => d.CategoryID);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Forms)
                .HasForeignKey(d => d.UserID);

        }
    }
}
