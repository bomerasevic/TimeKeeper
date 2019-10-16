using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class EmployeesRepository : Repository<Employee>
    {
        public EmployeesRepository(TimeKeeperContext context) : base(context) { }

    }
}
