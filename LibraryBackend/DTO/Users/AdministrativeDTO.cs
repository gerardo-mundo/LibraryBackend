namespace LibraryBackend.DTO.Users
{
    public class AdministrativeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public string EmployeeKey { get; set; } = null!;
    }
}
