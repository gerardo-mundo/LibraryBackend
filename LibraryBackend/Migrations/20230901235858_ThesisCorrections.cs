using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class ThesisCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Thesis_ThesisId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ThesisId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ThesisId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Assesor",
                table: "Thesis",
                newName: "AuthorOne");

            migrationBuilder.AddColumn<string>(
                name: "Assessor",
                table: "Thesis",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorThree",
                table: "Thesis",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorTwo",
                table: "Thesis",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assessor",
                table: "Thesis");

            migrationBuilder.DropColumn(
                name: "AuthorThree",
                table: "Thesis");

            migrationBuilder.DropColumn(
                name: "AuthorTwo",
                table: "Thesis");

            migrationBuilder.RenameColumn(
                name: "AuthorOne",
                table: "Thesis",
                newName: "Assesor");

            migrationBuilder.AddColumn<int>(
                name: "StudentsId",
                table: "Thesis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThesisId",
                table: "Students",
                type: "int",
                nullable: true);

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
    }
}
