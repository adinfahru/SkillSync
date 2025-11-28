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

    // Users
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

    public static List<Roles> GetDefaultRoles()
    {
        return new List<Roles>
        {
            new Roles { Id = AdminRoleId, Name = UserRole.Admin },
            new Roles { Id = HRRoleId, Name = UserRole.HR },
            new Roles { Id = ProjectManagerRoleId, Name = UserRole.ProjectManager },
            new Roles { Id = TalentRoleId, Name = UserRole.Talent },
        };
    }

    public static List<Skills> GetDefaultSkills()
    {
        return new List<Skills>
        {
            // Backend Development
            new Skills
            {
                Id = CSharpSkillId,
                Name = "C#",
                Category = "Backend Development",
            },
            new Skills
            {
                Id = AspNetCoreSkillId,
                Name = "ASP.NET Core",
                Category = "Backend Development",
            },
            new Skills
            {
                Id = EntityFrameworkSkillId,
                Name = "Entity Framework Core",
                Category = "Backend Development",
            },
            new Skills
            {
                Id = RESTfulAPISkillId,
                Name = "RESTful API",
                Category = "Backend Development",
            },
            // Frontend Development
            new Skills
            {
                Id = JavaScriptSkillId,
                Name = "JavaScript",
                Category = "Frontend Development",
            },
            new Skills
            {
                Id = ReactSkillId,
                Name = "React",
                Category = "Frontend Development",
            },
            new Skills
            {
                Id = TypeScriptSkillId,
                Name = "TypeScript",
                Category = "Frontend Development",
            },
            new Skills
            {
                Id = HtmlCssSkillId,
                Name = "HTML/CSS",
                Category = "Frontend Development",
            },
            // Database
            new Skills
            {
                Id = SqlServerSkillId,
                Name = "SQL Server",
                Category = "Database",
            },
            new Skills
            {
                Id = PostgreSqlSkillId,
                Name = "PostgreSQL",
                Category = "Database",
            },
            new Skills
            {
                Id = MongoDbSkillId,
                Name = "MongoDB",
                Category = "Database",
            },
            // DevOps
            new Skills
            {
                Id = DockerSkillId,
                Name = "Docker",
                Category = "DevOps",
            },
            new Skills
            {
                Id = AzureSkillId,
                Name = "Azure",
                Category = "DevOps",
            },
            new Skills
            {
                Id = CiCdSkillId,
                Name = "CI/CD",
                Category = "DevOps",
            },
        };
    }

    public static List<Users> GetDefaultUsers()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<Users>
        {
            // Admin User
            new Users
            {
                Id = AdminUserId,
                RoleId = AdminRoleId,
                UserName = "admin",
                Email = "admin@skillsync.com",
                Password = "$2a$11$N2oKjyGJN5e5FbZ3vHkdLO1KJxCvYuZx3pRqF8tVwYsOZx9HkQ.mG", // Admin123!
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            // HR User
            new Users
            {
                Id = HRUserId,
                RoleId = HRRoleId,
                UserName = "hr.manager",
                Email = "hr@skillsync.com",
                Password = "$2a$11$M1nJiyFIN4d4EaY2uGjcKN0JIwBuXtYw2oQpE7sUvXrNYx8GjP.lF", // HR123!
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            // Project Manager
            new Users
            {
                Id = PMUserId,
                RoleId = ProjectManagerRoleId,
                UserName = "pm.john",
                Email = "john.pm@skillsync.com",
                Password = "$2a$11$L0mHhxEHM3c3DaX1tFibJM9IHvAtWsXv1nPoD6rTuWqMXw7FiO.kE", // PM123!
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            // Talent Users
            new Users
            {
                Id = JohnDoeUserId,
                RoleId = TalentRoleId,
                UserName = "john.doe",
                Email = "john.doe@skillsync.com",
                Password = "$2a$11$K9lGgwDGL2b2CaW0sFhaIM8HGuAsVrWu0mOnC5qSsVpLWv6EhN.jD", // John123!
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            new Users
            {
                Id = JaneSmithUserId,
                RoleId = TalentRoleId,
                UserName = "jane.smith",
                Email = "jane.smith@skillsync.com",
                Password = "$2a$11$J8kFfvCFK1a1BaV9rEgaHL7GFtArUqVt9lNmB4pRrUoKVu5DgM.iC", // Jane123!
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
            new Users
            {
                Id = MikeWilsonUserId,
                RoleId = TalentRoleId,
                UserName = "mike.wilson",
                Email = "mike.wilson@skillsync.com",
                Password = "$2a$11$I7jEeuBEJ0Z0AaU8qDfaGK6FEsAqTpUs8kMlA3oQqTnJTt4CfL.hB", // Mike123!
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
            },
        };
    }

    public static List<TalentProfiles> GetDefaultTalentProfiles()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<TalentProfiles>
        {
            new TalentProfiles
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
            new TalentProfiles
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
            new TalentProfiles
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

    public static List<TalentSkills> GetDefaultTalentSkills()
    {
        return new List<TalentSkills>
        {
            // John Doe's skills
            new TalentSkills
            {
                UserId = JohnDoeUserId,
                SkillId = CSharpSkillId,
                Level = SkillLevel.Expert,
            },
            new TalentSkills
            {
                UserId = JohnDoeUserId,
                SkillId = AspNetCoreSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkills
            {
                UserId = JohnDoeUserId,
                SkillId = EntityFrameworkSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkills
            {
                UserId = JohnDoeUserId,
                SkillId = SqlServerSkillId,
                Level = SkillLevel.Advanced,
            },
            // Jane Smith's skills
            new TalentSkills
            {
                UserId = JaneSmithUserId,
                SkillId = CSharpSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkills
            {
                UserId = JaneSmithUserId,
                SkillId = AspNetCoreSkillId,
                Level = SkillLevel.Intermediate,
            },
            new TalentSkills
            {
                UserId = JaneSmithUserId,
                SkillId = JavaScriptSkillId,
                Level = SkillLevel.Expert,
            },
            new TalentSkills
            {
                UserId = JaneSmithUserId,
                SkillId = ReactSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkills
            {
                UserId = JaneSmithUserId,
                SkillId = TypeScriptSkillId,
                Level = SkillLevel.Advanced,
            },
            // Mike Wilson's skills
            new TalentSkills
            {
                UserId = MikeWilsonUserId,
                SkillId = DockerSkillId,
                Level = SkillLevel.Expert,
            },
            new TalentSkills
            {
                UserId = MikeWilsonUserId,
                SkillId = AzureSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkills
            {
                UserId = MikeWilsonUserId,
                SkillId = CiCdSkillId,
                Level = SkillLevel.Advanced,
            },
            new TalentSkills
            {
                UserId = MikeWilsonUserId,
                SkillId = PostgreSqlSkillId,
                Level = SkillLevel.Intermediate,
            },
        };
    }

    public static List<Projects> GetDefaultProjects()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<Projects>
        {
            new Projects
            {
                Id = ECommerceProjectId,
                Name = "E-Commerce Platform",
                Status = ProjectStatus.Active,
                Description =
                    "Building a modern e-commerce platform with .NET Core backend and React frontend. Features include user authentication, product catalog, shopping cart, and payment integration.",
                CreatedAt = now,
                UpdatedAt = now,
            },
            new Projects
            {
                Id = CrmProjectId,
                Name = "Customer Management System",
                Status = ProjectStatus.OnHold,
                Description =
                    "CRM system to manage customer relationships, track sales, and generate reports. Built with ASP.NET Core Web API and Angular frontend.",
                CreatedAt = now,
                UpdatedAt = now,
            },
            new Projects
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

    public static List<ProjectAssignments> GetDefaultProjectAssignments()
    {
        var now = new DateTime(2025, 11, 28, 0, 0, 0, DateTimeKind.Utc);

        return new List<ProjectAssignments>
        {
            // E-Commerce Platform assignments
            new ProjectAssignments
            {
                Id = new Guid("50000000-0000-0000-0000-000000000001"),
                ProjectId = ECommerceProjectId,
                UserId = JohnDoeUserId,
                RoleOnProject = "Senior Backend Developer",
                Status = ProjectAssignmentStatus.Active,
                AssignedAt = now.AddDays(-15),
            },
            new ProjectAssignments
            {
                Id = new Guid("50000000-0000-0000-0000-000000000002"),
                ProjectId = ECommerceProjectId,
                UserId = JaneSmithUserId,
                RoleOnProject = "Full Stack Developer",
                Status = ProjectAssignmentStatus.Active,
                AssignedAt = now.AddDays(-12),
            },
            // Customer Management System assignments
            new ProjectAssignments
            {
                Id = new Guid("50000000-0000-0000-0000-000000000003"),
                ProjectId = CrmProjectId,
                UserId = JaneSmithUserId,
                RoleOnProject = "Frontend Developer",
                Status = ProjectAssignmentStatus.Inactive,
                AssignedAt = now.AddDays(-45),
                CompletedAt = now.AddDays(-20),
            },
            // Mobile Banking App assignments (completed)
            new ProjectAssignments
            {
                Id = new Guid("50000000-0000-0000-0000-000000000004"),
                ProjectId = MobileBankingProjectId,
                UserId = JohnDoeUserId,
                RoleOnProject = "Tech Lead",
                Status = ProjectAssignmentStatus.Completed,
                AssignedAt = now.AddDays(-60),
                CompletedAt = now.AddDays(-5),
            },
            new ProjectAssignments
            {
                Id = new Guid("50000000-0000-0000-0000-000000000005"),
                ProjectId = MobileBankingProjectId,
                UserId = MikeWilsonUserId,
                RoleOnProject = "DevOps Engineer",
                Status = ProjectAssignmentStatus.Completed,
                AssignedAt = now.AddDays(-50),
                CompletedAt = now.AddDays(-5),
            },
        };
    }
}
