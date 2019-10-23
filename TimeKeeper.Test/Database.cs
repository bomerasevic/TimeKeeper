using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.Test
{
    public static class Database
    {
        public static void Seed(this TimeKeeperContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Teams.Add(new Team { Name = "Red" });
            context.Teams.Add(new Team { Name = "Blue" });
            context.Teams.Add(new Team { Name = "Green" });
            context.SaveChanges();
        }
    }
}
