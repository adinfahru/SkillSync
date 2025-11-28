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

            // Apply all configurations from Configuration files
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkillSyncDbContext).Assembly);

            // 1-to-Many between Roles and Users
            modelBuilder
                .Entity<Roles>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-to-1 between Users and TalentProfiles
            modelBuilder
                .Entity<Users>()
                .HasOne(u => u.TalentProfile)
                .WithOne(tp => tp.User)
                .HasForeignKey<TalentProfiles>(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between Users and ProjectAssignments
            modelBuilder
                .Entity<Users>()
                .HasMany(u => u.ProjectAssignments)
                .WithOne(pa => pa.User)
                .HasForeignKey(pa => pa.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1-to-Many between TalentProfiles and TalentSkills
            modelBuilder
                .Entity<TalentProfiles>()
                .HasMany(tp => tp.TalentSkills)
                .WithOne(ts => ts.TalentProfile)
                .HasForeignKey(ts => ts.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between Skills and TalentSkills
            modelBuilder
                .Entity<Skills>()
                .HasMany(s => s.TalentSkills)
                .WithOne(ts => ts.Skill)
                .HasForeignKey(ts => ts.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many between Projects and ProjectAssignments
            modelBuilder
                .Entity<Projects>()
                .HasMany(p => p.ProjectAssignments)
                .WithOne(pa => pa.Project)
                .HasForeignKey(pa => pa.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Database seeding
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed data using static seeder methods
            modelBuilder.Entity<Roles>().HasData(SkillSyncDataSeeder.GetDefaultRoles());

            modelBuilder.Entity<Skills>().HasData(SkillSyncDataSeeder.GetDefaultSkills());

            modelBuilder.Entity<Users>().HasData(SkillSyncDataSeeder.GetDefaultUsers());

            modelBuilder
                .Entity<TalentProfiles>()
                .HasData(SkillSyncDataSeeder.GetDefaultTalentProfiles());

            modelBuilder
                .Entity<TalentSkills>()
                .HasData(SkillSyncDataSeeder.GetDefaultTalentSkills());

            modelBuilder.Entity<Projects>().HasData(SkillSyncDataSeeder.GetDefaultProjects());

            modelBuilder
                .Entity<ProjectAssignments>()
                .HasData(SkillSyncDataSeeder.GetDefaultProjectAssignments());
        }
    }
}
