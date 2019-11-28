using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Services;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseController
    {
        public CalendarService calendarService;
        public CalendarController(TimeKeeperContext context) : base(context)
        {
            calendarService = new CalendarService(Unit);
        }

        /// <summary>
        /// This method returns all Days
        /// </summary>
        /// <returns>Returns all Days</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpGet("{empId}/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int empId, int year, int month)   
        {
            try
            {
                Log.Info("Try to get all Days");
                return Ok(calendarService.GetEmployeeMonth(empId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("employee-report/{empId}/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeReport(int empId, int year, int month)   
        {
            try
            {
                Log.Info($"Try to get report for employee with id:{empId}");
                return Ok(calendarService.CreateEmployeeReport(empId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("project-history/{projectId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetProjectHistory(int projectId)   
        {
            try
            {
                Log.Info($"Try to get project history for project with id:{projectId}");
                return Ok(calendarService.GetProjectHistoryModel(projectId));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("admin-dashboard/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAdminDashboard(int year, int month)   // rutu dodati /employees/month/day/year
        {
            try
            {
                //Log.Info($"Try to get report for employee with id:{empId}");
                return Ok(calendarService.GetAdminDashboardModel(year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns Day with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Day with Id=id</returns>
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
                Log.Info($"Try to get Day with {id} ");
                Day day = Unit.Calendar.Get(id);
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [AllowAnonymous]
        [HttpGet("team-time-tracking/{teamId}/{year}/{month}")]
        public IActionResult GetTimeTracking(int teamId, int year, int month)
        {
            try
            {
                return Ok(calendarService.GetTeamMonthReport(teamId, year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Inserts new Day
        /// </summary>
        /// <param name="day"></param>
        /// <returns>Creates new Day from request body</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] Day day)
        {
            try
            {
                Unit.Calendar.Insert(day);
                Unit.Save();
                Log.Info($"Day added with id {day.Id}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Updates Day with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="day"></param>
        /// <returns>Day with Id=id is Updated</returns>
        /// <response status="200">Status 200 OK</response>
        /// <response status="404">Status 404 Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Put(int id, [FromBody] Day day)
        {
            try
            {
                Unit.Calendar.Update(day, id);
                Unit.Save();
                Log.Info($"Day with id {id} updated with body {day}");
                return Ok(day.Create());
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method Deletes Day with specified Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Day with Id=id is Deleted</returns>
        /// <response status="204">Status 204 No Content</response>
        /// <response status="404">Status 404 Not Not Found</response>
        /// <response status="400">Status 400 Bad Request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                Unit.Calendar.Delete(id);
                Unit.Save();
                Log.Info($"Attempt to delete Day with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}