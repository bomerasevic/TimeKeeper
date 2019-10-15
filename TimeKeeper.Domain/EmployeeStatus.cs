using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class EmployeeStatus
    {
        public EmployeeStatus()
        {
            Employees = new List<Employee>();
        }
        public int Id { get; set; }
        public const int TRIAL = 0;
        public const int ACTIVE = 1;
        public const int LEAVER = 2;
        public virtual IList<Employee> Employees { get; set; }
    }
}
