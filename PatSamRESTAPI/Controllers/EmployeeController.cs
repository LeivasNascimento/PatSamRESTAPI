using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatSamRESTAPI.Contexts;
using PatSamRESTAPI.Models;

namespace PatSamRESTAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly PatSamDbContext _context;
        public EmployeeController(PatSamDbContext context)
        {
            _context = context;
        }

        // GET: api/employee

        [HttpGet]
        public async Task<IActionResult> GetEmployee() 
        {
            try
            {
                var employee = await _context.Employee
               .AsNoTracking()
               .ToListAsync();
                return await Task.FromResult(new JsonResult(employee));
            }
            catch (Exception ex )
            {

                throw;
            }

            return null;
           

        }
        // GET: api/employee/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return new NotFoundObjectResult($"Employee with Id {id} not found.");
            }

            return await Task.FromResult(new JsonResult(employee));
        }

        // PUT: api/employees/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await Task.FromResult(new JsonResult(employee));
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            var newEmployee = _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
            return await Task.FromResult(new OkObjectResult($"Successfully created employee {employee.Id}."));
        }

        // DELETE: api/employee/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return new NotFoundObjectResult($"Employee with Id {id} not found.");
            }

            _context.Employee.Remove(employee);
            var result = await _context.SaveChangesAsync();
            var statusCode = result > 0 ? 200 : 500;
            return await Task.FromResult(new StatusCodeResult(statusCode));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
