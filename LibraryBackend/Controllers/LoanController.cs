using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using LibraryBackend.context;
using AutoMapper;
using LibraryBackend.DTO.Loans;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LibraryBackend.Utilities;

namespace LibraryBackend.Controllers
{
    [Route("api/loans")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDBContext Context;
        private readonly UserManager<ApplicationIdentityUser> UserManager;
        public IMapper Mapper { get; }

        public LoanController(ApplicationDBContext context, 
            IMapper mapper, 
            UserManager<ApplicationIdentityUser> userManager)
        {
            Context = context;
            Mapper = mapper;
            UserManager = userManager;
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
            if (loan == null) { return NotFound(); }

            return Mapper.Map<LoanDTOWithBorrowedBooks>(loan);
        }

        [HttpPost]
        public async Task<ActionResult> PostLoan(LoanCreationDTO loanCreation)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
                var email = emailClaim?.Value;
                var account = await UserManager.FindByEmailAsync(email);
                var accountId = account.Id;           
            
            var userExist = await Context.Users.AnyAsync(u => u.Id == loanCreation.UserId);

            var adquisitionsList = await Context.Books.Where(book => loanCreation.BorrowedBooks.Contains(book.Adquisition))
                .Select(b => b.Adquisition)
                .ToListAsync();

            if (adquisitionsList.Count != loanCreation.BorrowedBooks.Count)
            {
                return BadRequest("Uno o varios de los libros no existen");
            }

            if (!userExist || accountId == null) { return NotFound("El usuario o empleado no se encuentra registrado"); }

            Loan loan = Mapper.Map<Loan>(loanCreation);
            loan.AccountId = accountId;
            Context.Add(loan);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("id:int")]
        public async Task<ActionResult> PatchLoan(int id, [FromBody] JsonPatchDocument<LoanPatchDTO> patchDocument)
        {
            if(patchDocument == null) { return BadRequest("Hizo falta un elemento para actualizar"); }
            var loanDB = await Context.Loans.FirstOrDefaultAsync(l => l.Id == id);
            if (loanDB == null) { return NotFound(); }


            var loanDTO = Mapper.Map<LoanPatchDTO>(loanDB);

            patchDocument.ApplyTo(loanDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(loanDTO, loanDB);
            await Context.SaveChangesAsync();
            return NoContent();

            /*
             Example Input data: 
            [
              {
                "path": "/DevolutionDate",
                "op": "replace",
                "value": "2023-09-15"
              },
              {
                "path": "/Returned",
                "op": "replace",
                "value": true
              }
            ]
             */
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loanExist = await Context.Loans.AnyAsync(l => l.Id == id);
            if (!loanExist) { return NotFound(); }

            Context.Remove(new Loan() { Id = id });
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}
