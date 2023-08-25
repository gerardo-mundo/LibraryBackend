using LibraryBackend.Utilities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.DTO.Publications
{
    public class PublicationCreationDTO
    {
        [Required(ErrorMessage = "Este campo es requerido")]
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
        [StringLength(16, ErrorMessage = "No es un ISBN válido")]
        public string? ISBN { get; set; }
        [StringLength(9, ErrorMessage = "No es un ISBN válido")]
        public string? ISSN { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Este campo es requerido"), MaxLength(10)]
        public string Vol { get; set; } = null!;
    }
}
