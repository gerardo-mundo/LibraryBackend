using Microsoft.AspNetCore.Identity;

namespace LibraryBackend.Utilities
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string EmployeeKey { get; set; } = string.Empty!;
    }
}
