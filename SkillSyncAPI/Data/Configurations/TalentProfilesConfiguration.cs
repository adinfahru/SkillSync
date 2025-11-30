using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class TalentProfilesConfiguration : IEntityTypeConfiguration<TalentProfile>
    {
        public void Configure(EntityTypeBuilder<TalentProfile> builder)
        {
            builder.ToTable("talent_profiles");

            builder.HasKey(tp => tp.UserId);

            builder.Property(tp => tp.UserId).HasColumnName("user_id");

            builder
                .Property(tp => tp.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(tp => tp.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(tp => tp.Department).HasColumnName("department").HasMaxLength(100);

            builder
                .Property(tp => tp.AvailabilityStatus)
                .HasColumnName("availability_status")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(TalentStatus.Available);

            builder.Property(tp => tp.Bio).HasColumnName("bio").HasColumnType("text");

            builder
                .Property(tp => tp.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(tp => tp.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
