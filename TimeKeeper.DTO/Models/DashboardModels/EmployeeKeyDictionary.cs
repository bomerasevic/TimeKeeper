using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Models.DomainModels;

namespace TimeKeeper.DTO.Models
{
    public class EmployeeKeyDictionary
    {
        public EmployeeKeyDictionary(MasterModel employee, decimal value)
        {
            Employee = employee;
            Value = value;
        }
        public MasterModel Employee { get; set; }
        public decimal Value { get; set; }
    }
}
