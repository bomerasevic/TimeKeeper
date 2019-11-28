using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class ProjectKeyDictionary
    {
        public ProjectKeyDictionary(MasterModel project, int value)
        {
            Project = project;
            Value = value;
        }

        public MasterModel Project { get; set; }
        public int Value { get; set; }
    }
}
