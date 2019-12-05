using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.DTO.Models
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
