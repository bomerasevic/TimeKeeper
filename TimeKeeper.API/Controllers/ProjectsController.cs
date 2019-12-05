using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DTO.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        public ProjectsController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method get data of all Projects
        /// </summary>
        /// <returns>Data of all projects</returns>
        /// <response status="200">Status OK</response>
        /// <response status="400">Status Not OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString());
                string role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString();
                if (role == "admin" || role == "lead")
                {
                    Log.Info($"Try to get all Projects");
                    return Ok(Unit.Projects.Get().ToList().Select(x => x.Create()).ToList());
                }
                else
                {
                    var query = Unit.Projects.Get(x => x.Team.TeamMembers.Any(y => y.Employee.Id == userId));
                    return Ok(query.ToList().Select(x => x.Create()).ToList());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns Project by specified Id
        /// </summary>
        /// <param name="id">Project value by specified Id</param>
        /// <returns>Project values by specified Id</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>        
        [HttpGet("{id}")]
        [Authorize(Policy = "IsMemberOnProject")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Get(int id)
        {
            try
            {
                Log.Info($"Try to get Project with {id} ");
                Project project = Unit.Projects.Get(id);
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method sets values of new Project
        /// </summary>
        /// <param name="role">Data which comes from frontend</param>
        /// <returns>New Role values</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Project project)
        {
            try
            {
                Unit.Projects.Insert(project);
                Unit.Save();
                Log.Info($"Project {project.Name} added with id {project.Id}");
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method updates values of Project
        /// </summary>
        /// <param name="id">ID of Project which we wish to Update</param>
        /// <param name="project">Data which comes from frontend</param>
        /// <returns>Project with new value of ID</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Project project)
        {
            try
            {
                Unit.Projects.Update(project, id);
                Unit.Save();
                Log.Info($"Project {project.Name} with id {project.Id} has changes.");
                return Ok(project.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method attempt to delete Project
        /// </summary>
        /// <param name="id">ID of Project which we wish to Delete</param>
        /// <returns>Team with new value of ID</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "IsAdmin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Projects.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete project with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}