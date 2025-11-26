using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class TalentProfilesConfiguration : IEntityTypeConfiguration<TalentProfiles>
    {
        public void Configure(EntityTypeBuilder<TalentProfiles> builder)
        {
            builder.ToTable("talent_profiles");

            builder.HasKey(tp => tp.UserId);

            builder.Property(tp => tp.UserId).HasColumnName("user_id");

            builder.Property(tp => tp.FirstName).HasColumnName("first_name");

            builder.Property(tp => tp.LastName).HasColumnName("last_name");

            builder.Property(tp => tp.Department).HasColumnName("department");

            builder.Property(tp => tp.AvailabilityStatus).HasColumnName("availability_status");

            builder.Property(tp => tp.Bio).HasColumnName("bio");

            builder.Property(tp => tp.CreatedAt).HasColumnName("created_at");

            builder.Property(tp => tp.UpdatedAt).HasColumnName("updated_at");
        }
    }
}
