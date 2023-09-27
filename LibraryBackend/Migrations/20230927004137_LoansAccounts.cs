using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class LoansAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Loans",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                table: "Loans",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_AccountId",
                table: "Loans",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_AccountId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AccountId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Loans");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Employees_EmployeeId",
                table: "Loans",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
