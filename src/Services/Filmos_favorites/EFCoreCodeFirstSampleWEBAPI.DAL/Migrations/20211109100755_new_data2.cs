using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreCodeFirstSampleWEBAPI.Migrations
{
    public partial class new_data2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "Password", "UserName" },
                values: new object[] { 1, true, "1234", "Nikolay" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsAdmin", "Password", "UserName" },
                values: new object[] { 2, false, "111", "Vasya" });

            migrationBuilder.InsertData(
                table: "FilmsUsers",
                columns: new[] { "IdFilms", "IdUser" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "FilmsUsers",
                columns: new[] { "IdFilms", "IdUser" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FilmsUsers",
                keyColumns: new[] { "IdFilms", "IdUser" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "FilmsUsers",
                keyColumns: new[] { "IdFilms", "IdUser" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
