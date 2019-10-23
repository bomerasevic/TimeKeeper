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
    public class ProjectsController : BaseController
    {
        public ProjectsController(TimeKeeperContext context, ILogger<ProjectsController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation($"Try to get all Projects");
                return Ok(Unit.Projects.Get().ToList().Select(x => x.Create()).ToList());
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
                Log.LogInformation($"Try to fetch project with id {id}");
                Project project = Unit.Projects.Get(id);
                if (project == null)
                {
                    Log.LogError($"There is no project with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(project.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            try
            {
                project.Team = Unit.Teams.Get(project.Team.Id);
                project.Customer = Unit.Customers.Get(project.Customer.Id);
                project.Status = Unit.ProjectStatuses.Get(project.Status.Id);
                project.Pricing = Unit.ProjectPrices.Get(project.Pricing.Id);
                Unit.Projects.Insert(project);
                Unit.Save();
                Log.LogInformation($"Project {project.Name} added with id {project.Id}");
                return Ok(project.Create());
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Project project)
        {
            try
            {
                project.Team = Unit.Teams.Get(project.Team.Id);
                project.Customer = Unit.Customers.Get(project.Customer.Id);
                project.Status = Unit.ProjectStatuses.Get(project.Status.Id);
                project.Pricing = Unit.ProjectPrices.Get(project.Pricing.Id);
                Unit.Projects.Update(project, id);
                Unit.Save();
                Log.LogInformation($"Project {project.Name} with id {project.Id} has changes.");
                return Ok(project.Create());
            }
            catch(Exception ex)
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
                Unit.Projects.Delete(id);
                Unit.Save();
                Log.LogInformation($"Attempt to delete project with id {id}");
                return NoContent();
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
    }
}