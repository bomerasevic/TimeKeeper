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
        public CustomersController(TimeKeeperContext context, ILogger<CustomersController> log) : base (context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation("Try to get all Customers");
                return Ok(Unit.Customers.Get().OrderBy(x => x.Name).ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                customer.Status = Unit.CustomerStatuses.Get(customer.Status.Id);
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                Unit.Customers.Update(customer, id);
                Unit.Save();
                Log.LogInformation($"Customer with id {id} updated with body {customer}");
                return Ok(customer.Create());
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error!");
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
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