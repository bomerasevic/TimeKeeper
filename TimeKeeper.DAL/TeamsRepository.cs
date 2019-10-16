using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class TeamsRepository : Repository<Team>
    {
        public TeamsRepository(TimeKeeperContext context) : base(context) { }

    }
}
