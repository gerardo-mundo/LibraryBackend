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
    }
}
