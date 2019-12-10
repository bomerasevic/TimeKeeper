using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;

namespace TimeKeeper.BLL.Services
{
    public class ProjectHistoryReport
    {
        protected UnitOfWork _unit;

        public ProjectHistoryReport(UnitOfWork unit)
        {
            _unit = unit;
        }

    }
}
