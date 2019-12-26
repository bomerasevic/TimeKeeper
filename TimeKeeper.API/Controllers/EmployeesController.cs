using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.Utility;
using Newtonsoft.Json;
using TimeKeeper.DTO.Factory;

namespace TimeKeeper.API.Controllers
{
    [Authorize(AuthenticationSchemes = "TokenAuthentication")]
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
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                Log.Info($"Try to get all Employees");
                //int totalItems = Unit.Employees.Get().Count();
                //int totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
                //if (page < 1) page = 1;
                //if (page > totalPages) page = totalPages;
                //int currentPage = page - 1;
                //var query = Unit.Employees.Get().Skip(currentPage * pageSize).Take(pageSize);
                //var pagination = new
                //{
                //    pageSize,
                //    totalItems,
                //    totalPages,
                //    page
                //};
                //HttpContext.Response.Headers.Add("pagination", JsonConvert.SerializeObject(pagination));
                return Ok(Unit.Employees.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
                Log.Info($"Try to get Employee with {id} ");
                Employee employee = Unit.Employees.Get(id);
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                Unit.Employees.Insert(employee);
                User user = UsersUtility.CreateUser(employee);
                Unit.Users.Insert(user);
                Unit.Save();
                Log.Info($"Employee {employee.FullName} added with Id {employee.Id}");
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsEmployee")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                Unit.Employees.Update(employee, id);
                Unit.Save();
                Log.Info($"Employee {employee.FullName} with Id {employee.Id} has changes.");
                return Ok(employee.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
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
        [Authorize(Policy = "IsAdmin")]
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
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}