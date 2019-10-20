using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.API.Factory;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Calendar.Get().OrderBy(x => x.Date).ToList().Select(x => x.Create()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Day day = Unit.Calendar.Get(id);
                if (day == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(day.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}