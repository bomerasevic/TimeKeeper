using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class ProjectsRepository : Repository<Project>
    {
        public ProjectsRepository(TimeKeeperContext context) : base(context) { }

    }
}
