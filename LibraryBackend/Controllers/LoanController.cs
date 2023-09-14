using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using LibraryBackend.context;
using AutoMapper;
using LibraryBackend.DTO.Loans;
using System.Linq;
using LibraryBackend.DTO.BorrowedBooks;

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            if (Context.Loans == null)
            {
                return NotFound();
            }
            var loan = await Context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
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
            if (!userExist || !employeeExist) { return BadRequest("El usuario o empleado no se encuentran registrados"); }

            Loan loan = Mapper.Map<Loan>(loanCreation);
            Context.Add(loan);
            await Context.SaveChangesAsync();

            return Ok(loan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            if (Context.Loans == null)
            {
                return NotFound();
            }
            var loan = await Context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            Context.Loans.Remove(loan);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return (Context.Loans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
