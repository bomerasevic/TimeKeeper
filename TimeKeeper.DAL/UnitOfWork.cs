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
        private IRepository<Calendar> _calendar;
        private IRepository<DayType> _dayTypes;
        private IRepository<Customer> _customers;
        private IRepository<CustomerStatus> _customerStatuses;
        private IRepository<Employee> _employees;
        private IRepository<EmployeePosition> _employeePositions;
        private IRepository<EmployeeStatus> _employeeStatuses;
        private IRepository<Member> _members;
        private IRepository<Project> _projects;
        private IRepository<ProjectPricing> _projectPrices;
        private IRepository<ProjectStatus> _projectStatuses;
        private IRepository<Role> _roles;
        private IRepository<Team> _teams;

        public TimeKeeperContext Context => _context;

        public UnitOfWork(TimeKeeperContext context=null)
        {
            if (context != null)
            {
                _context = context;
            }
            else
            {
                _context = new TimeKeeperContext();
            }
        }

        public IRepository<Assignment> Assignments => _assignments ?? (_assignments = new AssignmentsRepository(_context));
        public IRepository<Calendar> Calendar => _calendar ?? (_calendar = new CalendarRepository(_context));
        public IRepository<DayType> DayTypes => _dayTypes ?? (_dayTypes = new Repository<DayType>(_context));
        public IRepository<Customer> Customers => _customers ?? (_customers = new CustomersRepository(_context));
        public IRepository<CustomerStatus> CustomerStatuses => _customerStatuses ?? (_customerStatuses = new Repository<CustomerStatus>(_context));
        public IRepository<Employee> Employees => _employees ?? (_employees = new EmployeesRepository(_context));
        public IRepository<EmployeePosition> EmployeePositions => _employeePositions ?? (_employeePositions = new Repository<EmployeePosition>(_context));
        public IRepository<EmployeeStatus> EmployeeStatuses => _employeeStatuses ?? (_employeeStatuses = new Repository<EmployeeStatus>(_context));
        public IRepository<Member> Members => _members ?? (_members = new MembersRepository(_context));
        public IRepository<Project> Projects => _projects ?? (_projects = new ProjectsRepository(_context));
        public IRepository<ProjectPricing> ProjectPrices => _projectPrices ?? (_projectPrices = new Repository<ProjectPricing>(_context));
        public IRepository<ProjectStatus> ProjectStatuses => _projectStatuses ?? (_projectStatuses = new Repository<ProjectStatus>(_context));
        public IRepository<Role> Roles => _roles ?? (_roles = new RolesRepository(_context));
        public IRepository<Team> Teams => _teams ?? (_teams = new TeamsRepository(_context));

        public int Save() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
