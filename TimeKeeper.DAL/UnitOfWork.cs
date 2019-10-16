using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly TimeKeeperContext _context;
        private IRepository<Assignment> _assignments;
        private IRepository<Calendar> _days;
        private IRepository<Customer> _customers;
        private IRepository<Employee> _employees;
        private IRepository<Member> _members;
        private IRepository<Project> _projects;
        private IRepository<Role> _roles;
        private IRepository<Team> _teams;

        public TimeKeeperContext Context => _context;

        public IRepository<Assignment> Assignments => _assignments ?? (_assignments = new AssignmentsRepository(_context));
        public IRepository<Calendar> Days => _days ?? (_days = new CalendarRepository(_context));
        public IRepository<Customer> Customers => _customers ?? (_customers = new CustomersRepository(_context));
        public IRepository<Employee> Employees => _employees ?? (_employees = new EmployeesRepository(_context));
        public IRepository<Member> Members => _members ?? (_members = new MembersRepository(_context));
        public IRepository<Project> Projects => _projects ?? (_projects = new ProjectsRepository(_context));
        public IRepository<Role> Roles => _roles ?? (_roles = new RolesRepository(_context));
        public IRepository<Team> Teams => _teams ?? (_teams = new TeamsRepository(_context));

        public int Save() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
