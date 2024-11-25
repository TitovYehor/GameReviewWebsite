using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddGameAndReviewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewerName",
                table: "Reviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Reviews",
                newName: "Content");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_GameId",
                table: "Reviews",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Games_GameId",
                table: "Reviews",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Games_GameId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_GameId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reviews",
                newName: "ReviewerName");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Reviews",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
