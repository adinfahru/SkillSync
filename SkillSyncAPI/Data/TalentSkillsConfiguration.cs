using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class TalentSkillsConfiguration : IEntityTypeConfiguration<TalentSkills>
    {
        public void Configure(EntityTypeBuilder<TalentSkills> builder)
        {
            builder.ToTable("talent_skills");

            builder.HasKey(ts => new { ts.UserId, ts.SkillId });

            builder.Property(ts => ts.UserId).HasColumnName("user_id");

            builder.Property(ts => ts.SkillId).HasColumnName("skill_id");

            builder.Property(ts => ts.Level).HasColumnName("level");
        }
    }
}
