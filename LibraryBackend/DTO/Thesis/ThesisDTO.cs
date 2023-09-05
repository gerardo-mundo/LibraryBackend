namespace LibraryBackend.DTO.Thesis
{
    public class ThesisDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string AuthorOne { get; set; } = null!;
        public string? AuthorTwo { get; set; }
        public string? AuthorThree { get; set; }
        public string Assessor { get; set; } = null!;
        public int Year { get; set; }
    }
}
