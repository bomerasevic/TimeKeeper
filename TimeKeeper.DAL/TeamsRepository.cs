using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class TeamsRepository : Repository<Team>
    {
        public TeamsRepository(TimeKeeperContext context) : base(context) { }    
        
        public override void Delete(int id)
        {
            Team old = Get(id);

            if (old.Projects.Count != 0 || old.TeamMembers.Count != 0)
                throw new Exception("Object cannot be deleted because it has children entities!");

            Delete(old);
        }
    }   
}
