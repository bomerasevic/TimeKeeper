using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class DayType : BaseStatus
    {
        public DayType()
        {
            Calendar = new List<Calendar>();
        }
        public virtual IList<Calendar> Calendar { get; set; }
    }
}
