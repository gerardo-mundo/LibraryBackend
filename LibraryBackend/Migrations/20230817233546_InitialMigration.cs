using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AuthorSecondName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AuthorMotherName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ISBN = table.Column<long>(type: "bigint", maxLength: 12, nullable: false),
                    Year = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Collection = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Copies = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
