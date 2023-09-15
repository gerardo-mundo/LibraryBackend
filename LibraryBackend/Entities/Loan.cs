using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LoanDate { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-mm-yyyy}", NullDisplayText = "No date", ApplyFormatInEditMode = true)]
        public DateTime? DevolutionDate { get; set; } = null;

        //Navigation props and FK's
        public List<BorrowedBooks> BorrowedBooks { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
