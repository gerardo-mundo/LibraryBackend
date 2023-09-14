using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Loans
{
    public class LoanCreationDTO
    {
        public int EmployeeId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(3)]
        public List<int> BorrowedBooks { get; set; } = null!;
    }
}
