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
    }
}
