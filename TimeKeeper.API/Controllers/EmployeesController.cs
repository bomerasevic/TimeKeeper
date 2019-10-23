using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        public EmployeesController(TimeKeeperContext context, ILogger<EmployeesController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Employees.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Employee employee = Unit.Employees.Get(id);
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(employee.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                employee.Status = Unit.EmployeeStatuses.Get(employee.Status.Id);
                employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Insert(employee);
                Unit.Save();
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                employee.Status = Unit.EmployeeStatuses.Get(employee.Status.Id);
                employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Update(employee, id);
                Unit.Save();
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Employees.Delete(id);
                Unit.Save();
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}