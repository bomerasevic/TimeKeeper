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

        public override void Update(Employee employee, int id)
        {
            Employee old = Get(id);

            if(old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(employee);

            }
        }
    }
}
