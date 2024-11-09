using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameReviewWebsite.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Genre", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Exciting action game.", "Action", 4.5, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Game 1" },
                    { 2, "Immersive adventure game.", "Adventure", 4.2000000000000002, new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Game 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
