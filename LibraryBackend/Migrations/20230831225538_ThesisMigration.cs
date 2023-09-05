using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class ThesisMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Thesis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Assesor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thesis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ThesisId",
                table: "Students",
                column: "ThesisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Thesis_ThesisId",
                table: "Students",
                column: "ThesisId",
                principalTable: "Thesis",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Thesis_ThesisId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Thesis");

            migrationBuilder.DropIndex(
                name: "IX_Students_ThesisId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Students");
        }
    }
}
