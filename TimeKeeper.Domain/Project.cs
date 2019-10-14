using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Project
    {
        public Project()
        {
            Tasks = new List<Assignment>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public ProjectPricing Pricing { get; set; }
        public decimal Amount { get; set; }
        public virtual IList<Assignment> Tasks { get; set; }

    }
}
