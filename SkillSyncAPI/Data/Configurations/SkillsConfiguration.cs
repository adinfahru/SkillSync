using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class SkillsConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("skills");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasColumnName("id");

            builder.Property(s => s.Name).HasColumnName("name").IsRequired().HasMaxLength(100);

            builder.HasIndex(s => s.Name).IsUnique();

            builder
                .Property(s => s.Category)
                .HasColumnName("category")
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
