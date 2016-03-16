using System.Data.Entity.ModelConfiguration;
using Entities;

namespace DataAccess.Mapping
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            // Primary Key
            this.HasKey(t => t.TokenID);

            // Properties
            this.Property(t => t.TokenString)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Token");
            this.Property(t => t.TokenID).HasColumnName("TokenID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.TokenString).HasColumnName("TokenString");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ExpirationDate).HasColumnName("ExpirationDate");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Tokens)
                .HasForeignKey(d => d.UserID);

        }
    }
}
