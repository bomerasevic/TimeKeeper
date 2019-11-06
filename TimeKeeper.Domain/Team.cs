using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Team : BaseClass
    {
        public Team()
        {
            Projects = new List<Project>();
            TeamMembers = new List<Member>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        // dodati status
        public virtual IList<Project> Projects { get; set; }
        public virtual IList<Member> TeamMembers { get; set; }
    }
}
