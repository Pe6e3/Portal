using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Chat3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCount",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCount",
                table: "Chats");
        }
    }
}
