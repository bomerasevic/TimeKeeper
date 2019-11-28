using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Models
{
    public class TeamKeyDictionary
    {
        public TeamKeyDictionary(MasterModel team, int value)
        {
            Team = team;
            Value = value;
        }
        public MasterModel Team { get; set; }
        public int Value { get; set; }
    }
}
