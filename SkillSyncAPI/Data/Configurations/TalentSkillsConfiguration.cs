using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class TalentSkillsConfiguration : IEntityTypeConfiguration<TalentSkill>
    {
        public void Configure(EntityTypeBuilder<TalentSkill> builder)
        {
            builder.ToTable("talent_skills");

            // Composite Primary Key
            builder.HasKey(ts => new { ts.UserId, ts.SkillId });

            builder.Property(ts => ts.UserId).HasColumnName("user_id");

            builder.Property(ts => ts.SkillId).HasColumnName("skill_id");

            builder
                .Property(ts => ts.Level)
                .HasColumnName("level")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}
