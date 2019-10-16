using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class CalendarRepository : Repository<Calendar>
    {
        public CalendarRepository(TimeKeeperContext context) : base(context) { }

        public override IList<Calendar> Get(Func<Calendar, bool> where) => Get().Where(where).ToList();
        public override Calendar Get(int id) => Get().FirstOrDefault(x => x.Id == id);
    }
}
