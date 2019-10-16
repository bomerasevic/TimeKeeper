using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext() : base() { }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Day> Calendar { get; set; }
        public DbSet<DayType> DayTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPricing> ProjectPricings { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {            
            optionBuilder.UseNpgsql("User ID=postgres; Password=admin; Server=localhost; Port=5432; Database=time_keeper; Integrated Security=true; Pooling=true;");
            optionBuilder.UseLazyLoadingProxies(true); // default = true
            base.OnConfiguring(optionBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>().OwnsOne(x => x.Address);

            builder.Entity<Assignment>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Day>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<DayType>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Customer>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<CustomerStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Employee>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<EmployeePosition>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<EmployeeStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Member>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Project>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<ProjectPricing>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<ProjectStatus>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Role>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Team>().HasQueryFilter(x => !x.Deleted);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted && x.Entity is BaseClass))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["Deleted"] = true;
            }
            return base.SaveChanges();
        }
    }
}
