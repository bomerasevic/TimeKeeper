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
        }
        
        //private static void BuildPassword(User user)
        //{
        //    if (!string.IsNullOrWhiteSpace(user.Password)) user.Password = user.Username.HashWith(user.Password);
        //}

        public static void Relate<T>(this T oldEntity, T newEntity)
        {
            if (typeof(T) == typeof(Project)) Modify(oldEntity as Project, newEntity as Project);
            if (typeof(T) == typeof(Day)) Modify(oldEntity as Day, newEntity as Day);
        }
        private static void Create(Project p, TimeKeeperContext context)
        {
            p.Customer = context.Customers.Find(p.Customer.Id);
            p.Team = context.Teams.Find(p.Team.Id);
        }
        private static void Modify(Project oldP, Project newP)
        {
            oldP.Customer = newP.Customer;
            oldP.Team = newP.Team;
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
    }
}
