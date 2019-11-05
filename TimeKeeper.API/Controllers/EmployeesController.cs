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
        public EmployeesController(TimeKeeperContext context) : base(context) { }
        /// <summary>
        /// This method returns all Employees
        /// </summary>
        /// <returns>Returns all Employees</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Log.Info($"Try to get all Employees");
                return Ok(Unit.Employees.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method returns Employee by specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Employee with Id=id</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                //Log.LogInformation($"Try to fetch Employee with Id {id}");
                Employee employee = Unit.Employees.Get(id);
                if (employee == null)
                {
                    Log.Error($"There is no Employee with specified Id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(employee.Create());
                }
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Inserts a new Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Creates a new Employee from request body</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                employee.Status = Unit.EmployeeStatuses.Get(employee.Status.Id);
                employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Insert(employee);
                Unit.Save();
                Log.Info($"Employee {employee.FirstName + " " + employee.LastName} added with Id {employee.Id}");
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Updates Employee with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns>Employee with Id=id is Updated</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                employee.Status = Unit.EmployeeStatuses.Get(employee.Status.Id);
                employee.Position = Unit.EmployeePositions.Get(employee.Position.Id);
                Unit.Employees.Update(employee, id);
                Unit.Save();
                Log.Info($"Employee {employee.FirstName + " " + employee.LastName} with Id {employee.Id} has changes.");
                return Ok(employee.Create());
            }
            catch (ArgumentNullException ae)
            {
                Log.Error($"There is no Employee with specified Id {id}");
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Deletes Employee with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee with Id=id is Deleted</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Employees.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete Employee with Id {id}");
                return NoContent();
            }
            catch(ArgumentNullException ae)
            {
                Log.Error($"There is no Employee with specified Id {id}");
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
    }
}