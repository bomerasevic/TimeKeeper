using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class CalendarType
    {
        public int Id { get; set; }
        public const int VACATION = 0;
        public const int SICK = 1;
        public const int PerCapita = 2;
    }
}
