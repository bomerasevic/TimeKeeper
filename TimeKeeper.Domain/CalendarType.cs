using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class CalendarType : BaseStatus
    {
        public CalendarType()
        {
            Calendar = new List<Calendar>();
        }
        public virtual IList<Calendar> Calendar { get; set; }
    }
}
