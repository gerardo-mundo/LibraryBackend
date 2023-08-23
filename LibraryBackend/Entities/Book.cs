using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string Title { get; set; } = null!;
        [Required,MaxLength(20)]
        public string AuthorName { get; set; } = null!;
        [MaxLength(20)]
        public string? AuthorSecondName { get; set; }
        [Required,MaxLength(20)]
        public string LastName { get; set; } = null!;
        [MaxLength(20)]
        public string? AuthorMotherName { get; set; } = null!;
        [Required,MaxLength(50)]
        public string Publisher { get; set; } = null!;
        [Required]
        public long ISBN { get; set; }
        [Required]
        public int Year { get; set; }
        [Required, MaxLength(20)]
        public string Collection { get; set; } = null!;
        [Required]
        public byte Copies { get; set; }

    }
}
