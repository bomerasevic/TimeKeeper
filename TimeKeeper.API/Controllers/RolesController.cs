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
    public class RolesController : BaseController
    {
        public RolesController(TimeKeeperContext context, ILogger<RolesController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation($"Try to get all Roles");
                return Ok(Unit.Roles.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Log.LogInformation($"Try to fetch role with id {id}");
                Role role = Unit.Roles.Get(id);
                if (role == null)
                {
                    Log.LogError($"There is no role with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(role.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Role role)
        {
            try
            {
                Unit.Roles.Insert(role);
                Unit.Save();
                Log.LogInformation($"Role {role.Name} added with id {role.Id}");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Role role)
        {
            try
            {
                Unit.Roles.Update(role, id);
                Unit.Save();
                Log.LogInformation($"Role {role.Name} with id {role.Id} has changes.");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Roles.Delete(id);
                Unit.Save();
                Log.LogInformation($"Attempt to delete role with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
    }
}