using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class ISSNPublicationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ISBN",
                table: "Publications",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "ISSN",
                table: "Publications",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISSN",
                table: "Publications");

            migrationBuilder.AlterColumn<long>(
                name: "ISBN",
                table: "Publications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
