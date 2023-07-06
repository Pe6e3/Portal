using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MyLogger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyLoggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserClick = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyLoggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyLoggers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyLoggers_UserId",
                table: "MyLoggers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyLoggers");
        }
    }
}
