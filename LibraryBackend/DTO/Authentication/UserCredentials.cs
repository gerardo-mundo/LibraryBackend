using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Authentication
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string Email { get; set;}
        [Required]
        public string Password { get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, MaxLength(8)]
        public string EmployeeKey { get; set; }
    }
}
