using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext: DbContext
    {
        public TimeKeeperContext(): base(){ }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseNpgsql("User ID=postgres; Password=postgres123; Server=localhost; Port=5432; Database=time_keeper; Integrated Security=true; Pooling=true;");
        }
    }
}
