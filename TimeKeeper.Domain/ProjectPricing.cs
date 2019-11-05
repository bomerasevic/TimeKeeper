using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class ProjectPricing : BaseStatus
    {
        public ProjectPricing()
        {
            Projects = new List<Project>();
        }
        public virtual IList<Project> Projects { get; set; }
    }
}
