using AutoMapper;
using LibraryBackend.context;
using LibraryBackend.DTO.Students;
using LibraryBackend.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public StudentController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDTO>>> GetStudents()
        {
            var students = await context.Students.ToListAsync();
            return mapper.Map<List<StudentDTO>>(students);
        }

        [HttpGet("{id:int}", Name = "getStudentById")]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if(student == null) { return NotFound(); }

            return mapper.Map<StudentDTO>(student);
        }

        [HttpPost]
        public async Task<ActionResult> PostStudent(StudentCreationDTO studentCreation)
        {
            bool studentExist = await context.Students.AnyAsync(s => 
                                     s.EnrollmentNum == studentCreation.EnrollmentNum);

            if (studentCreation == null)
            {
                return BadRequest();
            } else if(studentExist)
            {
                return BadRequest("El estudiante ya se encuentra registrado");
            }

            Student student = mapper.Map<Student>(studentCreation);
            context.Add(student);
            await context.SaveChangesAsync();
            return CreatedAtRoute("getStudentById", new {id = student.Id}, student);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutStudent(int id, StudentCreationDTO studentCreation)
        {
            var student = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) { return NotFound(); }

            student = mapper.Map(studentCreation, student);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PatchStudent(int id, JsonPatchDocument<StudentPatchDTO> patchDocument)
        {
            if(patchDocument == null) { return BadRequest(); }
            var studentDB = await context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (studentDB == null) { return NotFound(nameof(studentDB)); }

            var studentDTO = mapper.Map<StudentPatchDTO>(studentDB);
            patchDocument.ApplyTo(studentDTO);

            var isValid = TryValidateModel(studentDTO);
            if(!isValid) { return BadRequest(ModelState); }
            
            mapper.Map(studentDTO, studentDB);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            bool studentExist = await context.Students.AnyAsync(s => s.Id == id);

            if (!studentExist) { return NotFound($"El estudiante con el ID: {id} no existe"); }

            context.Remove( new Student() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
