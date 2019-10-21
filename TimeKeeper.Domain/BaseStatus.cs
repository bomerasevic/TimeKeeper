using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeKeeper.Domain
{
    public class BaseStatus : BaseClass
    {
        public string Name { get; set; }
    }
}
