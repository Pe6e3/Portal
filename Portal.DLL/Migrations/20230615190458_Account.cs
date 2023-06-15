using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "UserProfiles",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "UserProfiles",
                newName: "Firstname");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImg",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "UserProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AvatarImg",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "UserProfiles",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "UserProfiles",
                newName: "FirstName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "UserProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");
        }
    }
}
