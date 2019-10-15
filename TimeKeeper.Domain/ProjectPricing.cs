using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain
{
    public class ProjectPricing
    {
        public int Id { get; set; }
        public const int FIXED_BID = 0;
        public const int HOURLY = 1;
        public const int PER_CAPITA = 2;
    }
}
