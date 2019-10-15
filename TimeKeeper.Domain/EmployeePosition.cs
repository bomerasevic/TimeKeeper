using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class EmployeePosition
    {
        public EmployeePosition()
        {
            Employees = new List<Employee>();
        }
        public int Id { get; set; }
        public const int FULL_STACK = 0;
        public const int BACKEND_DEV = 1;
        public const int FRONTEND_DEV = 2;
        public const int QA_ENGINEER = 3;
        public const int PROJECT_MANAGER = 4;
        public virtual IList<Employee> Employees { get; set; }
    }
}
