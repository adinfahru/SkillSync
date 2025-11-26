using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class ProjectAssignmentsConfiguration : IEntityTypeConfiguration<ProjectAssignments>
    {
        public void Configure(EntityTypeBuilder<ProjectAssignments> builder)
        {
            builder.ToTable("project_assignments");

            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.Id).HasColumnName("id");

            builder.Property(pa => pa.ProjectId).HasColumnName("project_id");

            builder.Property(pa => pa.UserId).HasColumnName("user_id");

            builder.Property(pa => pa.RoleOnProject).HasColumnName("role_on_project").IsRequired();

            builder
                .Property(pa => pa.Status)
                .HasColumnName("status")
                .HasDefaultValue(ProjectAssignmentStatus.Active);

            builder.Property(pa => pa.AssignedAt).HasColumnName("assigned_at");

            builder.Property(pa => pa.CompletedAt).HasColumnName("completed_at");
        }
    }
}
