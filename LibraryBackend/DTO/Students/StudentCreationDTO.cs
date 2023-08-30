using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Students
{
    public class StudentCreationDTO
    {
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
        [Required(ErrorMessage = "La mátricula del alumno es requerida"), 
            MaxLength(8, ErrorMessage = "Se admiten un máximo de 8 caracteres")]
        public string EnrollmentNum { get; set; } = null!;
        [MaxLength(120)]
        public string? Address { get; set; } = null!;
    }
}
