using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(r => r.Name).HasColumnName("name");
        }
    }
}
