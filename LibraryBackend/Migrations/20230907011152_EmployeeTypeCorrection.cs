using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class EmployeeTypeCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Employees",
                type: "nvarchar(8)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Employees",
                type: "nvarchar(7)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)");
        }
    }
}
