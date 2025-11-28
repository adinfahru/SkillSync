using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSyncAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationAndCardinality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_assignments_users_user_id",
                table: "project_assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_talent_skills_talent_profiles_TalentProfileUserId",
                table: "talent_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_talent_skills_TalentProfileUserId",
                table: "talent_skills");

            migrationBuilder.DropColumn(
                name: "TalentProfileUserId",
                table: "talent_skills");

            migrationBuilder.AddForeignKey(
                name: "FK_project_assignments_users_user_id",
                table: "project_assignments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_talent_skills_talent_profiles_user_id",
                table: "talent_skills",
                column: "user_id",
                principalTable: "talent_profiles",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_assignments_users_user_id",
                table: "project_assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_talent_skills_talent_profiles_user_id",
                table: "talent_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_role_id",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "TalentProfileUserId",
                table: "talent_skills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_talent_skills_TalentProfileUserId",
                table: "talent_skills",
                column: "TalentProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_assignments_users_user_id",
                table: "project_assignments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_talent_skills_talent_profiles_TalentProfileUserId",
                table: "talent_skills",
                column: "TalentProfileUserId",
                principalTable: "talent_profiles",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
