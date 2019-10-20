using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class DayModel
    {
        public DayModel()
        {
            Tasks = new List<MasterModel>();
        }
        public int Id { get; set; }
        public MasterModel Employee { get; set; }
        public MasterModel DayType { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalHours { get; set; }
        public IList<MasterModel> Tasks { get; set; }
    }
}
