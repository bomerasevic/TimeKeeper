using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Project : BaseClass
    {
        public Project()
        {
            Tasks = new List<Assignment>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual Team Team { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public ProjectPricing Pricing { get; set; }
        public decimal Amount { get; set; }
        public virtual IList<Assignment> Tasks { get; set; }

    }
}
