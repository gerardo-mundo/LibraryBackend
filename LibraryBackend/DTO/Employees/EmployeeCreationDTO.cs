using LibraryBackend.Utilities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Employees
{
    public class EmployeeCreationDTO
    {
        [Required, Column(TypeName = "nvarchar(7)")]
        public EmployeeTypes Type { get; set; }
        [Required, MaxLength(20, ErrorMessage = "Sólo se acepta un máximo de 20 caracteres")]
        public string Name { get; set; } = null!;
        [MaxLength(20, ErrorMessage = "Sólo se acepta un máximo de 20 caracteres")]
        public string? SecondName { get; set; } = null!;
        [Required, MaxLength(20, ErrorMessage = "Sólo se acepta un máximo de 20 caracteres")]
        public string LastName { get; set; } = null!;
        [Required, MaxLength(20, ErrorMessage = "Sólo se acepta un máximo de 20 caracteres")]
        public string MotherName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, StringLength(10, MinimumLength = 10, ErrorMessage = "Número de empleado no válido")]
        public string EmployeeKey { get; set; } = null!;
    }
}
