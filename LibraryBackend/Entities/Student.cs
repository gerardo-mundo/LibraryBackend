using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string Name { get; set; } = null!;
        [MaxLength(40)]
        public string? SecondName { get; set; } = null!;
        [Required, MaxLength(60)]
        public string LastName { get; set; } = null!;
        [Required, MaxLength(60)]
        public string MotherName { get; set; } = null!;
        [Required, MaxLength(8)]
        public string EnrollmentNum { get; set; } = null!;
        [MaxLength(120)]
        public string? Address { get; set; } = null!;
    }
}
