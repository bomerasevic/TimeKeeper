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

        public override IList<Day> Get(Func<Day, bool> where) => Get().Where(where).ToList();
        public override Day Get(int id) => Get().FirstOrDefault(x => x.Id == id);
    }
}
