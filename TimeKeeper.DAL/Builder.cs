using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public static class Builder
    {
        public static void Build<T>(this T entity, TimeKeeperContext context)
        {
            if (typeof(T) == typeof(Project)) Create(entity as Project, context);
            if (typeof(T) == typeof(Day)) Create(entity as Day, context);
            if (typeof(T) == typeof(Assignment)) Create(entity as Assignment, context);
            if (typeof(T) == typeof(Customer)) Create(entity as Customer, context);
            if (typeof(T) == typeof(Employee)) Create(entity as Employee, context);
            if (typeof(T) == typeof(Member)) Create(entity as Member, context);
            if (typeof(T) == typeof(User)) BuildPassword(entity as User);
        }

        private static void BuildPassword(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Password)) user.Password = user.Username.HashWith(user.Password);
        }

        public static void Relate<T>(this T oldEntity, T newEntity)
        {
            if (typeof(T) == typeof(Project)) Modify(oldEntity as Project, newEntity as Project);
            if (typeof(T) == typeof(Day)) Modify(oldEntity as Day, newEntity as Day);
            if (typeof(T) == typeof(Assignment)) Modify(oldEntity as Assignment, newEntity as Assignment);
            if (typeof(T) == typeof(Customer)) Modify(oldEntity as Customer, newEntity as Customer);
            if (typeof(T) == typeof(Employee)) Modify(oldEntity as Employee, newEntity as Employee);
            if (typeof(T) == typeof(Member)) Modify(oldEntity as Member, newEntity as Member);
        }
        private static void Create(Project p, TimeKeeperContext context)
        {
            p.Customer = context.Customers.Find(p.Customer.Id);
            p.Team = context.Teams.Find(p.Team.Id);
            p.Status = context.ProjectStatuses.Find(p.Status.Id);
            p.Pricing = context.ProjectPricings.Find(p.Pricing.Id);
        }
        private static void Modify(Project oldP, Project newP)
        {
            oldP.Customer = newP.Customer;
            oldP.Team = newP.Team;
            oldP.Status = newP.Status;
            oldP.Pricing = newP.Pricing;
        }
        private static void Create(Day d, TimeKeeperContext context)
        {
            d.Employee = context.Employees.Find(d.Employee.Id);
            d.DayType = context.DayTypes.Find(d.DayType.Id);
        }
        private static void Modify(Day oldD, Day newD)
        {
            oldD.Employee = newD.Employee;
            oldD.DayType = newD.DayType;
        }
        private static void Create(Assignment a, TimeKeeperContext context)
        {
            a.Day = context.Calendar.Find(a.Day.Id);
            a.Project = context.Projects.Find(a.Project.Id);
        }
        private static void Modify(Assignment oldA, Assignment newA)
        {
            oldA.Day = newA.Day;
            oldA.Project = newA.Project;
        }
        private static void Create(Customer c, TimeKeeperContext context)
        {
            c.Status = context.CustomerStatuses.Find(c.Status.Id);
        }
        private static void Modify(Customer oldC, Customer newC)
        {
            oldC.Address = newC.Address;
            oldC.Status = newC.Status;
        }
        private static void Create(Employee e, TimeKeeperContext context)
        {
            e.Status = context.EmployeeStatuses.Find(e.Status.Id);
            e.Position = context.EmployeePositions.Find(e.Position.Id);
        }
        private static void Modify(Employee oldE, Employee newE)
        {
            oldE.Status = newE.Status;
            oldE.Position = newE.Position;
        }
        private static void Create(Member m, TimeKeeperContext context)
        {
            m.Team = context.Teams.Find(m.Team.Id);
            m.Employee = context.Employees.Find(m.Employee.Id);
            m.Role = context.Roles.Find(m.Role.Id);
            m.Status = context.MemberStatuses.Find(m.Status.Id);
        }
        private static void Modify(Member oldM, Member newM)
        {
            oldM.Team = newM.Team;
            oldM.Employee = newM.Employee;
            oldM.Role = newM.Role;
            oldM.Status = newM.Status;
        }
    }
}
