﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(TimeKeeperContext context) : base(context) { }

        /// <summary>
        /// This method get data of all Teams
        /// </summary>
        /// <returns>Data of all teams</returns>
        /// <response status="200">Status OK</response>
        /// <response status="400">Status Not OK</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Log.Info($"Try to get all Teams");
                return Ok(Unit.Teams.Get().ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method returns Team by specified Id
        /// </summary>
        /// <param name="id">Team value by specified Id</param>
        /// <returns>Team values by specified Id</returns>
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
                Log.Info($"Try to fetch team with id {id}");
                Team team = Unit.Teams.Get(id);
                if (team == null)
                {
                    Log.Error($"There is no team with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(team.Create());
                }
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method creates new Team
        /// </summary>
        /// <param name="team">Data which comes from frontend</param>
        /// <returns>New Team values</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Team team)
        {
            try
            {
                Unit.Teams.Insert(team);
                Unit.Save();
                Log.Info($"Team {team.Name} added with id {team.Id}");
                return Ok(team.Create());
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method updates Teams data
        /// </summary>
        /// <param name="id">ID of Team which we wish to Update</param>
        /// <param name="team">Data which comes from frontend</param>
        /// <returns>Team with new value of ID</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Team team)
        {
            try
            {
                Unit.Teams.Update(team, id);
                Unit.Save();
                Log.Info($"Team {team.Name} with id {team.Id} has changes.");
                return Ok(team.Create());
            }
            catch (ArgumentNullException ae)
            {
                Log.Error($"There is no Team with specified Id {id}");
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Attempt to delete Team
        /// </summary>
        /// <param name="id">ID of Team which we wish to Delete</param>
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
                Unit.Teams.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete team with id {id}");
                return NoContent();
            }
            catch (ArgumentNullException ae)
            {
                Log.Error($"There is no Team with specified Id {id}");
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
    }
}