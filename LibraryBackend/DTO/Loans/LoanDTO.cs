using LibraryBackend.DTO.BorrowedBooks;
using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Loans
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public bool Returned { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DevolutionDate { get; set; }
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
    }
}
