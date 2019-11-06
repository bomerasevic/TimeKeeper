using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class EmployeesRepository : Repository<Employee>
    {
        public EmployeesRepository(TimeKeeperContext context) : base(context) { }

        private void Build(Employee employee)
        {
            employee.Status = _context.EmployeeStatuses.Find(employee.Status.Id);
            employee.Position = _context.EmployeePositions.Find(employee.Position.Id);
        }
        public override void Insert(Employee employee)
        {
            Build(employee);
            base.Insert(employee);
        }
        public override void Update(Employee employee, int id)
        {
            Employee old = Get(id);

            Build(employee);
            _context.Entry(old).CurrentValues.SetValues(employee);
            old.Status = employee.Status;
            old.Position = employee.Position;
        }
        public override void Delete(int id)
        {
            Employee old = Get(id);

            if (old.Days.Count != 0 || old.Memberships.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}
