using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class ProjectAssignmentsConfiguration : IEntityTypeConfiguration<ProjectAssignment>
    {
        public void Configure(EntityTypeBuilder<ProjectAssignment> builder)
        {
            builder.ToTable("project_assignments");

            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Id).HasColumnName("id");

            builder.Property(pa => pa.ProjectId).HasColumnName("project_id");

            builder.Property(pa => pa.UserId).HasColumnName("user_id");

            builder
                .Property(pa => pa.RoleOnProject)
                .HasColumnName("role_on_project")
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(pa => pa.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(ProjectAssignmentStatus.Active);

            builder
                .Property(pa => pa.AssignedAt)
                .HasColumnName("assigned_at")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pa => pa.CompletedAt).HasColumnName("completed_at");
        }
    }
}
