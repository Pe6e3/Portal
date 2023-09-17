using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class lifehacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryImage", "Description", "Name", "Slug" },
                values: new object[] { 4, "6.jpg", "Различные интересные способы", "Лайфхаки", "lifehacks" });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "Position",
                value: 5);

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "MenuId", "Name", "Position", "Slug" },
                values: new object[] { 5, 1, "Лайфхаки", 4, "category/lifehacks" });

            migrationBuilder.UpdateData(
                table: "PostContents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PostBody", "Title" },
                values: new object[] { "Авторизуйтесь. \r\n<br />  Логин <b> Admin</b>, Пароль <b>111</b>.\r\n<br /> Перейдите по ссылке ниже, чтобы сгенерировать 20 случайных постов (занимает около 20 секунд после нажатия, ждите)\r\n<br />  <b><a href=\"https://localhost:7164/Admin/Posts/GenerateRandomPosts?count=20\"> Генератор постов</a> </b>", "Стартовый пост (прочитайте)" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 17, 17, 42, 15, 232, DateTimeKind.Local).AddTicks(6564));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 9, 17, 17, 42, 15, 232, DateTimeKind.Local).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2023, 9, 17, 17, 42, 15, 232, DateTimeKind.Local).AddTicks(6521));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "Position",
                value: 4);

            migrationBuilder.UpdateData(
                table: "PostContents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PostBody", "Title" },
                values: new object[] { "Авторизуйтесь, логин <b> Admin</b>, пароль <b>111</b>. Перейдите по ссылке ниже, чтобы сгенерировать 20 случайных постов (занимает около 20 секунд после нажатия, жди)\r\n<br />  <b><a href=\"https://localhost:7164/Admin/Posts/GenerateRandomPosts?count=20\"> Генератор постов</a> </b>", "Стартовый пост (прочитай)" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 8, 6, 14, 46, 36, 561, DateTimeKind.Local).AddTicks(98));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationDate",
                value: new DateTime(2023, 8, 6, 14, 46, 36, 561, DateTimeKind.Local).AddTicks(64));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationDate",
                value: new DateTime(2023, 8, 6, 14, 46, 36, 561, DateTimeKind.Local).AddTicks(81));
        }
    }
}
