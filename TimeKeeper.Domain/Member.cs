using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Member : BaseClass
    {
        public int TeamId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public decimal HoursWeekly { get; set; }
        public virtual Team Team { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
