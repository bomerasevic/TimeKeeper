using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class AssignmentModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public MasterModel Project { get; set; }
    }
}
