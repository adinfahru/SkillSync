using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSyncAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ProjectAssignments");

            migrationBuilder.DropPrimaryKey(name: "PK_Users", table: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_Skills", table: "Skills");

            migrationBuilder.DropPrimaryKey(name: "PK_Roles", table: "Roles");

            migrationBuilder.DropPrimaryKey(name: "PK_Projects", table: "Projects");

            migrationBuilder.DropPrimaryKey(name: "PK_TalentSkills", table: "TalentSkills");

            migrationBuilder.DropPrimaryKey(name: "PK_TalentProfiles", table: "TalentProfiles");

            migrationBuilder.RenameTable(name: "Users", newName: "users");

            migrationBuilder.RenameTable(name: "Skills", newName: "skills");

            migrationBuilder.RenameTable(name: "Roles", newName: "roles");

            migrationBuilder.RenameTable(name: "Projects", newName: "projects");

            migrationBuilder.RenameTable(name: "TalentSkills", newName: "talent_skills");

            migrationBuilder.RenameTable(name: "TalentProfiles", newName: "talent_profiles");

            migrationBuilder.RenameColumn(name: "Password", table: "users", newName: "password");

            migrationBuilder.RenameColumn(name: "Email", table: "users", newName: "email");

            migrationBuilder.RenameColumn(name: "Id", table: "users", newName: "id");

            migrationBuilder.RenameColumn(name: "UpdatedAt", table: "users", newName: "updated_at");

            migrationBuilder.RenameColumn(name: "RoleId", table: "users", newName: "role_id");

            migrationBuilder.RenameColumn(name: "IsActive", table: "users", newName: "is_active");

            migrationBuilder.RenameColumn(name: "CreatedAt", table: "users", newName: "created_at");

            migrationBuilder.RenameColumn(name: "Name", table: "skills", newName: "name");

            migrationBuilder.RenameColumn(name: "Category", table: "skills", newName: "category");

            migrationBuilder.RenameColumn(name: "Id", table: "skills", newName: "id");

            migrationBuilder.RenameColumn(name: "Name", table: "roles", newName: "name");

            migrationBuilder.RenameColumn(name: "Id", table: "roles", newName: "id");

            migrationBuilder.RenameColumn(name: "Status", table: "projects", newName: "status");

            migrationBuilder.RenameColumn(name: "Name", table: "projects", newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "projects",
                newName: "description"
            );

            migrationBuilder.RenameColumn(name: "Id", table: "projects", newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "projects",
                newName: "updated_at"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "projects",
                newName: "created_at"
            );

            migrationBuilder.RenameColumn(name: "Level", table: "talent_skills", newName: "level");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "talent_skills",
                newName: "skill_id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "talent_skills",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "talent_profiles",
                newName: "department"
            );

            migrationBuilder.RenameColumn(name: "Bio", table: "talent_profiles", newName: "bio");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "talent_profiles",
                newName: "updated_at"
            );

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "talent_profiles",
                newName: "last_name"
            );

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "talent_profiles",
                newName: "first_name"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "talent_profiles",
                newName: "created_at"
            );

            migrationBuilder.RenameColumn(
                name: "AvailabilityStatus",
                table: "talent_profiles",
                newName: "availability_status"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "talent_profiles",
                newName: "user_id"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_users", table: "users", column: "id");

            migrationBuilder.AddPrimaryKey(name: "PK_skills", table: "skills", column: "id");

            migrationBuilder.AddPrimaryKey(name: "PK_roles", table: "roles", column: "id");

            migrationBuilder.AddPrimaryKey(name: "PK_projects", table: "projects", column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_talent_skills",
                table: "talent_skills",
                columns: new[] { "user_id", "skill_id" }
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_talent_profiles",
                table: "talent_profiles",
                column: "user_id"
            );

            migrationBuilder.CreateTable(
                name: "project_assignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    project_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_on_project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    assigned_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_assignments", x => x.id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "project_assignments");

            migrationBuilder.DropPrimaryKey(name: "PK_users", table: "users");

            migrationBuilder.DropPrimaryKey(name: "PK_skills", table: "skills");

            migrationBuilder.DropPrimaryKey(name: "PK_roles", table: "roles");

            migrationBuilder.DropPrimaryKey(name: "PK_projects", table: "projects");

            migrationBuilder.DropPrimaryKey(name: "PK_talent_skills", table: "talent_skills");

            migrationBuilder.DropPrimaryKey(name: "PK_talent_profiles", table: "talent_profiles");

            migrationBuilder.RenameTable(name: "users", newName: "Users");

            migrationBuilder.RenameTable(name: "skills", newName: "Skills");

            migrationBuilder.RenameTable(name: "roles", newName: "Roles");

            migrationBuilder.RenameTable(name: "projects", newName: "Projects");

            migrationBuilder.RenameTable(name: "talent_skills", newName: "TalentSkills");

            migrationBuilder.RenameTable(name: "talent_profiles", newName: "TalentProfiles");

            migrationBuilder.RenameColumn(name: "password", table: "Users", newName: "Password");

            migrationBuilder.RenameColumn(name: "email", table: "Users", newName: "Email");

            migrationBuilder.RenameColumn(name: "id", table: "Users", newName: "Id");

            migrationBuilder.RenameColumn(name: "updated_at", table: "Users", newName: "UpdatedAt");

            migrationBuilder.RenameColumn(name: "role_id", table: "Users", newName: "RoleId");

            migrationBuilder.RenameColumn(name: "is_active", table: "Users", newName: "IsActive");

            migrationBuilder.RenameColumn(name: "created_at", table: "Users", newName: "CreatedAt");

            migrationBuilder.RenameColumn(name: "name", table: "Skills", newName: "Name");

            migrationBuilder.RenameColumn(name: "category", table: "Skills", newName: "Category");

            migrationBuilder.RenameColumn(name: "id", table: "Skills", newName: "Id");

            migrationBuilder.RenameColumn(name: "name", table: "Roles", newName: "Name");

            migrationBuilder.RenameColumn(name: "id", table: "Roles", newName: "Id");

            migrationBuilder.RenameColumn(name: "status", table: "Projects", newName: "Status");

            migrationBuilder.RenameColumn(name: "name", table: "Projects", newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Projects",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(name: "id", table: "Projects", newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Projects",
                newName: "UpdatedAt"
            );

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Projects",
                newName: "CreatedAt"
            );

            migrationBuilder.RenameColumn(name: "level", table: "TalentSkills", newName: "Level");

            migrationBuilder.RenameColumn(
                name: "skill_id",
                table: "TalentSkills",
                newName: "SkillId"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TalentSkills",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "department",
                table: "TalentProfiles",
                newName: "Department"
            );

            migrationBuilder.RenameColumn(name: "bio", table: "TalentProfiles", newName: "Bio");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "TalentProfiles",
                newName: "UpdatedAt"
            );

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "TalentProfiles",
                newName: "LastName"
            );

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "TalentProfiles",
                newName: "FirstName"
            );

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "TalentProfiles",
                newName: "CreatedAt"
            );

            migrationBuilder.RenameColumn(
                name: "availability_status",
                table: "TalentProfiles",
                newName: "AvailabilityStatus"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "TalentProfiles",
                newName: "UserId"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Users", table: "Users", column: "Id");

            migrationBuilder.AddPrimaryKey(name: "PK_Skills", table: "Skills", column: "Id");

            migrationBuilder.AddPrimaryKey(name: "PK_Roles", table: "Roles", column: "Id");

            migrationBuilder.AddPrimaryKey(name: "PK_Projects", table: "Projects", column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TalentSkills",
                table: "TalentSkills",
                columns: new[] { "UserId", "SkillId" }
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_TalentProfiles",
                table: "TalentProfiles",
                column: "UserId"
            );

            migrationBuilder.CreateTable(
                name: "ProjectAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleOnProject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssignments", x => x.Id);
                }
            );
        }
    }
}
