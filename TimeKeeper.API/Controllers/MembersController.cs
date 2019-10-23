using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.DAL;
using TimeKeeper.Domain;
using TimeKeeper.API.Factory;


namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController
    {
        public MembersController(TimeKeeperContext context, ILogger<EmployeesController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation($"Try to get all Members");
                return Ok(Unit.Members.Get().ToList().Select(x=>x.Create()).ToList());
            }
            catch(Exception ex)
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
                Log.LogInformation($"Try to fetch member with id {id}");
                Member member = Unit.Members.Get(id);
                if (member == null)
                {
                    Log.LogError($"There is no project with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(member.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPost]
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
                Log.LogInformation($"Member added with id {member.Id}");
                return Ok(member.Create());
            }
            catch(Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
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
                Log.LogInformation($"Member with id {member.Id} has changes.");
                return Ok(member.Create());
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
                Unit.Members.Delete(id);
                Unit.Save();
                Log.LogInformation($"Attempt to delete project with id {id}");
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