using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class ProjectsRepository : Repository<Project>
    {
        public ProjectsRepository(TimeKeeperContext context) : base(context) { }

        public override void Delete(int id)
        {
            Project old = Get(id);

            if (old.Tasks.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }
}
