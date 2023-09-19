using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryBackend.Entities;
using LibraryBackend.context;
using AutoMapper;
using LibraryBackend.DTO.Employees;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LibraryBackend.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDBContext Context;
        private readonly IMapper Mapper;

        public EmployeeController(ApplicationDBContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployees()
        {
            var employees = await Context.Employees.ToListAsync();
            return Mapper.Map<List<EmployeeDTO>>(employees);
        }

        [HttpGet("{id}", Name = "getEmployeById")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await Context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) { return NotFound(); }

            return Mapper.Map<EmployeeDTO>(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutEmployee(int id, EmployeeCreationDTO employeeCreation)
        {
            var employee = await Context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) { return NotFound(); }

            employee = Mapper.Map(employeeCreation, employee);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> PostEmployee(EmployeeCreationDTO employeeCreation)
        {
          var employeeExist = await Context.Employees.FirstOrDefaultAsync(e => 
          e.EmployeeKey == employeeCreation.EmployeeKey);

          if (employeeExist != null)
          {
              return BadRequest($"El numero de empleado {employeeCreation.EmployeeKey} ya está registrado");
          }
            Employee employee = Mapper.Map<Employee>(employeeCreation);
            Context.Add(employee);
            await Context.SaveChangesAsync();

            return CreatedAtRoute("getEmployeById", new { id = employee.Id }, employee);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if(!await EmployeeExists(id)) { return NotFound(); }

            Context.Remove(new Employee { Id = id});
            await Context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> EmployeeExists(int id)
        {
            return await Context.Employees.AnyAsync(e => e.Id == id);
        }
    }
}
