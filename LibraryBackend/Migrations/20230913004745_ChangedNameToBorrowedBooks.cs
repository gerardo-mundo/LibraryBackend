using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryBackend.Migrations
{
    public partial class ChangedNameToBorrowedBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "BorrowedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_LoanId",
                table: "BorrowedBooks",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Loans_LoanId",
                table: "BorrowedBooks",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Loans_LoanId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_LoanId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "BorrowedBooks");
        }
    }
}
