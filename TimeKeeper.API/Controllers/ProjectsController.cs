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
    public class ProjectsController : BaseController
    {
        public ProjectsController(TimeKeeperContext context) : base(context) { }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Unit.Projects.Get().OrderBy(x => x.Name).ToList().Select(x => x.Create()).ToList());
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
                Project project = Unit.Projects.Get(id);
                if (project == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(project.Create());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}