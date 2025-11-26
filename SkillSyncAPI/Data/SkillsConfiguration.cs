using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class SkillsConfiguration : IEntityTypeConfiguration<Skills>
    {
        public void Configure(EntityTypeBuilder<Skills> builder)
        {
            builder.ToTable("skills");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.Name).HasColumnName("name");

            builder.Property(s => s.Category).HasColumnName("category");
        }
    }
}
