using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillSyncAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Otp", table: "users", newName: "otp");

            migrationBuilder.RenameColumn(name: "Expired", table: "users", newName: "expired");

            migrationBuilder.RenameColumn(name: "IsUsed", table: "users", newName: "is_used");

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "id", "created_at", "description", "name", "updated_at" },
                values: new object[]
                {
                    new Guid("40000000-0000-0000-0000-000000000001"),
                    new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    "Building a modern e-commerce platform with .NET Core backend and React frontend. Features include user authentication, product catalog, shopping cart, and payment integration.",
                    "E-Commerce Platform",
                    new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                }
            );

            migrationBuilder.InsertData(
                table: "projects",
                columns: new[]
                {
                    "id",
                    "created_at",
                    "description",
                    "name",
                    "status",
                    "updated_at",
                },
                values: new object[,]
                {
                    {
                        new Guid("40000000-0000-0000-0000-000000000002"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "CRM system to manage customer relationships, track sales, and generate reports. Built with ASP.NET Core Web API and Angular frontend.",
                        "Customer Management System",
                        "OnHold",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    {
                        new Guid("40000000-0000-0000-0000-000000000003"),
                        new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Utc),
                        "Secure mobile banking application with features like account management, fund transfers, bill payments, and transaction history.",
                        "Mobile Banking App",
                        "Completed",
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                }
            );

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Admin" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "HR" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "ProjectManager" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Talent" },
                }
            );

            migrationBuilder.InsertData(
                table: "skills",
                columns: new[] { "id", "category", "name" },
                values: new object[,]
                {
                    {
                        new Guid("20000000-0000-0000-0000-000000000001"),
                        "Backend Development",
                        "C#",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000002"),
                        "Backend Development",
                        "ASP.NET Core",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000003"),
                        "Backend Development",
                        "Entity Framework Core",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000004"),
                        "Backend Development",
                        "RESTful API",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000005"),
                        "Frontend Development",
                        "JavaScript",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000006"),
                        "Frontend Development",
                        "React",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000007"),
                        "Frontend Development",
                        "TypeScript",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000008"),
                        "Frontend Development",
                        "HTML/CSS",
                    },
                    { new Guid("20000000-0000-0000-0000-000000000009"), "Database", "SQL Server" },
                    { new Guid("20000000-0000-0000-0000-000000000010"), "Database", "PostgreSQL" },
                    { new Guid("20000000-0000-0000-0000-000000000011"), "Database", "MongoDB" },
                    { new Guid("20000000-0000-0000-0000-000000000012"), "DevOps", "Docker" },
                    { new Guid("20000000-0000-0000-0000-000000000013"), "DevOps", "Azure" },
                    { new Guid("20000000-0000-0000-0000-000000000014"), "DevOps", "CI/CD" },
                }
            );

            migrationBuilder.InsertData(
                table: "users",
                columns: new[]
                {
                    "id",
                    "created_at",
                    "email",
                    "expired",
                    "is_active",
                    "is_used",
                    "otp",
                    "password",
                    "role_id",
                    "updated_at",
                    "username",
                },
                values: new object[,]
                {
                    {
                        new Guid("30000000-0000-0000-0000-000000000001"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "admin@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$N2oKjyGJN5e5FbZ3vHkdLO1KJxCvYuZx3pRqF8tVwYsOZx9HkQ.mG",
                        new Guid("10000000-0000-0000-0000-000000000001"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "admin",
                    },
                    {
                        new Guid("30000000-0000-0000-0000-000000000002"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "hr@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$M1nJiyFIN4d4EaY2uGjcKN0JIwBuXtYw2oQpE7sUvXrNYx8GjP.lF",
                        new Guid("10000000-0000-0000-0000-000000000002"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "hr.manager",
                    },
                    {
                        new Guid("30000000-0000-0000-0000-000000000003"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "john.pm@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$L0mHhxEHM3c3DaX1tFibJM9IHvAtWsXv1nPoD6rTuWqMXw7FiO.kE",
                        new Guid("10000000-0000-0000-0000-000000000003"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "pm.john",
                    },
                    {
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "john.doe@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$K9lGgwDGL2b2CaW0sFhaIM8HGuAsVrWu0mOnC5qSsVpLWv6EhN.jD",
                        new Guid("10000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "john.doe",
                    },
                    {
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "jane.smith@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$J8kFfvCFK1a1BaV9rEgaHL7GFtArUqVt9lNmB4pRrUoKVu5DgM.iC",
                        new Guid("10000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "jane.smith",
                    },
                    {
                        new Guid("30000000-0000-0000-0000-000000000006"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "mike.wilson@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$I7jEeuBEJ0Z0AaU8qDfaGK6FEsAqTpUs8kMlA3oQqTnJTt4CfL.hB",
                        new Guid("10000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "mike.wilson",
                    },
                }
            );

            migrationBuilder.InsertData(
                table: "project_assignments",
                columns: new[]
                {
                    "id",
                    "assigned_at",
                    "completed_at",
                    "project_id",
                    "role_on_project",
                    "user_id",
                },
                values: new object[,]
                {
                    {
                        new Guid("50000000-0000-0000-0000-000000000001"),
                        new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        "Senior Backend Developer",
                        new Guid("30000000-0000-0000-0000-000000000004"),
                    },
                    {
                        new Guid("50000000-0000-0000-0000-000000000002"),
                        new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        "Full Stack Developer",
                        new Guid("30000000-0000-0000-0000-000000000005"),
                    },
                }
            );

            migrationBuilder.InsertData(
                table: "project_assignments",
                columns: new[]
                {
                    "id",
                    "assigned_at",
                    "completed_at",
                    "project_id",
                    "role_on_project",
                    "status",
                    "user_id",
                },
                values: new object[,]
                {
                    {
                        new Guid("50000000-0000-0000-0000-000000000003"),
                        new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc),
                        new Guid("40000000-0000-0000-0000-000000000002"),
                        "Frontend Developer",
                        "Inactive",
                        new Guid("30000000-0000-0000-0000-000000000005"),
                    },
                    {
                        new Guid("50000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                        new Guid("40000000-0000-0000-0000-000000000003"),
                        "Tech Lead",
                        "Completed",
                        new Guid("30000000-0000-0000-0000-000000000004"),
                    },
                    {
                        new Guid("50000000-0000-0000-0000-000000000005"),
                        new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                        new Guid("40000000-0000-0000-0000-000000000003"),
                        "DevOps Engineer",
                        "Completed",
                        new Guid("30000000-0000-0000-0000-000000000006"),
                    },
                }
            );

            migrationBuilder.InsertData(
                table: "talent_profiles",
                columns: new[]
                {
                    "user_id",
                    "bio",
                    "created_at",
                    "department",
                    "first_name",
                    "last_name",
                    "updated_at",
                },
                values: new object[,]
                {
                    {
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        "Senior .NET developer with 5+ years of experience in building scalable web applications. Expertise in ASP.NET Core, Entity Framework, and cloud technologies.",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "Engineering",
                        "John",
                        "Doe",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    {
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        "Full-stack developer specializing in React and .NET technologies. Strong background in database design and API development.",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "Engineering",
                        "Jane",
                        "Smith",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                }
            );

            migrationBuilder.InsertData(
                table: "talent_profiles",
                columns: new[]
                {
                    "user_id",
                    "availability_status",
                    "bio",
                    "created_at",
                    "department",
                    "first_name",
                    "last_name",
                    "updated_at",
                },
                values: new object[]
                {
                    new Guid("30000000-0000-0000-0000-000000000006"),
                    "OnLeave",
                    "DevOps engineer with expertise in containerization, CI/CD pipelines, and cloud infrastructure management.",
                    new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    "DevOps",
                    "Mike",
                    "Wilson",
                    new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                }
            );

            migrationBuilder.InsertData(
                table: "talent_skills",
                columns: new[] { "skill_id", "user_id", "level" },
                values: new object[,]
                {
                    {
                        new Guid("20000000-0000-0000-0000-000000000001"),
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        "Expert",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000002"),
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000003"),
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000009"),
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000001"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000002"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        "Intermediate",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000005"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        "Expert",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000006"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000007"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000010"),
                        new Guid("30000000-0000-0000-0000-000000000006"),
                        "Intermediate",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000012"),
                        new Guid("30000000-0000-0000-0000-000000000006"),
                        "Expert",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000013"),
                        new Guid("30000000-0000-0000-0000-000000000006"),
                        "Advanced",
                    },
                    {
                        new Guid("20000000-0000-0000-0000-000000000014"),
                        new Guid("30000000-0000-0000-0000-000000000006"),
                        "Advanced",
                    },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000008")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000011")
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000001"),
                    new Guid("30000000-0000-0000-0000-000000000004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000002"),
                    new Guid("30000000-0000-0000-0000-000000000004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000003"),
                    new Guid("30000000-0000-0000-0000-000000000004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000009"),
                    new Guid("30000000-0000-0000-0000-000000000004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000001"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000002"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000005"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000006"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000007"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000010"),
                    new Guid("30000000-0000-0000-0000-000000000006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000012"),
                    new Guid("30000000-0000-0000-0000-000000000006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000013"),
                    new Guid("30000000-0000-0000-0000-000000000006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("20000000-0000-0000-0000-000000000014"),
                    new Guid("30000000-0000-0000-0000-000000000006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003")
            );

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "id",
                keyValue: new Guid("40000000-0000-0000-0000-000000000003")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000007")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000009")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000010")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000012")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000013")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000014")
            );

            migrationBuilder.DeleteData(
                table: "talent_profiles",
                keyColumn: "user_id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004")
            );

            migrationBuilder.DeleteData(
                table: "talent_profiles",
                keyColumn: "user_id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005")
            );

            migrationBuilder.DeleteData(
                table: "talent_profiles",
                keyColumn: "user_id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004")
            );

            migrationBuilder.RenameColumn(name: "otp", table: "users", newName: "Otp");

            migrationBuilder.RenameColumn(name: "expired", table: "users", newName: "Expired");

            migrationBuilder.RenameColumn(name: "is_used", table: "users", newName: "IsUsed");
        }
    }
}
