using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Entities
{
    public class BorrowedBooks
    {
        public int Id { get; set; }
        [Required]
        public int Adquisition { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; } = null!;
    }
}
