using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Role : BaseClass
    {
        public string Name { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}
