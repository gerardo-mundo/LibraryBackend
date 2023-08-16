using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Books
{
    public class BookCreationDTO
    {
        [Required(ErrorMessage = "El campo título es requerido"), StringLength(maximumLength:80)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "El campo nombre es requerido"), StringLength(maximumLength:20, 
            ErrorMessage ="El campo nombre sólo puede tener un máximo de 20 caracteres")]
        public string AuthorName { get; set; } = null!;

        [StringLength(maximumLength: 20,
            ErrorMessage = "El campo segundo nombre sólo puede tener un máximo de 20 caracteres")]
        public string? AuthorSecondName { get; set; }

        [Required(ErrorMessage = "El campo apellido es requerido"), StringLength(maximumLength: 20,
            ErrorMessage = "El campo apellido sólo puede tener un máximo de 20 caracteres")]
        public string LastName { get; set; } = null!;

        [StringLength(maximumLength: 20,
            ErrorMessage = "El campo apellido materno sólo puede tener un máximo de 20 caracteres")]
        public string? AuthorMotherName { get; set; } = null!;

        [Required(ErrorMessage = "El campo editorial es requerido"), StringLength(maximumLength: 20,
            ErrorMessage = "El campo editorial sólo puede tener un máximo de 20 caracteres")]
        public string Publisher { get; set; } = null!;

        [Required(ErrorMessage = "El campo ISBN es requerido"), MaxLength(12, ErrorMessage = "No es un ISBN válido")]
        public byte ISBN { get; set; }

        [Required(ErrorMessage = "El campo año es requerido"), MaxLength(4, ErrorMessage ="No es un formato de fecha válido")]
        public byte Year { get; set; }

        [Required(ErrorMessage = "El campo colección es requerido"), StringLength(maximumLength: 20,
            ErrorMessage = "El campo colección sólo puede tener un máximo de 20 caracteres")]
        public string Collection { get; set; } = null!;

        [Required, MaxLength(250)]
        public byte Copies { get; set; }
    }
}
