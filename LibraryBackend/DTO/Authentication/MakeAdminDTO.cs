using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Authentication
{
    public class MakeAdminDTO
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set; }
    }
}
