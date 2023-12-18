using AutoMapper;
using LibraryBackend.context;
using LibraryBackend.DTO;
using LibraryBackend.DTO.Books;
using LibraryBackend.DTO.Publications;
using LibraryBackend.Entities;
using LibraryBackend.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("api/publications")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PublicationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDBContext context;

        public PublicationController(IMapper mapper, ApplicationDBContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostPublication(PublicationCreationDTO publicationCreationDTO)
        {
            var publicationExist = await context.Publications.AnyAsync(p => p.ISBN == publicationCreationDTO.ISBN 
            && p.ISSN == publicationCreationDTO.ISSN);

            if(publicationExist)
            {
                return BadRequest("El elemento ya se encuentra registrado");
            }

            Publication publication = mapper.Map<Publication>(publicationCreationDTO);
            context.Add(publication);
            context.SaveChanges();

            PublicationDTO publicationDTO = mapper.Map<PublicationDTO>(publication);
            return CreatedAtRoute("getPublicationById", new {publication.Id}, publicationDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdatePublication(int id, PublicationCreationDTO publicationCreationDTO)
        {
            var publication = await context.Publications.FirstOrDefaultAsync(p => p.Id == id);
            if(publication == null) { return NotFound(); }

            publication = mapper.Map(publicationCreationDTO, publication);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchPublication(int id, JsonPatchDocument<PublicationPatchDTO> patchDocument )
        {
            if(patchDocument == null) { return BadRequest(); }

            var publicationDB = await context.Publications.FirstOrDefaultAsync(p => p.Id == id);

            if(publicationDB == null) { return NotFound(); }

            var publicationDTO = mapper.Map<PublicationPatchDTO>(publicationDB);

            patchDocument.ApplyTo(publicationDTO);

            var isValid = TryValidateModel(publicationDTO);
            if(!isValid) { return BadRequest(ModelState); }

            mapper.Map(publicationDTO, publicationDB);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<ActionResult> DeletePublication(int id)
        {
            bool publicationExist = await context.Publications.AnyAsync(p => p.Id == id);
            if(!publicationExist) { return NotFound($"El elemento con ID: {id} no existe"); }

            context.Remove(new Publication { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet, Route("paginated")]
        public async Task<ActionResult<List<PublicationDTO>>> GetPublications([FromQuery] PaginationDTO paginationDTO)
        {

            var queryable = context.Publications.AsQueryable();
            await HttpContext.InsertParametersIntoHeader(queryable);

            var publications = await queryable.OrderBy(p => p.Year).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<PublicationDTO>>(publications);
        }

        [HttpGet]
        public async Task<ActionResult<List<PublicationDTO>>> GetListBooks()
        {
            var publications = await context.Publications.OrderBy(b => b.Year).ToListAsync();
            return mapper.Map<List<PublicationDTO>>(publications);
        }

        [HttpGet("{id:int}", Name = "getPublicationById")]
        public async Task<ActionResult<PublicationDTO>> GetPublicationById(int id)
        {
            var publication = await context.Publications.FirstOrDefaultAsync(p => p.Id == id);

            if(publication == null) { return NotFound(); }

            return mapper.Map<PublicationDTO>(publication);
        }
    }
}
