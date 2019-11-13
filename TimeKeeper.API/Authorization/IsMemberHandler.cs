using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Authorization
{
    public class IsMemberHandler : AuthorizationHandler<IsMemberRequirement>
    {
        protected UnitOfWork Unit;
        public IsMemberHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsMemberRequirement requirement)
        {
            //var filterContext = context.Resource as AuthorizationFilterContext;
            //var teamid = Convert.ToInt32(filterContext.RouteData.Values["id"]);
            //var empid = context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            //var team = await Unit.Teams.Get(teamid);
            //if (filterContext == null || !int.TryParse(teamid.ToString(), out int teamId) || team == null ||
            //    !int.TryParse(empid, out int empId))
            //{
            //    context.Fail();
            //    await Task.CompletedTask;
            //}
            //if (team.Members.Any(x => x.Employee.Id == Convert.ToInt32(empid)))
            //{
            //    context.Succeed(requirement);
            //    await Task.CompletedTask;
            //}
            //context.Fail();
            //await Task.CompletedTask;
            var role = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value.ToString();
            if (role == "admin" || role == "lead")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int teamId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            Team team = Unit.Teams.Get(teamId);

            if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (team.TeamMembers.Any(x => x.Employee.Id == empId))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
