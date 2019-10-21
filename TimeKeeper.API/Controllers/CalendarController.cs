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
    public class CalendarController : BaseController
    {
        public CalendarController(TimeKeeperContext context, ILogger<BaseController> log) : base(context, log) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Log.LogInformation($"Try to get all Days");
                return Ok(Unit.Calendar.Get().ToList().Select(x => x.Create()).ToList());
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
                Log.LogInformation($"Try to fetch day with id {id}");
                Day day = Unit.Calendar.Get(id);
                if (day == null)
                {
                    Log.LogError($"There is no day with specified id {id}");
                    return NotFound();
                }
                else
                {
                    return Ok(day.Create());
                }
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Day day)
        {
            try
            {
                Unit.Calendar.Insert(day);
                Unit.Save();
                Log.LogInformation($"Day added with id {day.Id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "Server error");
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Day day)
        {
            try
            {
                Unit.Calendar.Update(day, id);
                Unit.Save();
                Log.LogInformation($"Day with id {day.Id} has changes.");
                return Ok(day.Create());
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
                Unit.Calendar.Delete(id);
                Unit.Save();
                Log.LogInformation($"Attempt to delete day with id {id}");
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