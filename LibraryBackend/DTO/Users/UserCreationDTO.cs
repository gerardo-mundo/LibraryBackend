using LibraryBackend.Utilities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.DTO.Users
{
    public class UserCreationDTO
    {
        [Required, Column(TypeName = "nvarchar(7)")]
        public UserTypes Type { get; set; }
        [Required(ErrorMessage ="El nombre del estudiante es requerido"), 
            MaxLength(40, ErrorMessage = "Se requiere un máximo de 40 caracteres")]
        public string Name { get; set; } = null!;
        [MaxLength(40)]
        public string? SecondName { get; set; } = null!;
        [Required(ErrorMessage = "El apellido paterno es requerido"), 
            MaxLength(60, ErrorMessage = "Se admiten un máximo de 60 caracteres")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "El apellido materno es requerido"), 
            MaxLength(60, ErrorMessage = "Se admiten un máximo de 60 caracteres")]
        public string MotherName { get; set; } = null!;
        [StringLength(8, ErrorMessage = "No es una matricula válida")]
        public string? EnrollmentNum { get; set; } = null!;
        [StringLength(10, ErrorMessage = "No es un número de empleado válido")]
        public string? EmployeeKey { get; set; } = null!;
        [MaxLength(120)]
        public string? Address { get; set; } = null!;
    }
}
