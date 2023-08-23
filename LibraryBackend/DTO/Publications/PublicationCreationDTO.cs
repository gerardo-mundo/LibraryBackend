using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.DTO.Publications
{
    public class PublicationCreationDTO
    {
        [Required(ErrorMessage = "Este campo es requerido"), Column(TypeName = "nvarchar(11)")]
        public PublicationTypes Type { get; set; }
        [Required(ErrorMessage = "Este campo es requerido"), MaxLength(150)]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "Se requiere al menos un autor"), MaxLength(120),]
        public string Author { get; set; } = null!;
        [MaxLength(120)]
        public string? AuthorTwo { get; set; }
        [MaxLength(120)]
        public string? AuthorThree { get; set; }
        [MaxLength(120)]
        public string? AuthorFour { get; set; }
        [Required(ErrorMessage = "Este campo es requerido"), MaxLength(50)]
        public string Publisher { get; set; } = null!;
        public long? ISBN { get; set; }
        public int? ISSN { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Este campo es requerido"), MaxLength(10)]
        public string Vol { get; set; } = null!;
    }
}
