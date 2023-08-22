using LibraryBackend.DTO.Books;
using LibraryBackend.context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public BookController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> PostBook(BookCreationDTO bookCreationDTO)
        {
            bool bookExist = await context.Books.AnyAsync(b => b.ISBN == bookCreationDTO.ISBN);
            string yearBookString = bookCreationDTO.Year.ToString();
            string isbnBookString = bookCreationDTO.ISBN.ToString();

            if (yearBookString.Length != 4)
            {
                return BadRequest($"{yearBookString} No es un formato de año válido.");
            }

            if (isbnBookString.Length > 13 || isbnBookString.Length < 9)
            {
                return BadRequest($"{isbnBookString} No es un formato válido de ISBN.");
            }

            if (bookCreationDTO == null || bookExist)
            {
                return BadRequest("El libro ya existe o falta algún elemento.");
            }

            Book book = mapper.Map<Book>(bookCreationDTO);
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var books = await context.Books.ToListAsync();
            return mapper.Map<List<BookDTO>>(books);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutBook(int id, BookCreationDTO bookCreationDTO)
        {
            var bookDb = await context.Books.FirstOrDefaultAsync(book => book.Id == id);

            if(bookDb == null) { return NotFound(id); }

            bookDb = mapper.Map(bookCreationDTO, bookDb);
            await context.SaveChangesAsync();
            return Ok(bookDb);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var bookExist = await context.Books.AnyAsync(book => book.Id == id);

            if(!bookExist) { return NotFound($"El libro con el ID {id} no existe."); }

            context.Remove(new Book { Id = id });
            await context.SaveChangesAsync();
            return Ok("Se ha eliminado.");
        }
    }
}
