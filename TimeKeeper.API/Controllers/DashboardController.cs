using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeKeeper.BLL.Services;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        public DashboardService dashboardService;
        public DashboardController(TimeKeeperContext context) : base(context)
        {
            dashboardService = new DashboardService(Unit);
        }
        [HttpGet("admin-dashboard/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAdminDashboard(int year, int month)
        {
            try
            {
                Log.Info($"Try to get dashboard for admin");
                //return Ok(dashboardService.GetAdminDashboardInfo(year, month));
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("admin-dashboard-stored/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAdminDashboardStored(int year, int month)
        {
            try
            {
                Log.Info($"Try to get dashboard for admin");
                return Ok(dashboardService.GetAdminDashboardStored(year, month));
                //return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("team-dashboard/{teamId}/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetTeamDashboard(int teamId, int year, int month)
        {
            try
            {
                Log.Info($"Try to get dashboard for team with id:{teamId}");
                //return Ok(dashboardService.GetTeamDashboard(teamId, year, month));
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("team-dashboard-stored/{teamId}/{year}/{month}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetTeamDashboardStored(int teamId, int year, int month)
        {
            try
            {
                Log.Info($"Try to get dashboard for team with id:{teamId}");
                return Ok(dashboardService.GetTeamDashboardStored(Unit.Teams.Get(teamId), year, month));
                //return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// This method returns all Days for employee for specified year and month
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
                return Ok(dashboardService.GetEmployeeMonth(empId, year, month));
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
                return Ok(dashboardService.CreateEmployeeReport(empId, year, month));
                //return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        //[AllowAnonymous]
        [HttpGet("team-time-tracking/{teamId}/{year}/{month}")]
        public IActionResult GetTimeTracking(int teamId, int year, int month)
        {
            try
            {
                return Ok(dashboardService.GetTeamMonthReport(Unit.Teams.Get(teamId), year, month));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        [HttpGet("bradford-factor/{empId}/{year}")]
        public IActionResult GetBradfordFactor(int empId, int year)
        {
            try
            {
                return Ok(dashboardService.GetBradfordFactor(empId, year));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

    }
}