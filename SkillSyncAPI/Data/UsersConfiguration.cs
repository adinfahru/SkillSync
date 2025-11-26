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

            //otp, IsUsed, Expired

            builder.Property(u => u.UserName).HasColumnName("username");

            builder.Property(u => u.Id).HasColumnName("id");

            builder.Property(u => u.RoleId).HasColumnName("role_id");

            builder.Property(u => u.Email).HasColumnName("email");

            builder.Property(u => u.Password).HasColumnName("password");

            builder.Property(u => u.CreatedAt).HasColumnName("created_at");

            builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");

            builder.Property(u => u.IsActive).HasColumnName("is_active");
        }
    }
}
