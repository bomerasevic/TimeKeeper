using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class CalendarRepository : Repository<Day>
    {
        public CalendarRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(Day day, int id)
        {
            Day old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(day);
                old.Employee = day.Employee;
                old.DayType = day.DayType;
            }
        }
    }
}
