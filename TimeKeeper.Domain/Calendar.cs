using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Calendar
    {
        public Calendar()
        {
            Tasks = new List<Assignment>();
        }
        public int EmployeeId { get; set; }
        public CalendarType CalendarType { get; set; }
        public DateTime Date { get; set; }
        public decimal Hours { get; set; }
        public virtual IList<Assignment> Tasks { get; set; }
    }
}
