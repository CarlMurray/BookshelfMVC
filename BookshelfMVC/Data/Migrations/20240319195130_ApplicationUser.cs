using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookshelfMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "BlogPosts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ApplicationUserId1",
                table: "BlogPosts",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_AspNetUsers_ApplicationUserId1",
                table: "BlogPosts",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_AspNetUsers_ApplicationUserId1",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_ApplicationUserId1",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "BlogPosts");
        }
    }
}
