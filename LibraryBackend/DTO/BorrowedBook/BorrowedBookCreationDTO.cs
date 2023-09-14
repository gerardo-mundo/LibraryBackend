using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.BorrowedBooks
{
    public class BorrowedBookCreationDTO
    {
        [Required]
        public int Adquisition { get; set; }
    }
}
