
namespace LibraryBackend.DTO.Books
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorName { get; set; } = null!;
        public string? AuthorSecondName { get; set; }
        public string LastName { get; set; } = null!;
        public string? AuthorMotherName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public byte ISBN { get; set; }
        public byte Year { get; set; }
        public string Collection { get; set; } = null!;
        public byte Copies { get; set; }
    }
}
