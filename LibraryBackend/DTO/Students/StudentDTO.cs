namespace LibraryBackend.DTO.Students
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? SecondName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public string EnrollmentNum { get; set; } = null!;
        public string? Adress { get; set; } = null!;
    }
}
