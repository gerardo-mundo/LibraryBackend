using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string Title { get; set; } = null!;
        [Required,StringLength(20)]
        public string AuthorName { get; set; } = null!;
        [StringLength(20)]
        public string? AuthorSecondName { get; set; }
        [Required,StringLength(20)]
        public string LastName { get; set; } = null!;
        [StringLength(20)]
        public string? AuthorMotherName { get; set; } = null!;
        [Required,StringLength(50)]
        public string Publisher { get; set; } = null!;
        [Required, MaxLength(12)]
        public long ISBN { get; set; }
        [Required, MaxLength(4)]
        public int Year { get; set; }
        [Required, StringLength(20)]
        public string Collection { get; set; } = null!;
        [Required]
        public byte Copies { get; set; }

    }
}
