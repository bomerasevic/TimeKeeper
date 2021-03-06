﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.API.Factory;
using Microsoft.AspNetCore.Authorization;

namespace TimeKeeper.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController
    {
        public MembersController(TimeKeeperContext context) : base(context) { }
        /// <summary>
        /// This method returns all Members
        /// </summary>
        /// <returns>Returns all Members</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            try
            {
                Log.Info($"Try to get all Members");
                return Ok(Unit.Members.Get().ToList().Select(x=>x.Create()).ToList());
            }
            catch(Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method returns Member with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Member with Id=id</returns>
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
                Log.Info($"Try to fetch member with id {id}");
                Member member = Unit.Members.Get(id);
                if (member == null)
                {
                    Log.Error($"There is no project with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(member.Create());
                }
            }
            catch (Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Inserts a new Member
        /// </summary>
        /// <param name="member"></param>
        /// <returns>Creates a new Member from request body</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Member member)
        {
            try
            {
                member.Team = Unit.Teams.Get(member.Team.Id);
                member.Employee = Unit.Employees.Get(member.Employee.Id);
                member.Role = Unit.Roles.Get(member.Role.Id);
                member.Status = Unit.MemberStatuses.Get(member.Status.Id);
                Unit.Members.Insert(member);
                Unit.Save();
                Log.Info($"Member added with id {member.Id}");
                return Ok(member.Create());
            }
            catch(Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// This method Updates Member with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="member"></param>
        /// <returns>Member with Id=id is Updated</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Member member)
        {
            try
            {
                member.Team = Unit.Teams.Get(member.Team.Id);
                member.Employee = Unit.Employees.Get(member.Employee.Id);
                member.Role = Unit.Roles.Get(member.Role.Id);
                member.Status = Unit.MemberStatuses.Get(member.Status.Id);
                Unit.Members.Update(member, id);
                Unit.Save();
                Log.Info($"Member with id {member.Id} has changes.");
                return Ok(member.Create());
            }
            catch(ArgumentNullException ae)
            {
                Log.Error($"There is no Member with specified Id {id}");
                return NotFound();
            }
            catch(Exception ex)
            {
                Log.Fatal("Server error");
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// Member with specified Id is Deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Member with Id=id is Deleted</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 204 Bad Request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Members.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete project with id {id}");
                return NoContent();
            }
            catch(ArgumentNullException ae)
            {
                Log.Error($"There is no Member with specified Id {id}");
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