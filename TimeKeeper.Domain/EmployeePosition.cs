using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
