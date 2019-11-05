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

        public override void Update(Assignment assignment, int id)
        {
            Assignment old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(assignment);
                old.Day = assignment.Day;
                old.Project = assignment.Project;
            }
        }
    }
}
