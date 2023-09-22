using LibraryBackend.Utilities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace LibraryBackend.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required, Column(TypeName = "nvarchar(8)")]
        public EmployeeTypes Type { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; } = null!;
        [MaxLength(20)]
        public string? SecondName { get; set; } = null!;
        [Required, MaxLength(20)]
        public string LastName { get; set; } = null!;
        [Required, MaxLength(20)]
        public string MotherName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required, MaxLength(10)]
        public string EmployeeKey { get; set; } = null!;
        public List<Loan> Loans { get; set; } = null!;
        public string? UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
