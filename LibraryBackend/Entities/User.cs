using LibraryBackend.Utilities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar(15)")]
        public UserTypes Type { get; set; }
        [Required, MaxLength(40)] 
        public string Name { get; set; } = null!;
        [MaxLength(40)]
        public string? SecondName { get; set; } = null!;
        [Required, MaxLength(60)]
        public string LastName { get; set; } = null!;
        [Required, MaxLength(60)]
        public string MotherName { get; set; } = null!;
        [MaxLength(8)]
        public string? EnrollmentNum { get; set; } = null!;
        [MaxLength(10)]
        public string? EmployeeKey { get; set; } = null!;
        [MaxLength(120)]
        public string? Address { get; set; } = null!;
    }
}
