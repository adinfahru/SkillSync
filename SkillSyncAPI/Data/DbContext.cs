using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data
{
    public class SkillSyncDbContext : DbContext
    {
        public SkillSyncDbContext(DbContextOptions<SkillSyncDbContext> options)
            : base(options) { }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<TalentProfiles> TalentProfiles { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<TalentSkills> TalentSkills { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectAssignments> ProjectAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillSyncDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new Configurations.RolesConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UsersConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TalentProfilesConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SkillsConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TalentSkillsConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProjectsConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProjectAssignmentsConfiguration());

            // 1-to-1 between Users and TalentProfiles
            modelBuilder
                .Entity<Users>()
                .HasOne(u => u.TalentProfile)
                .WithOne(u => u.User)
                .HasForeignKey<TalentProfiles>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between Users and Roles
            modelBuilder
                .Entity<Users>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-1 between Roles and Users
            modelBuilder
                .Entity<Roles>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
