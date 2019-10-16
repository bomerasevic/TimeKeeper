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
            ProjectMembers = new List<Member>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual IList<Project> Projects { get; set; }
        public virtual IList<Member> ProjectMembers { get; set; }
    }
}
