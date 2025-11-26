using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class ProjectsConfiguration : IEntityTypeConfiguration<Projects>
    {
        public void Configure(EntityTypeBuilder<Projects> builder)
        {
            builder.ToTable("projects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.Name).HasColumnName("name");

            builder.Property(p => p.Status).HasColumnName("status");

            builder.Property(p => p.Description).HasColumnName("description");

            builder.Property(p => p.CreatedAt).HasColumnName("created_at");

            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
