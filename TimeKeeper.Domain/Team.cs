using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Team : BaseClass
    {
        public Team()
        {
            Projects = new List<Project>();
        }
        public string Name { get; set; }
        public virtual IList<Project> Projects { get; set; }
    }
}
