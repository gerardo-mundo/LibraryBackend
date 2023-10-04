using AutoMapper;
using LibraryBackend.context;
using LibraryBackend.DTO;
using LibraryBackend.DTO.Thesis;
using LibraryBackend.Entities;
using LibraryBackend.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Controllers
{
    [Route("api/thesis")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ThesisController : ControllerBase
    {
        public ApplicationDBContext Context { get; }
        public IMapper Mapper { get; }

        public ThesisController(ApplicationDBContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        // GET: api/<ThesisController>
        [HttpGet]
        public async Task<ActionResult<List<ThesisDTO>>> GetThesis([FromQuery] PaginationDTO paginationDTO)
        {

            var queryable = Context.Thesis.AsQueryable();
            await HttpContext.InsertParametersIntoHeader(queryable);

            var thesis = await queryable.OrderBy(t => t.Year).Paginate(paginationDTO).ToListAsync();
            return Mapper.Map<List<ThesisDTO>>(thesis);
        }

        [HttpGet("{id:int}", Name = "getThesisById")]
        public async Task<ActionResult<ThesisDTO>> GetThesisById(int id)
        {
            var thesis = await Context.Thesis.FirstOrDefaultAsync(t => t.Id == id);
            if (thesis == null) { return NotFound(); }

            return Mapper.Map<ThesisDTO>(thesis);
        }

        [HttpPost]
        public async Task<ActionResult> PostThesis(ThesisCreationDTO thesisCreation)
        {
            var thesisExist = await Context.Thesis.AnyAsync(t => t.AuthorOne == thesisCreation.AuthorOne);
            if(thesisExist) { return BadRequest("La tesis ya se encuentra registrada") ; }

            var thesis = Mapper.Map<Thesis>(thesisCreation);
            Context.Add(thesis);
            await Context.SaveChangesAsync();
            return CreatedAtRoute("getThesisById", new {thesis.Id}, thesis);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutThesis(int id, ThesisCreationDTO thesisCreation)
        {
            var thesis = await Context.Thesis.FirstOrDefaultAsync(t => t.Id == id);
            if (thesis == null) { return NotFound(); }

            thesis = Mapper.Map(thesisCreation, thesis);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchThesis(int id, JsonPatchDocument<ThesisPatchDTO> patchDocument)
        {
            var thesisDB = await Context.Thesis.FirstOrDefaultAsync(t => t.Id == id);
            if(thesisDB == null) { return NotFound(); }

            var thesisDTO = Mapper.Map<ThesisPatchDTO>(thesisDB);
            patchDocument.ApplyTo(thesisDTO);

            var isValid = TryValidateModel(thesisDTO);
            if(!isValid) { return BadRequest(ModelState); }

            Mapper.Map(thesisDTO, thesisDB);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isAdmin")]
        public async Task<ActionResult> DeleteThesis(int id)
        {
            var thesis = await Context.Thesis.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);//Deja de trackear el obj con el Id específico
            if(thesis == null) { return NotFound(); }

            Context.Remove(new Thesis { Id = id });
            await Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
