using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Authentication
{
    public class UpdatePasswordDTO
    {
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(10, ErrorMessage = "Debe tener entre 8 y 10 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
