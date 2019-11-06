using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Models;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Factory
{
    public static class ModelFactory
    {
        public static TeamModel Create(this Team team)
        {
            return new TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                Members = team.TeamMembers.Select(x => x.Master("team")).ToList(),
                Projects = team.Projects.Select(x => x.Master()).ToList()
            };
        }
        public static ProjectModel Create(this Project project)
        {
            return new ProjectModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Team = new MasterModel { Id = project.Team.Id, Name = project.Team.Name },
                Customer = new MasterModel { Id = project.Customer.Id, Name = project.Customer.Name },
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = new MasterModel { Id = project.Status.Id, Name = project.Status.Name },
                Pricing = new MasterModel { Id = project.Pricing.Id, Name = project.Pricing.Name },
                Amount = project.Amount
            };           
        }
        public static DayModel Create(this Day day)
        {
            return new DayModel
            {
                Id = day.Id,
                Employee = new MasterModel { Id = day.Employee.Id, Name = day.Employee.FullName },
                DayType = new MasterModel { Id = day.DayType.Id, Name = day.DayType.Name },
                Date = day.Date,
                TotalHours = day.TotalHours,
                Tasks = day.Tasks.Select(x => x.Master()).ToList()
            };
        }
        public static RoleModel Create(this Role role)
        {
            return new RoleModel
            {
                Id = role.Id,
                Name = role.Name,
                HourlyPrice = role.HourlyPrice,
                MonthlyPrice = role.MonthlyPrice,
                MemberRoles = role.MemberRoles.Select(x => x.Master("role")).ToList()
            };
        }
        public static EmployeeModel Create(this Employee employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FullName = employee.FullName,
                Image = employee.Image,
                Email = employee.Email,
                Phone = employee.Phone,
                Birthday = employee.Birthday,
                BeginDate = employee.BeginDate,
                EndDate = employee.EndDate,
                Status = new MasterModel { Id = employee.Status.Id, Name = employee.Status.Name },
                Position = new MasterModel { Id = employee.Position.Id, Name = employee.Position.Name },
                Days = employee.Days.Select(x => x.Master()).ToList(),
                Memberships = employee.Memberships.Select(x => x.Master("role")).ToList(),
            };
        }

        public static AssignmentModel Create(this Assignment assignment)
        {
            return new AssignmentModel
            {
                Id = assignment.Id,
                Description = assignment.Description,
                Hours = assignment.Hours,
                Project = new MasterModel { Id = assignment.Project.Id, Name = assignment.Project.Name }
            };
        }

        public static CustomerModel Create(this Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Image = customer.Image,
                Contact = customer.Contact,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Status = new MasterModel { Id=customer.Status.Id, Name=customer.Status.Name},
                Projects = customer.Projects.Select(x=>x.Master()).ToList()
            };
        }

        public static MemberModel Create(this Member member)
        {
            return new MemberModel
            {
                Id = member.Id,
                Employee = new MasterModel { Id=member.Employee.Id, Name=$"{member.Employee.FullName}"},
                Role = new MasterModel { Id=member.Role.Id, Name=member.Role.Name},
                Team = new MasterModel { Id=member.Team.Id, Name=member.Team.Name},
                Status = new MasterModel { Id = member.Status.Id, Name = member.Status.Name },
                HoursWeekly = member.HoursWeekly
            };
        }

        public static MasterModel Create(this BaseStatus baseStatus)
        {
            return new MasterModel
            {
                Id = baseStatus.Id,
                Name = baseStatus.Name
            };
        }
    }
}
