using Microsoft.EntityFrameworkCore;
using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data
{
    public class SkillSyncDbContext : DbContext
    {
        public SkillSyncDbContext(DbContextOptions<SkillSyncDbContext> options)
            : base(options) { }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TalentProfile> TalentProfile { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<TalentSkill> TalentSkill { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<ProjectAssignment> ProjectAssignment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from Configuration files
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillSyncDbContext).Assembly);

            // 1-to-Many between Role and User
            modelBuilder
                .Entity<Role>()
                .HasMany(r => r.User)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-to-1 between User and TalentProfile
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.TalentProfile)
                .WithOne(tp => tp.User)
                .HasForeignKey<TalentProfile>(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between User and ProjectAssignments
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.ProjectAssignment)
                .WithOne(pa => pa.User)
                .HasForeignKey(pa => pa.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-to-Many between TalentProfile and TalentSkills
            modelBuilder
                .Entity<TalentProfile>()
                .HasMany(tp => tp.TalentSkill)
                .WithOne(ts => ts.TalentProfile)
                .HasForeignKey(ts => ts.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between Skills and TalentSkills
            modelBuilder
                .Entity<Skill>()
                .HasMany(s => s.TalentSkill)
                .WithOne(ts => ts.Skill)
                .HasForeignKey(ts => ts.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between Projects and ProjectAssignments
            modelBuilder
                .Entity<Project>()
                .HasMany(p => p.ProjectAssignment)
                .WithOne(pa => pa.Project)
                .HasForeignKey(pa => pa.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Database seeding
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed data using static seeder methods
            modelBuilder.Entity<Role>().HasData(SkillSyncDataSeeder.GetDefaultRoles());

            modelBuilder.Entity<Skill>().HasData(SkillSyncDataSeeder.GetDefaultSkills());

            modelBuilder.Entity<User>().HasData(SkillSyncDataSeeder.GetDefaultUser());

            modelBuilder
                .Entity<TalentProfile>()
                .HasData(SkillSyncDataSeeder.GetDefaultTalentProfiles());

            modelBuilder
                .Entity<TalentSkill>()
                .HasData(SkillSyncDataSeeder.GetDefaultTalentSkills());

            modelBuilder.Entity<Project>().HasData(SkillSyncDataSeeder.GetDefaultProjects());

            modelBuilder
                .Entity<ProjectAssignment>()
                .HasData(SkillSyncDataSeeder.GetDefaultProjectAssignments());
        }
    }
}
