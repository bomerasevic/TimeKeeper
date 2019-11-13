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
        }
        public static void Relate<T>(this T oldEntity, T newEntity)
        {
            if (typeof(T) == typeof(Project)) Modify(oldEntity as Project, newEntity as Project);
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
    }
}
