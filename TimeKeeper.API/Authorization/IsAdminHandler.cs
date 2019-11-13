﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Authorization
{
    public class IsAdminHandler : AuthorizationHandler<IsRoleRequirement>
    {
        protected UnitOfWork Unit;
        public IsAdminHandler(TimeKeeperContext context)
        {
            Unit = new UnitOfWork(context);
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsRoleRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if(filterContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            if (!int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "sub").Value, out int empId))
            {
                context.Fail();
                return Task.CompletedTask;
            }
            string userRole = context.User.Claims.FirstOrDefault(c => c.Type == "role").Value;
            if(userRole == "admin")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}