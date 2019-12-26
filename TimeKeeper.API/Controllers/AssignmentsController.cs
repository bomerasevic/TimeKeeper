using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Factory;

namespace TimeKeeper.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : BaseController
    {
        public AssignmentsController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Log.Info($"Try to get all Assignments!");
                List<Assignment> query;

                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString());
                string role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString();

                if (role == "lead")
                {
                    query = Unit.Assignments.Get(x => x.Project.Team.TeamMembers.Any(y => y.Employee.Id == userId)).ToList();
                }
                else if (role == "user")
                {
                    query = Unit.Assignments.Get(x => x.Day.Employee.Id == userId).ToList();
                }
                else
                {
                    query = Unit.Assignments.Get().ToList();
                }
                return Ok(query.ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method returns a task with specified id
        /// </summary>
        /// <param name="id">Id of role</param>
        /// <returns>Role with specified id</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString());
                string role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString();

                Log.Info($"Try to get assignment with id {id}");
                var assignment = Unit.Assignments.Get(id);
                if (assignment == null)
                {
                    return NotFound($"Assignment with requested id {id} does not exist!");
                }
                if ((role == "lead" && !(assignment.Project.Team.TeamMembers.Any(x => x.Employee.Id == userId))) ||
                    role == "user" && !(assignment.Day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(assignment.Create());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method inserts a new task
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns>Model of inserted task</returns>
        /// <response status="200">OK</response>
        /// <response status="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Assignment assignment)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString());
                string role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString();

                if (role == "lead" && !assignment.Project.Team.TeamMembers.Any(x => x.Employee.Id == userId) ||
                   role == "user" && !(assignment.Day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                else
                {
                    Unit.Assignments.Insert(assignment);
                    Unit.Save();
                    Log.Info($"Assignment added with id {assignment.Id}");
                    return Ok(assignment.Create());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method updates data for a task with the specified id
        /// </summary>
        /// <param name="id">Id of task that will be updated</param>
        /// <param name="assignment">Data that comes from frontend</param>
        /// <returns>Task model with new values</returns>
        /// <response status="200">OK</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Assignment assignment)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value.ToString());
                string role = User.Claims.FirstOrDefault(x => x.Type == "role").Value.ToString();

                if (role == "lead" && !assignment.Project.Team.TeamMembers.Any(x => x.Employee.Id == userId) ||
                   role == "user" && !(assignment.Day.Employee.Id == userId))
                {
                    return Unauthorized();
                }
                else
                {
                    Unit.Assignments.Update(assignment, id);
                    Unit.Save();
                    Log.Info($"Assignment with id {assignment.Id} has changes.");
                    return Ok(assignment.Create());
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// This method deletes a task with the specified id
        /// </summary>
        /// <param name="id">Id of task that has to be deleted</param>
        /// <returns>No content</returns>
        /// <response status="204">No content</response>
        /// <response status="404">Not found</response>
        /// <response status="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Policy = "IsAdmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                Log.Info($"Attempt to delete project with id {id}");
                Unit.Assignments.Delete(id);
                Unit.Save();

                Log.Info($"Task with id {id} deleted successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}

