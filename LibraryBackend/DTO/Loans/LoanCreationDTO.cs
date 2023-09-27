using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Loans
{
    public class LoanCreationDTO
    {
        public int UserId { get; set; }
        [Required, MinLength(1), MaxLength(3)]
        public List<int> BorrowedBooks { get; set; }
    }
}
