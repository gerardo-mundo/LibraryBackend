using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBackend.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LoanDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-mm-yyyy}", NullDisplayText = "No date", ApplyFormatInEditMode = true)]
        public DateTime? DevolutionDate { get; set; }

        //Navigation props and FK's
        public List<BorrowedBooks> BorrowedBooks { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
