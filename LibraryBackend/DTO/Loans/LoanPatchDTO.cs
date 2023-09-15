using System.ComponentModel.DataAnnotations;

namespace LibraryBackend.DTO.Loans
{
    public class LoanPatchDTO
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-mm-yyyy}", NullDisplayText = "No date", ApplyFormatInEditMode = true)]
        public DateTime DevolutionDate { get; set; } = DateTime.Now;
    }
}
