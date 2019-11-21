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
    public class IsMemberInTeamHandler: AuthorizationHandler<HasAccessToMembers>
    {
        protected UnitOfWork Unit;
        public IsMemberInTeamHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAccessToMembers requirement)
        {
            try
            {
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

                if (!int.TryParse(filterContext.RouteData.Values["id"].ToString(), out int memberId))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                Member member = Unit.Members.Get(memberId);

                if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                if (member.Team.TeamMembers.Any(x => x.Employee.Id == empId))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                context.Fail();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                context.Fail();
                return Task.CompletedTask;
            }            
        }
    }
}
