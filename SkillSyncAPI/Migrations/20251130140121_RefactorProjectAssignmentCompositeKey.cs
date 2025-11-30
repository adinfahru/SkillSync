using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillSyncAPI.Migrations
{
    /// <inheritdoc />
    public partial class RefactorProjectAssignmentCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_project_assignments",
                table: "project_assignments"
            );

            migrationBuilder.DropIndex(
                name: "IX_project_assignments_project_id",
                table: "project_assignments"
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004")
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumn: "id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005")
            );

            migrationBuilder.DropColumn(name: "id", table: "project_assignments");

            migrationBuilder.DropColumn(name: "status", table: "project_assignments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_assignments",
                table: "project_assignments",
                columns: new[] { "project_id", "user_id" }
            );

            migrationBuilder.InsertData(
                table: "project_assignments",
                columns: new[]
                {
                    "project_id",
                    "user_id",
                    "assigned_at",
                    "completed_at",
                    "role_on_project",
                },
                values: new object[,]
                {
                    {
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        "Senior Backend Developer",
                    },
                    {
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        "Full Stack Developer",
                    },
                    {
                        new Guid("40000000-0000-0000-0000-000000000002"),
                        new Guid("30000000-0000-0000-0000-000000000005"),
                        new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc),
                        "Frontend Developer",
                    },
                    {
                        new Guid("40000000-0000-0000-0000-000000000003"),
                        new Guid("30000000-0000-0000-0000-000000000004"),
                        new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                        "Tech Lead",
                    },
                    {
                        new Guid("40000000-0000-0000-0000-000000000003"),
                        new Guid("30000000-0000-0000-0000-000000000006"),
                        new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc),
                        new DateTime(2025, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                        "DevOps Engineer",
                    },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_project_assignments",
                table: "project_assignments"
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumns: new[] { "project_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("40000000-0000-0000-0000-000000000001"),
                    new Guid("30000000-0000-0000-0000-000000000004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumns: new[] { "project_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("40000000-0000-0000-0000-000000000001"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumns: new[] { "project_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("40000000-0000-0000-0000-000000000002"),
                    new Guid("30000000-0000-0000-0000-000000000005"),
                }
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumns: new[] { "project_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("40000000-0000-0000-0000-000000000003"),
                    new Guid("30000000-0000-0000-0000-000000000004"),
                }
            );

            migrationBuilder.DeleteData(
                table: "project_assignments",
                keyColumns: new[] { "project_id", "user_id" },
                keyValues: new object[]
                {
                    new Guid("40000000-0000-0000-0000-000000000003"),
                    new Guid("30000000-0000-0000-0000-000000000006"),
                }
            );

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "project_assignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "project_assignments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Active"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_assignments",
                table: "project_assignments",
                column: "id"
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
                        new Guid("50000000-0000-0000-0000-000000000001"),
                        new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        "Senior Backend Developer",
                        "Active",
                        new Guid("30000000-0000-0000-0000-000000000004"),
                    },
                    {
                        new Guid("50000000-0000-0000-0000-000000000002"),
                        new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc),
                        null,
                        new Guid("40000000-0000-0000-0000-000000000001"),
                        "Full Stack Developer",
                        "Active",
                        new Guid("30000000-0000-0000-0000-000000000005"),
                    },
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

            migrationBuilder.CreateIndex(
                name: "IX_project_assignments_project_id",
                table: "project_assignments",
                column: "project_id"
            );
        }
    }
}
