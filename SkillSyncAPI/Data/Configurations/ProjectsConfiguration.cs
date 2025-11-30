using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class ProjectsConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projects");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(200);

            builder
                .Property(p => p.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(ProjectStatus.Active);

            builder.Property(p => p.Description).HasColumnName("description").HasColumnType("text");

            builder
                .Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(p => p.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
