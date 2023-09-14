using LibraryBackend.DTO.BorrowedBooks;
using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Loans
{
    public class LoanDTO
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DevolutionDate { get; set; }
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        public List<BorrowedBookDTO> BorrowedBooks { get; set; } = null!;
    }
}
