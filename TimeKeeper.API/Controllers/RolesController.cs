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
        public RolesController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method get data of all Roles
        /// </summary>
        /// <returns>Data of all Roles</returns>
        /// <response status="200">Status OK</response>
        /// <response status="400">Status Not OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Log.Info($"Try to get all Roles");
                return Ok(Unit.Roles.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns Role by specified Id
        /// </summary>
        /// <param name="id">Role value by specified Id</param>
        /// <returns>Role values by specified Id</returns>
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
                Log.Info($"Try to get Role with {id} ");
                Role role = Unit.Roles.Get(id);
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method sets values of new Role
        /// </summary>
        /// <param name="role">Data which comes from frontend</param>
        /// <returns>New Role values</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Role role)
        {
            try
            {
                Unit.Roles.Insert(role);
                Unit.Save();
                Log.Info($"Role {role.Name} added with id {role.Id}");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method updates Roles data
        /// </summary>
        /// <param name="id">ID of Role which we wish to Update</param>
        /// <param name="role">Data which comes from frontend</param>
        /// <returns>Role with new value of ID</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Role role)
        {
            try
            {
                Unit.Roles.Update(role, id);
                Unit.Save();
                Log.Info($"Role {role.Name} with id {role.Id} has changes.");
                return Ok(role.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method attempt to delete Role
        /// </summary>
        /// <param name="id">ID of Role which we wish to Delete</param>
        /// <returns>Team with new value of ID</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Roles.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete role with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}