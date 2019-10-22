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
    public class TeamsController : BaseController
    {
        public TeamsController(TimeKeeperContext context, ILogger<TeamsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation($"Try to get all Teams");
                return Ok(Unit.Teams.Get().ToList().Select(x => x.Create()).ToList());
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
                Log.LogInformation($"Try to fetch team with id {id}");
                Team team = Unit.Teams.Get(id);
                if (team == null)
                {
                    Log.LogError($"There is no team with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(team.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/members")]
        public IActionResult GetMembers(int id)
        {
            try
            {
                Log.LogInformation($"Try to fetch team members for team with id {id}");
                Team team = Unit.Teams.Get(id);
                if(team == null)
                {
                    Log.LogError($"There is no team with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(
                        new
                        {
                            team.Id,
                            team.Name,
                            Members = team.TeamMembers.Select(x => x.Create())
                        });
                }
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Team team)
        {
            try
            {
                Unit.Teams.Insert(team);
                Unit.Save();
                Log.LogInformation($"Team {team.Name} added with id {team.Id}");
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Ovaj metod azurira podatke za team
        /// </summary>
        /// <param name="team">Podaci koji dodju sa frontenda</param>
        /// <returns>Inserted data with generated Id</returns>
        /// <response code="200">valja</response>
        /// <response code="400">ne valja</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Team team)
        {
            try
            {
                Unit.Teams.Update(team, id);
                Unit.Save();
                Log.LogInformation($"Team {team.Name} with id {team.Id} has changes.");
                return Ok(team.Create());
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
                Unit.Teams.Delete(id);
                Unit.Save();
                Log.LogInformation($"Attempt to delete team with id {id}");
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