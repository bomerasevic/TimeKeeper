using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class EmployeeStatus : BaseStatus
    {
        public EmployeeStatus()
        {
            Employees = new List<Employee>();
        }
        public virtual IList<Employee> Employees { get; set; }
    }
}
