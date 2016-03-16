using Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Mapping
{
    public class VotedFormMap : EntityTypeConfiguration<VotedForms>
    {
        public VotedFormMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UserID, t.FormID });

            // Properties
            this.Property(t => t.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FormID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VotedForms");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.FormID).HasColumnName("FormID");
        }
    }
}
