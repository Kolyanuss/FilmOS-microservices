using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreCodeFirstSampleWEBAPI.Migrations
{
    public partial class add_new_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Description",
                columns: new[] { "Id", "Author", "DescriptionText" },
                values: new object[] { 1, "Husmant", "Best film" });

            migrationBuilder.InsertData(
                table: "Description",
                columns: new[] { "Id", "Author", "DescriptionText" },
                values: new object[] { 2, "Husmant2", "Almost best film" });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Country", "FKDescriptionId", "NameFilm", "ReleaseData" },
                values: new object[] { 1, "USA", 1, "Hellowin", new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Country", "FKDescriptionId", "NameFilm", "ReleaseData" },
                values: new object[] { 2, "Ukraine", 2, "Strangers", new DateTime(2021, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Description",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Description",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
