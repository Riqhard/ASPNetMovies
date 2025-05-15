using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Sci-Fi" },
                    { 3, "Fantasy" },
                    { 4, "Horror" },
                    { 5, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "Name", "Price", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, 1, "Inception", 12.99m, new DateOnly(2003, 1, 1) },
                    { 2, 1, "The Dark Knight", 14.99m, new DateOnly(2005, 1, 1) },
                    { 3, 2, "Interstellar", 10.99m, new DateOnly(2007, 1, 1) },
                    { 4, 2, "The Matrix", 9.99m, new DateOnly(1999, 1, 1) },
                    { 5, 3, "The Lord of the Rings: The Fellowship of the Ring", 11.99m, new DateOnly(2001, 1, 1) },
                    { 6, 3, "The Lord of the Rings: The Two Towers", 11.99m, new DateOnly(2002, 1, 1) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
