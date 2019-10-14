using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Assignment : BaseClass
    {
        public int DayId { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }

    }
}
