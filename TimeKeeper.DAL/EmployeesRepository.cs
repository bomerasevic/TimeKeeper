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

        public override void Delete(int id)
        {
            Employee old = Get(id);

            if (old.Days.Count != 0 || old.Memberships.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}
