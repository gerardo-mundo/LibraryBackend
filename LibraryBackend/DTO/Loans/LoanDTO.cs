namespace LibraryBackend.DTO.Loans
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public bool Returned { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DevolutionDate { get; set; }
        public string AccountId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}
