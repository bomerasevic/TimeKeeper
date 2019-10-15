using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class BaseStatus : BaseClass
    {
        [Required]
        public string Name { get; set; }
    }
}
