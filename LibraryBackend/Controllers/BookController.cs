using LibraryBackend.DTO.Books;
using LibraryBackend.context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDBContext Context;
        private readonly IMapper Mapper;

        public BookController(ApplicationDBContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> PostBook(BookCreationDTO bookCreationDTO)
        {
            bool bookExist = await Context.Books.AnyAsync(b => b.Adquisition == bookCreationDTO.Adquisition);
            string yearString = bookCreationDTO.Year.ToString();
            string adquisitionString = bookCreationDTO.Adquisition.ToString();

            if (bookCreationDTO == null || bookExist)
            {
                return BadRequest("El libro ya existe o falta algún elemento.");
            }

            if (yearString.Length != 4)
            {
                return BadRequest($"{yearString} No es un formato de año válido.");
            }

            if (adquisitionString.Length > 5)
            {
                return BadRequest($"{adquisitionString} No es un formato válido de adquisición.");
            }

            Book book = Mapper.Map<Book>(bookCreationDTO);
            Context.Add(book);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var books = await Context.Books.ToListAsync();
            return Mapper.Map<List<BookDTO>>(books);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutBook(int id, BookCreationDTO bookCreationDTO)
        {
            var bookDb = await Context.Books.FirstOrDefaultAsync(book => book.Id == id);

            if(bookDb == null) { return NotFound(id); }

            bookDb = Mapper.Map(bookCreationDTO, bookDb);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchBook(int id, JsonPatchDocument<BookCreationDTO> patchDocument)
        {
            var bookDB = await Context.Books.FirstOrDefaultAsync(book => book.Id == id);
            if(bookDB == null) { return NotFound($"No se encontró el libro con ID: {id}"); }

           var bookDTO = Mapper.Map<BookCreationDTO>(bookDB);
           patchDocument.ApplyTo(bookDTO);
           var bookValid = TryValidateModel(bookDTO);
            if(!bookValid) { return BadRequest(ModelState); }

            Mapper.Map(bookDTO, bookDB);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var bookExist = await Context.Books.AnyAsync(book => book.Id == id);

            if(!bookExist) { return NotFound($"El libro con el ID {id} no existe."); }

            Context.Remove(new Book { Id = id });
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
