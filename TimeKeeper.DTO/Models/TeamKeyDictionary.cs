using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models
{
    public class TeamKeyDictionary
    {
        public TeamKeyDictionary(MasterModel team, decimal value)
        {
            Team = team;
            Value = value;
        }
        public MasterModel Team { get; set; }
        public decimal Value { get; set; }
    }
}
