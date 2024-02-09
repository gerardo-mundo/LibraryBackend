using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Authentication
{
    public class UpdatePasswordDTO
    {
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(10, ErrorMessage = "Debe tener mínimo 7 caracteres", MinimumLength = 7)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
