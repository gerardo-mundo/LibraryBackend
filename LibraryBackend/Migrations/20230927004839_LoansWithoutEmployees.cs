using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class LoansWithoutEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_EmployeeId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Loans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Loans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_EmployeeId",
                table: "Loans",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
