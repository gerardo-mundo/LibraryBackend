using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class AdquisitionForISBN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Adquisition",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adquisition",
                table: "Books");

            migrationBuilder.AddColumn<long>(
                name: "ISBN",
                table: "Books",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
