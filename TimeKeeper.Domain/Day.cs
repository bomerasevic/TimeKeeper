using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        [NotMapped]
        public decimal TotalHours {
            get {
                if (DayType.Name != "workday")
                    return 8;

                return (Tasks.Sum(x => x.Hours));
            }}
        public virtual IList<Assignment> Tasks { get; set; }
    }
}
