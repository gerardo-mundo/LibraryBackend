using LibraryBackend.Utilities.Enums;

namespace LibraryBackend.DTO.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? SecondName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string EmployeeKey { get; set; } = null!;
    }
}
