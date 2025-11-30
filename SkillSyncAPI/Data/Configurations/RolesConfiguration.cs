using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id");

            builder
                .Property(r => r.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.HasIndex(r => r.Name).IsUnique();
        }
    }
}
