using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreCodeFirstSampleWEBAPI.Migrations
{
    public partial class add_new_genres_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Description_FKDescriptionId",
                table: "Films");

            migrationBuilder.DropTable(
                name: "ListFilms");

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "DescriptionId",
                table: "Films");

            migrationBuilder.AlterColumn<int>(
                name: "FKDescriptionId",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FilmsUsers",
                columns: table => new
                {
                    IdFilms = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsUsers", x => new { x.IdFilms, x.IdUser });
                    table.ForeignKey(
                        name: "FK_FilmsUsers_Films_IdFilms",
                        column: x => x.IdFilms,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsUsers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmsGenres",
                columns: table => new
                {
                    IdFilms = table.Column<int>(type: "int", nullable: false),
                    IdGenres = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsGenres", x => new { x.IdFilms, x.IdGenres });
                    table.ForeignKey(
                        name: "FK_FilmsGenres_Films_IdFilms",
                        column: x => x.IdFilms,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmsGenres_Genres_IdGenres",
                        column: x => x.IdGenres,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmsGenres_IdGenres",
                table: "FilmsGenres",
                column: "IdGenres");

            migrationBuilder.CreateIndex(
                name: "IX_FilmsUsers_IdUser",
                table: "FilmsUsers",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Description_FKDescriptionId",
                table: "Films",
                column: "FKDescriptionId",
                principalTable: "Description",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Description_FKDescriptionId",
                table: "Films");

            migrationBuilder.DropTable(
                name: "FilmsGenres");

            migrationBuilder.DropTable(
                name: "FilmsUsers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.AlterColumn<int>(
                name: "FKDescriptionId",
                table: "Films",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DescriptionId",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ListFilms",
                columns: table => new
                {
                    IdFilms = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListFilms", x => new { x.IdFilms, x.IdUser });
                    table.ForeignKey(
                        name: "FK_ListFilms_Films_IdFilms",
                        column: x => x.IdFilms,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListFilms_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Country", "DescriptionId", "FKDescriptionId", "NameFilm", "ReleaseData" },
                values: new object[] { 1, "USA", 0, null, "Hellowin", new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Country", "DescriptionId", "FKDescriptionId", "NameFilm", "ReleaseData" },
                values: new object[] { 2, "Ukraine", 1, null, "Strangers", new DateTime(2021, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_ListFilms_IdUser",
                table: "ListFilms",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Description_FKDescriptionId",
                table: "Films",
                column: "FKDescriptionId",
                principalTable: "Description",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
