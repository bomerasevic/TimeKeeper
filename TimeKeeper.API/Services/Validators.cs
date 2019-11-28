using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Services
{
    public static class Validators
    {
        public static bool IsAbsence(this Day day)
        {
            return day.DayType.Name != "Workday";
        }
    }
}
