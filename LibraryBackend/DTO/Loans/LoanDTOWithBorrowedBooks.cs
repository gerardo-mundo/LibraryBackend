using LibraryBackend.DTO.BorrowedBooks;

namespace LibraryBackend.DTO.Loans
{
    public class LoanDTOWithBorrowedBooks : LoanDTO
    {
        public List<BorrowedBookDTO> BorrowedBooks { get; set; }
    }
}
