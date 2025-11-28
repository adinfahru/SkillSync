using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id");

            builder.Property(u => u.RoleId).HasColumnName("role_id");

            builder
                .Property(u => u.UserName)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(255);

            builder.HasIndex(u => u.Email).IsUnique();

            builder
                .Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Otp).HasColumnName("Otp");

            builder.Property(u => u.Expired).HasColumnName("Expired");

            builder.Property(u => u.IsUsed).HasColumnName("IsUsed");

            builder
                .Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        }
    }
}
