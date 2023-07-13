using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChatName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChatUsers_ChatUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatUserId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_UserId",
                table: "ChatUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_Users_UserId",
                table: "ChatUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_Users_UserId",
                table: "ChatUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_UserId",
                table: "ChatUsers");

            migrationBuilder.AddColumn<int>(
                name: "ChatUserId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatUserId",
                table: "Users",
                column: "ChatUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ChatUsers_ChatUserId",
                table: "Users",
                column: "ChatUserId",
                principalTable: "ChatUsers",
                principalColumn: "Id");
        }
    }
}
