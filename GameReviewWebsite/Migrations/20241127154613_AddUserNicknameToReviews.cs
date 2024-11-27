using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviewWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNicknameToReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserNickname",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserNickname",
                table: "Reviews");
        }
    }
}
