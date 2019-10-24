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
    public class CustomersController : BaseController
    {
        public CustomersController(TimeKeeperContext context, ILogger<CustomersController> log) : base(context, log) { }
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
                Log.LogInformation("Try to get all Customers");
                return Ok(Unit.Customers.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                Log.LogInformation($"Try to get Customer with {id} ");
                Customer customer = Unit.Customers.Get(id);
                if (customer == null)
                {
                    Log.LogInformation($"Customer with id {id} not found");
                    return NotFound();
                }
                else
                {
                    return Ok(customer.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);
                customer.Address = new CustomerAddress
                {
                    Road = customer.Address.Road,
                    ZipCode = customer.Address.ZipCode,
                    City = customer.Address.City,
                    Country = customer.Address.Country
                };
                Unit.Customers.Insert(customer);
                Unit.Save();
                Log.LogInformation($"Customer {customer.Name} added with id {customer.Id}");
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Updates Customer with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns>Customer with Id=id is Updated</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);
                customer.Address = new CustomerAddress
                {
                    Road = customer.Address.Road,
                    ZipCode = customer.Address.ZipCode,
                    City = customer.Address.City,
                    Country = customer.Address.Country
                };
                Unit.Customers.Update(customer, id);
                Unit.Save();
                Log.LogInformation($"Customer with id {id} updated with body {customer}");
                return Ok(customer.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Deletes Customer with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer with Id=id is Deleted</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Customers.Delete(id);
                Unit.Save();
                Log.LogInformation($"Attempt to delete Customer with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
            }
        }
    }
}