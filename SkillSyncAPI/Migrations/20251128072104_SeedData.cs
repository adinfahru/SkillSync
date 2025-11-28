using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSyncAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "projects",
                columns: new[] { "id", "created_at", "description", "name", "updated_at" },
                values: new object[]
                {
                    new Guid("880e8400-e29b-41d4-a716-446655440001"),
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
                        new Guid("880e8400-e29b-41d4-a716-446655440002"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "CRM system to manage customer relationships, track sales, and generate reports. Built with ASP.NET Core Web API and Angular frontend.",
                        "Customer Management System",
                        "OnHold",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    {
                        new Guid("880e8400-e29b-41d4-a716-446655440003"),
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
                    { new Guid("550e8400-e29b-41d4-a716-446655440001"), "Admin" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440002"), "HR" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440003"), "ProjectManager" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440004"), "Talent" },
                }
            );

            migrationBuilder.InsertData(
                table: "skills",
                columns: new[] { "id", "category", "name" },
                values: new object[,]
                {
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440001"),
                        "Backend Development",
                        "C#",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440002"),
                        "Backend Development",
                        "ASP.NET Core",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440003"),
                        "Backend Development",
                        "Entity Framework Core",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440004"),
                        "Backend Development",
                        "RESTful API",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440005"),
                        "Frontend Development",
                        "JavaScript",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440006"),
                        "Frontend Development",
                        "React",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440007"),
                        "Frontend Development",
                        "TypeScript",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440008"),
                        "Frontend Development",
                        "HTML/CSS",
                    },
                    { new Guid("660e8400-e29b-41d4-a716-446655440009"), "Database", "SQL Server" },
                    { new Guid("660e8400-e29b-41d4-a716-446655440010"), "Database", "PostgreSQL" },
                    { new Guid("660e8400-e29b-41d4-a716-446655440011"), "Database", "MongoDB" },
                    { new Guid("660e8400-e29b-41d4-a716-446655440012"), "DevOps", "Docker" },
                    { new Guid("660e8400-e29b-41d4-a716-446655440013"), "DevOps", "Azure" },
                    { new Guid("660e8400-e29b-41d4-a716-446655440014"), "DevOps", "CI/CD" },
                }
            );

            migrationBuilder.InsertData(
                table: "users",
                columns: new[]
                {
                    "id",
                    "created_at",
                    "email",
                    "Expired",
                    "is_active",
                    "IsUsed",
                    "Otp",
                    "password",
                    "role_id",
                    "updated_at",
                    "username",
                },
                values: new object[,]
                {
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440001"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "admin@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$N2oKjyGJN5e5FbZ3vHkdLO1KJxCvYuZx3pRqF8tVwYsOZx9HkQ.mG",
                        new Guid("550e8400-e29b-41d4-a716-446655440001"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "admin",
                    },
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440002"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "hr@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$M1nJiyFIN4d4EaY2uGjcKN0JIwBuXtYw2oQpE7sUvXrNYx8GjP.lF",
                        new Guid("550e8400-e29b-41d4-a716-446655440002"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "hr.manager",
                    },
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440003"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "john.pm@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$L0mHhxEHM3c3DaX1tFibJM9IHvAtWsXv1nPoD6rTuWqMXw7FiO.kE",
                        new Guid("550e8400-e29b-41d4-a716-446655440003"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "pm.john",
                    },
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "john.doe@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$K9lGgwDGL2b2CaW0sFhaIM8HGuAsVrWu0mOnC5qSsVpLWv6EhN.jD",
                        new Guid("550e8400-e29b-41d4-a716-446655440004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "john.doe",
                    },
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "jane.smith@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$J8kFfvCFK1a1BaV9rEgaHL7GFtArUqVt9lNmB4pRrUoKVu5DgM.iC",
                        new Guid("550e8400-e29b-41d4-a716-446655440004"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "jane.smith",
                    },
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440006"),
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "mike.wilson@skillsync.com",
                        null,
                        true,
                        false,
                        null,
                        "$2a$11$I7jEeuBEJ0Z0AaU8qDfaGK6FEsAqTpUs8kMlA3oQqTnJTt4CfL.hB",
                        new Guid("550e8400-e29b-41d4-a716-446655440004"),
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
                        new Guid("990e8400-e29b-41d4-a716-446655440001"),
                        new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        new Guid("880e8400-e29b-41d4-a716-446655440001"),
                        "Senior Backend Developer",
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                    },
                    {
                        new Guid("990e8400-e29b-41d4-a716-446655440002"),
                        new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        new Guid("880e8400-e29b-41d4-a716-446655440001"),
                        "Full Stack Developer",
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
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
                        new Guid("990e8400-e29b-41d4-a716-446655440003"),
                        new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc),
                        new Guid("880e8400-e29b-41d4-a716-446655440002"),
                        "Frontend Developer",
                        "Inactive",
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                    },
                    {
                        new Guid("990e8400-e29b-41d4-a716-446655440004"),
                        new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                        new Guid("880e8400-e29b-41d4-a716-446655440003"),
                        "Tech Lead",
                        "Completed",
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                    },
                    {
                        new Guid("990e8400-e29b-41d4-a716-446655440005"),
                        new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                        new Guid("880e8400-e29b-41d4-a716-446655440003"),
                        "DevOps Engineer",
                        "Completed",
                        new Guid("770e8400-e29b-41d4-a716-446655440006"),
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
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                        "Senior .NET developer with 5+ years of experience in building scalable web applications. Expertise in ASP.NET Core, Entity Framework, and cloud technologies.",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                        "Engineering",
                        "John",
                        "Doe",
                        new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc),
                    },
                    {
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
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
                    new Guid("770e8400-e29b-41d4-a716-446655440006"),
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
                        new Guid("660e8400-e29b-41d4-a716-446655440001"),
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                        "Expert",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440002"),
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440003"),
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440009"),
                        new Guid("770e8400-e29b-41d4-a716-446655440004"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440001"),
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440002"),
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                        "Intermediate",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440005"),
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                        "Expert",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440006"),
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440007"),
                        new Guid("770e8400-e29b-41d4-a716-446655440005"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440010"),
                        new Guid("770e8400-e29b-41d4-a716-446655440006"),
                        "Intermediate",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440012"),
                        new Guid("770e8400-e29b-41d4-a716-446655440006"),
                        "Expert",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440013"),
                        new Guid("770e8400-e29b-41d4-a716-446655440006"),
                        "Advanced",
                    },
                    {
                        new Guid("660e8400-e29b-41d4-a716-446655440014"),
                        new Guid("770e8400-e29b-41d4-a716-446655440006"),
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
                keyValue: new Guid("990e8400-e29b-41d4-a716-446655440001")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("990e8400-e29b-41d4-a716-446655440002")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("990e8400-e29b-41d4-a716-446655440003")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("990e8400-e29b-41d4-a716-446655440004")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyValue: new Guid("990e8400-e29b-41d4-a716-446655440005")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440004")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440008")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440011")
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440001"),
                    new Guid("770e8400-e29b-41d4-a716-446655440004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440002"),
                    new Guid("770e8400-e29b-41d4-a716-446655440004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440003"),
                    new Guid("770e8400-e29b-41d4-a716-446655440004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440009"),
                    new Guid("770e8400-e29b-41d4-a716-446655440004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440001"),
                    new Guid("770e8400-e29b-41d4-a716-446655440005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440002"),
                    new Guid("770e8400-e29b-41d4-a716-446655440005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440005"),
                    new Guid("770e8400-e29b-41d4-a716-446655440005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440006"),
                    new Guid("770e8400-e29b-41d4-a716-446655440005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440007"),
                    new Guid("770e8400-e29b-41d4-a716-446655440005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440010"),
                    new Guid("770e8400-e29b-41d4-a716-446655440006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440012"),
                    new Guid("770e8400-e29b-41d4-a716-446655440006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440013"),
                    new Guid("770e8400-e29b-41d4-a716-446655440006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "talent_skills",
                keyColumns: new[] { "skill_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("660e8400-e29b-41d4-a716-446655440014"),
                    new Guid("770e8400-e29b-41d4-a716-446655440006"),
                }
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440001")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440002")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440003")
            );

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "id",
                keyValue: new Guid("880e8400-e29b-41d4-a716-446655440001")
            );

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "id",
                keyValue: new Guid("880e8400-e29b-41d4-a716-446655440002")
            );

            migrationBuilder.DeleteData(
                table: "projects",
                keyColumn: "id",
                keyValue: new Guid("880e8400-e29b-41d4-a716-446655440003")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440001")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440002")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440003")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440001")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440002")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440003")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440005")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440006")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440007")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440009")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440010")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440012")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440013")
            );

            migrationBuilder.DeleteData(
                table: "skills",
                keyColumn: "id",
                keyValue: new Guid("660e8400-e29b-41d4-a716-446655440014")
            );

            migrationBuilder.DeleteData(
                table: "talent_profiles",
                keyColumn: "user_id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440004")
            );

            migrationBuilder.DeleteData(
                table: "talent_profiles",
                keyColumn: "user_id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440005")
            );

            migrationBuilder.DeleteData(
                table: "talent_profiles",
                keyColumn: "user_id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440006")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440004")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440005")
            );

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("770e8400-e29b-41d4-a716-446655440006")
            );

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440004")
            );
        }
    }
}
