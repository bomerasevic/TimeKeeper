using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class Assignment : BaseClass
    {
        public virtual Calendar Day { get; set; }
        public virtual Project Project { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Hours { get; set; }
    }
}
