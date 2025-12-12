using SkillSyncAPI.Models;

namespace SkillSyncAPI.Data;

public static class SkillSyncDataSeeder
{
    // Static readonly GUIDs for consistent data

    // Roles
    public static readonly Guid AdminRoleId = new Guid("10000000-0000-0000-0000-000000000001");
    public static readonly Guid HRRoleId = new Guid("10000000-0000-0000-0000-000000000002");
    public static readonly Guid ProjectManagerRoleId = new Guid(
        "10000000-0000-0000-0000-000000000003"
    );
    public static readonly Guid TalentRoleId = new Guid("10000000-0000-0000-0000-000000000004");

    // Skills
    public static readonly Guid CSharpSkillId = new Guid("20000000-0000-0000-0000-000000000001");
    public static readonly Guid AspNetCoreSkillId = new Guid(
        "20000000-0000-0000-0000-000000000002"
    );
    public static readonly Guid EntityFrameworkSkillId = new Guid(
        "20000000-0000-0000-0000-000000000003"
    );
    public static readonly Guid RESTfulAPISkillId = new Guid(
        "20000000-0000-0000-0000-000000000004"
    );
    public static readonly Guid JavaScriptSkillId = new Guid(
        "20000000-0000-0000-0000-000000000005"
    );
    public static readonly Guid ReactSkillId = new Guid("20000000-0000-0000-0000-000000000006");
    public static readonly Guid TypeScriptSkillId = new Guid(
        "20000000-0000-0000-0000-000000000007"
    );
    public static readonly Guid HtmlCssSkillId = new Guid("20000000-0000-0000-0000-000000000008");
    public static readonly Guid SqlServerSkillId = new Guid("20000000-0000-0000-0000-000000000009");
    public static readonly Guid PostgreSqlSkillId = new Guid(
        "20000000-0000-0000-0000-000000000010"
    );
    public static readonly Guid MongoDbSkillId = new Guid("20000000-0000-0000-0000-000000000011");
    public static readonly Guid DockerSkillId = new Guid("20000000-0000-0000-0000-000000000012");
    public static readonly Guid AzureSkillId = new Guid("20000000-0000-0000-0000-000000000013");
    public static readonly Guid CiCdSkillId = new Guid("20000000-0000-0000-0000-000000000014");

    // User
    public static readonly Guid AdminUserId = new Guid("30000000-0000-0000-0000-000000000001");
    public static readonly Guid HRUserId = new Guid("30000000-0000-0000-0000-000000000002");
    public static readonly Guid PMUserId = new Guid("30000000-0000-0000-0000-000000000003");
    public static readonly Guid JohnDoeUserId = new Guid("30000000-0000-0000-0000-000000000004");
    public static readonly Guid JaneSmithUserId = new Guid("30000000-0000-0000-0000-000000000005");
    public static readonly Guid MikeWilsonUserId = new Guid("30000000-0000-0000-0000-000000000006");

    // Projects
    public static readonly Guid ECommerceProjectId = new Guid(
        "40000000-0000-0000-0000-000000000001"
    );
    public static readonly Guid CrmProjectId = new Guid("40000000-0000-0000-0000-000000000002");
    public static readonly Guid MobileBankingProjectId = new Guid(
        "40000000-0000-0000-0000-000000000003"
    );

    public static List<Role> GetDefaultRoles()
    {
        return new List<Role>
        {
            new Role { Id = AdminRoleId, Name = UserRole.Admin },
            new Role { Id = HRRoleId, Name = UserRole.HR },
            new Role { Id = ProjectManagerRoleId, Name = UserRole.ProjectManager },
            new Role { Id = TalentRoleId, Name = UserRole.Talent },
        };
    }

    public static List<Skill> GetDefaultSkills()
    {
        return new List<Skill>
        {
            // Backend Development
            new Skill
            {
                Id = CSharpSkillId,
                Name = "C#",
                Category = "Backend Development",
            },
            new Skill
            {
                Id = AspNetCoreSkillId,
                Name = "ASP.NET Core",
                Category = "Backend Development",
            },
            new Skill
            {
                Id = EntityFrameworkSkillId,
                Name = "Entity Framework Core",
                Category = "Backend Development",
            },
            new Skill
            {
                Id = RESTfulAPISkillId,
                Name = "RESTful API",
                Category = "Backend Development",
            },
            // Frontend Development
            new Skill
            {
                Id = JavaScriptSkillId,
                Name = "JavaScript",
                Category = "Frontend Development",
            },
            new Skill
            {
                Id = ReactSkillId,
                Name = "React",
                Category = "Frontend Development",
            },
            new Skill
            {
                Id = TypeScriptSkillId,
                Name = "TypeScript",
                Category = "Frontend Development",
            },
            new Skill
            {
                Id = HtmlCssSkillId,
                Name = "HTML/CSS",
                Category = "Frontend Development",
            },
            // Database
            new Skill
            {
                Id = SqlServerSkillId,
                Name = "SQL Server",
                Category = "Database",
            },
            new Skill
            {
                Id = PostgreSqlSkillId,
                Name = "PostgreSQL",
                Category = "Database",
            },
            new Skill
            {
                Id = MongoDbSkillId,
                Name = "MongoDB",
                Category = "Database",
            },
            // DevOps
            new Skill
            {
                Id = DockerSkillId,
                Name = "Docker",
                Category = "DevOps",
            },
            new Skill
            {
                Id = AzureSkillId,
                Name = "Azure",
                Category = "DevOps",
            },
            new Skill
            {
                Id = CiCdSkillId,
                Name = "CI/CD",
                Category = "DevOps",
            },
        };
    }

    public static List<User> GetDefaultUser()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);
        // Password: 12345678 (hashed with BCrypt cost factor 11)
        var hashedPassword = "$2a$11$FVpzZRbJEI5eZgpIq59.FOfLEYYsM3dYyTz93FfGKBr6OQFQvDlhS";

        return new List<User>
        {
            // Admin User
            new User
            {
                Id = AdminUserId,
                RoleId = AdminRoleId,
                UserName = "admin",
                Email = "admin@skillsync.com",
                Password = hashedPassword,
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            // HR User
            new User
            {
                Id = HRUserId,
                RoleId = HRRoleId,
                UserName = "hr.manager",
                Email = "hr@skillsync.com",
                Password = hashedPassword,
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            // Project Manager
            new User
            {
                Id = PMUserId,
                RoleId = ProjectManagerRoleId,
                UserName = "pm.john",
                Email = "john.pm@skillsync.com",
                Password = hashedPassword,
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            // Talent User
            new User
            {
                Id = JohnDoeUserId,
                RoleId = TalentRoleId,
                UserName = "john.doe",
                Email = "john.doe@skillsync.com",
                Password = hashedPassword,
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            new User
            {
                Id = JaneSmithUserId,
                RoleId = TalentRoleId,
                UserName = "jane.smith",
                Email = "jane.smith@skillsync.com",
                Password = hashedPassword,
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            new User
            {
                Id = MikeWilsonUserId,
                RoleId = TalentRoleId,
                UserName = "mike.wilson",
                Email = "mike.wilson@skillsync.com",
                Password = hashedPassword,
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
        };
    }

    public static List<TalentProfile> GetDefaultTalentProfiles()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<TalentProfile>
        {
            new TalentProfile
            {
                UserId = JohnDoeUserId,
                FirstName = "John",
                LastName = "Doe",
                Department = "Engineering",
                AvailabilityStatus = TalentStatus.Available,
                Bio =
                    "Senior .NET developer with 5+ years of experience in building scalable web applications. Expertise in ASP.NET Core, Entity Framework, and cloud technologies.",
                CreatedAt = now,
                UpdatedAt = now,
            },
            new TalentProfile
            {
                UserId = JaneSmithUserId,
                FirstName = "Jane",
                LastName = "Smith",
                Department = "Engineering",
                AvailabilityStatus = TalentStatus.Available,
                Bio =
                    "Full-stack developer specializing in React and .NET technologies. Strong background in database design and API development.",
                CreatedAt = now,
                UpdatedAt = now,
            },
            new TalentProfile
            {
                UserId = MikeWilsonUserId,
                FirstName = "Mike",
                LastName = "Wilson",
                Department = "DevOps",
                AvailabilityStatus = TalentStatus.OnLeave,
                Bio =
                    "DevOps engineer with expertise in containerization, CI/CD pipelines, and cloud infrastructure management.",
                CreatedAt = now,
                UpdatedAt = now,
            },
        };
    }

    public static List<TalentSkill> GetDefaultTalentSkills()
    {
        return new List<TalentSkill>
        {
            // John Doe's skills
            new TalentSkill
            {
                UserId = JohnDoeUserId,
                SkillId = CSharpSkillId,
                Level = SkillLevel.Expert,
            },
            new TalentSkill
            {
                UserId = JohnDoeUserId,
                SkillId = AspNetCoreSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkill
            {
                UserId = JohnDoeUserId,
                SkillId = EntityFrameworkSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkill
            {
                UserId = JohnDoeUserId,
                SkillId = SqlServerSkillId,
                Level = SkillLevel.Advanced,
            },
            // Jane Smith's skills
            new TalentSkill
            {
                UserId = JaneSmithUserId,
                SkillId = CSharpSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkill
            {
                UserId = JaneSmithUserId,
                SkillId = AspNetCoreSkillId,
                Level = SkillLevel.Intermediate,
            },
            new TalentSkill
            {
                UserId = JaneSmithUserId,
                SkillId = JavaScriptSkillId,
                Level = SkillLevel.Expert,
            },
            new TalentSkill
            {
                UserId = JaneSmithUserId,
                SkillId = ReactSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkill
            {
                UserId = JaneSmithUserId,
                SkillId = TypeScriptSkillId,
                Level = SkillLevel.Advanced,
            },
            // Mike Wilson's skills
            new TalentSkill
            {
                UserId = MikeWilsonUserId,
                SkillId = DockerSkillId,
                Level = SkillLevel.Expert,
            },
            new TalentSkill
            {
                UserId = MikeWilsonUserId,
                SkillId = AzureSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkill
            {
                UserId = MikeWilsonUserId,
                SkillId = CiCdSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkill
            {
                UserId = MikeWilsonUserId,
                SkillId = PostgreSqlSkillId,
                Level = SkillLevel.Intermediate,
            },
        };
    }

    public static List<Project> GetDefaultProjects()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<Project>
        {
            new Project
            {
                Id = ECommerceProjectId,
                Name = "E-Commerce Platform",
                Status = ProjectStatus.Active,
                Description =
                    "Building a modern e-commerce platform with .NET Core backend and React frontend. Features include user authentication, product catalog, shopping cart, and payment integration.",
                CreatedAt = now,
                UpdatedAt = now,
            },
            new Project
            {
                Id = CrmProjectId,
                Name = "Customer Management System",
                Status = ProjectStatus.OnHold,
                Description =
                    "CRM system to manage customer relationships, track sales, and generate reports. Built with ASP.NET Core Web API and Angular frontend.",
                CreatedAt = now,
                UpdatedAt = now,
            },
            new Project
            {
                Id = MobileBankingProjectId,
                Name = "Mobile Banking App",
                Status = ProjectStatus.Completed,
                Description =
                    "Secure mobile banking application with features like account management, fund transfers, bill payments, and transaction history.",
                CreatedAt = now.AddDays(-30),
                UpdatedAt = now.AddDays(-5),
            },
        };
    }

    public static List<ProjectAssignment> GetDefaultProjectAssignments()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<ProjectAssignment>
        {
            // E-Commerce Platform assignments
            new ProjectAssignment
            {
                ProjectId = ECommerceProjectId,
                UserId = JohnDoeUserId,
                RoleOnProject = "Senior Backend Developer",
                AssignedAt = now.AddDays(-15),
            },
            new ProjectAssignment
            {
                ProjectId = ECommerceProjectId,
                UserId = JaneSmithUserId,
                RoleOnProject = "Full Stack Developer",
                AssignedAt = now.AddDays(-12),
            },
            // Customer Management System assignments
            new ProjectAssignment
            {
                ProjectId = CrmProjectId,
                UserId = JaneSmithUserId,
                RoleOnProject = "Frontend Developer",
                AssignedAt = now.AddDays(-45),
                CompletedAt = now.AddDays(-20),
            },
            // Mobile Banking App assignments (completed)
            new ProjectAssignment
            {
                ProjectId = MobileBankingProjectId,
                UserId = JohnDoeUserId,
                RoleOnProject = "Tech Lead",
                AssignedAt = now.AddDays(-60),
                CompletedAt = now.AddDays(-5),
            },
            new ProjectAssignment
            {
                ProjectId = MobileBankingProjectId,
                UserId = MikeWilsonUserId,
                RoleOnProject = "DevOps Engineer",
                AssignedAt = now.AddDays(-50),
                CompletedAt = now.AddDays(-5),
            },
        };
    }
}
