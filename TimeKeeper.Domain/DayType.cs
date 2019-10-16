using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class DayType : BaseStatus
    {
        public DayType()
        {
            Days = new List<Calendar>();
        }
        public virtual IList<Calendar> Days { get; set; }
    }
}
