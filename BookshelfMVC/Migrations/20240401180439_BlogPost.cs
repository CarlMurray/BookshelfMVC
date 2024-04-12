using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookshelfMVC.Migrations
{
    /// <inheritdoc />
    public partial class BlogPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.CreateTable(
                name: "BlogPostViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_BlogPostViewModel", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "BookDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumPages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_BookDTO", x => x.Id);
                });

            _ = migrationBuilder.CreateTable(
                name: "AuthorDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookDTOId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    _ = table.PrimaryKey("PK_AuthorDTO", x => x.Id);
                    _ = table.ForeignKey(
                        name: "FK_AuthorDTO_BookDTO_BookDTOId",
                        column: x => x.BookDTOId,
                        principalTable: "BookDTO",
                        principalColumn: "Id");
                });

            _ = migrationBuilder.CreateIndex(
                name: "IX_AuthorDTO_BookDTOId",
                table: "AuthorDTO",
                column: "BookDTOId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropTable(
                name: "AuthorDTO");

            _ = migrationBuilder.DropTable(
                name: "BlogPostViewModel");

            _ = migrationBuilder.DropTable(
                name: "BookDTO");
        }
    }
}
