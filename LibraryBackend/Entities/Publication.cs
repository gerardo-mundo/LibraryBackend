using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Entities
{
    public class Publication
    {
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar(11)")]
        public PublicationTypes Type { get; set; }
        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;
        [Required, MaxLength(120)]
        public string Author { get; set; } = null!;
        [MaxLength(120)]
        public string? AuthorTwo { get; set; }
        [MaxLength(120)]
        public string? AuthorThree { get; set; }
        [MaxLength(120)]
        public string? AuthorFour { get; set; }
        [Required, MaxLength(50)]
        public string Publisher { get; set; } = null!;
        [Required]
        public long ISBN { get; set; }
        [Required]
        public int Year { get; set; }
        [Required, MaxLength(10)]
        public string Vol { get; set; } = null!;
    }
}
