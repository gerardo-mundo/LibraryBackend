using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using LibraryBackend.context;
using AutoMapper;
using LibraryBackend.DTO.Loans;
using System.Linq;
using LibraryBackend.DTO.BorrowedBooks;
using Microsoft.AspNetCore.JsonPatch;

namespace LibraryBackend.Controllers
{
    [Route("api/loans")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDBContext Context;

        public IMapper Mapper { get; }

        public LoanController(ApplicationDBContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoanDTO>>> GetLoans()
        {
            var loans = await Context.Loans.ToListAsync();
            return Mapper.Map<List<LoanDTO>>(loans);
        }

        [HttpGet, Route("get-borrowed-books")]
        public async Task<ActionResult<List<LoanDTOWithBorrowedBooks>>> GetLoansWithBorrowedBooks()
        {
            var loans = await Context.Loans.Include(loandBD => loandBD.BorrowedBooks).ToListAsync();
            return Mapper.Map<List<LoanDTOWithBorrowedBooks>>(loans);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LoanDTOWithBorrowedBooks>> GetLoanById(int id)
        {
            var loan = await Context.Loans.Include(loandBD => loandBD.BorrowedBooks)
                .FirstOrDefaultAsync(l => l.Id == id);
            if(loan == null) { return NotFound(); }

            return Mapper.Map<LoanDTOWithBorrowedBooks>(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(LoanCreationDTO loanCreation)
        {
            var userExist = await Context.Users.AnyAsync(u => u.Id == loanCreation.UserId);
            var employeeExist = await Context.Employees.AnyAsync(e => e.Id == loanCreation.EmployeeId);
            var adquisitionsList = await Context.Books.Where(book => loanCreation.BorrowedBooks.Contains(book.Adquisition))
                .Select(b => b.Adquisition)
                .ToListAsync();

            if (adquisitionsList.Count != loanCreation.BorrowedBooks.Count)
            {
                return BadRequest("Uno o varios de los libros no existen");
            }
            if (!userExist || !employeeExist) { return NotFound("El usuario o empleado no se encuentran registrados"); }

            Loan loan = Mapper.Map<Loan>(loanCreation);
            Context.Add(loan);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("id:int")]
        public async Task<ActionResult> PatchLoan(int id, JsonPatchDocument<LoanPatchDTO> patchDocument)
        {
            var loanDB = await Context.Loans.FirstOrDefaultAsync(l => l.Id == id);
            if (loanDB == null) { return NotFound(nameof(loanDB)); }

            var loanDTO = Mapper.Map<LoanPatchDTO>(loanDB);
            patchDocument.ApplyTo(loanDTO, ModelState);
            
            var isValid = TryValidateModel(loanDTO);
            if(!isValid) { return BadRequest(ModelState); }

            Mapper.Map(loanDTO, loanDB);
            await Context.SaveChangesAsync();
            return NoContent();
            
            /*
             Example Input data: 
            [
              {
                "path": "/DevolutionDate",
                "op": "replace",
                "value": "yyyy-MM-dd",
              }
            ]
             */
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loanExist = await Context.Loans.AnyAsync(l => l.Id == id);
            if (!loanExist) { return NotFound(); }

            Context.Remove(new Loan() {Id = id });
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}
