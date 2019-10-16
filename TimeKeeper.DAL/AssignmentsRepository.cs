using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class AssignmentsRepository : Repository<Assignment>
    {
        public AssignmentsRepository(TimeKeeperContext context) : base(context) { }

        public override IList<Assignment> Get(Func<Assignment, bool> where) => Get().Where(where).ToList();
        public override Assignment Get(int id) => Get().FirstOrDefault(x => x.Id == id);
    }
}
