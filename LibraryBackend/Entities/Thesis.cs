using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Entities
{
    public class Thesis
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;
        [Required, MaxLength(100)]
        public string AuthorOne { get; set; } = null!;
        [MaxLength(100)]
        public string? AuthorTwo { get; set; }
        [MaxLength(100)]
        public string? AuthorThree { get; set; }
        [Required, MaxLength(100)]
        public string Assessor { get; set; } = null!;
        [Required]
        public int Year { get; set; }
    }
}
