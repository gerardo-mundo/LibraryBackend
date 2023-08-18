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
            bool bookExist = await context.Books.AnyAsync<Book>(b => b.ISBN == bookCreationDTO.ISBN);

            if (bookCreationDTO == null || bookExist)
            {
                return BadRequest("El libro ya existe o falta algún elemento");
            }

            Book book = mapper.Map<Book>(bookCreationDTO);
            context.Add(book);
            await context.SaveChangesAsync();
            return Ok(book);
        }
    }
}
