using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TimeKeeper.Domain
{
    [Table("Calendar")]
    public class Day : BaseClass
    {
        public Day()
        {
            Tasks = new List<Assignment>();
        }
        public virtual Employee Employee { get; set; }
        public virtual DayType DayType { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalHours { get; set; }
        public virtual IList<Assignment> Tasks { get; set; }
    }
}
