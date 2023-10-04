namespace LibraryBackend.DTO
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordsPeerPage { get; set; } = 10;
        private readonly int quantityPeerPage = 30;


        public int RecordsPeerage
        {
            get { return recordsPeerPage; }
            set { recordsPeerPage = (value > quantityPeerPage) ? quantityPeerPage : value; }
        }
    }
}
