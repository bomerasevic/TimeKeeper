using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public MasterModel Employee { get; set; }
<<<<<<< HEAD
        public MasterModel Team { get; set; }
        public MasterModel Role { get; set; }
=======
        public MasterModel Role { get; set; }
        public MasterModel Team { get; set; }
>>>>>>> dev
        public decimal HoursWeekly { get; set; }
    }
}
