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
using TimeKeeper.DTO.Factory;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseController
    {
        public CustomersController(TimeKeeperContext context) : base(context) { }
        /// <summary>
        /// This method returns all Customers
        /// </summary>
        /// <returns>Returns all Customers</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
                if (role == "user") return Unauthorized("Access denied!");
                var query = Unit.Customers.Get();
                if (role != "admin")
                {
                    var empid = (User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
                    var employee = Unit.Employees.Get(int.Parse(empid));
                    var teams = employee.Memberships.GroupBy(x => x.Team.Id).Select(y => y.Key).ToList();
                    List<Project> projects = new List<Project>();
                    foreach (var team in teams)
                    {
                        // dodaje listu u listu
                        projects.AddRange(Unit.Projects.Get(x => x.Team.Id == team));
                    }
                    List<Customer> customers = new List<Customer>();
                    foreach (var project in projects)
                    {
                        customers.Add(project.Customer);
                    }
                    return Ok(customers.Select(x => x.Create()).ToList());
                }
                Log.Info("Fetching list of customers");
                return Ok(query.ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns Customer with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Customer with Id=id</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "IsCustomer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                Log.Info($"Try to get Customer with {id} ");
                Customer customer = Unit.Customers.Get(id);
                return Ok(customer.Create());
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Inserts new Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Creates new Customer from request body</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                Unit.Customers.Insert(customer);
                Unit.Save();
                Log.Info($"Customer {customer.Name} added with id {customer.Id}");
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Updates Customer with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns>Customer with Id=id is Updated</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                Unit.Customers.Update(customer, id);
                Unit.Save();
                Log.Info($"Customer with id {id} updated with body {customer}");
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method Deletes Customer with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer with Id=id is Deleted</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="404">Status 404 Not Not Found</response>
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
                Unit.Customers.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete Customer with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}