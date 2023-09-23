using Microsoft.AspNetCore.Identity;

namespace LibraryBackend.Utilities
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string EmployeKey { get; set; } = string.Empty!;
    }
}
