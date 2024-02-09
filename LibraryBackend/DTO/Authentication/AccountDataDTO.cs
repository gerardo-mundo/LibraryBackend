using LibraryBackend.Utilities;

namespace LibraryBackend.DTO.Authentication
{
    public class AccountDataDTO
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string EmployeeKey { get; set; } = null!;
    }
}
