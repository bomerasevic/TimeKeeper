using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Services
{
    public class CalendarService
    {
        protected UnitOfWork Unit;
        public CalendarService(UnitOfWork unit)
        {
            Unit = unit;
        }
        
        public List<EmployeeTimeModel> TeamMonthReport(int teamId, int year, int month)
        {
            Team team = Unit.Teams.Get(teamId);
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();

            foreach (Member member in team.TeamMembers)
            {
                employeeTimeModels.Add(CreateEmployeeReport(member.Employee.Id, year, month));
            }

            return employeeTimeModels;
        }

        public EmployeeTimeModel CreateEmployeeReport(int employeeId, int year, int month)
        {
            Employee employee = Unit.Employees.Get(employeeId);
            EmployeeTimeModel employeePersonalReport = employee.CreateTimeModel();
            List<DayModel> calendar = GetEmployeeMonth(employeeId, year, month);

            //SetDayTypes(employeePersonalReport.HourTypes);
            //Dictionary<string, decimal> hours = employeePersonalReport.HourTypes;

            //hours.Add("Total hours", 0);
            List<DayType> dayTypes = Unit.DayTypes.Get().ToList();
            int totalHours = 0;

            foreach (DayType day in dayTypes)
            {
                ////Adds the day's total hours to the appropriate place in the dictionary (to the appropriate day type)
                ////hours[day.DayType.Name] += day.JobDetails.Where(x => x.Project.Id == project.Id).Sum(jd => jd.Hours); 
                ////hours[day.DayType.Name] += day.TotalHours;//CalculateHoursOnProject(day, project);
                //hours["Total hours"] += day.TotalHours;
                //hours["Missing entries"] -= day.TotalHours;
                int sumHoursPerDay = (int)calendar.FindAll(x => x.DayType.Id == day.Id).Sum(x => x.TotalHours);
                employeePersonalReport.HourTypes.Add(day.Name, sumHoursPerDay);
                totalHours += sumHoursPerDay;
            }
            int MissingEntries = calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8;
            employeePersonalReport.HourTypes.Add("MissingEntries", MissingEntries);
            employeePersonalReport.HourTypes.Add("TotalHours", totalHours + MissingEntries);
            //hours.Add("Missing entries", calendar.FindAll(x => x.DayType.Name == "Empty").Sum(x => x.TotalHours));
            return employeePersonalReport;
        }

        private void SetDayTypes(Dictionary<string, decimal> hourTypes)
        {
            List<DayType> dayTypes = Unit.DayTypes.Get().ToList();
            DayType future = new DayType { Id = 10, Name = "Future" };
            DayType weekend = new DayType { Id = 11, Name = "Weekend" };
            DayType empty = new DayType { Id = 12, Name = "Empty" };
            //REFACTOR
            DayType na = new DayType { Id = 13, Name = "N/A" };
            foreach (DayType day in dayTypes)
            {
                hourTypes.Add(day.Name, 0);
            }
            hourTypes.Add(future.Name, 0);
            hourTypes.Add(empty.Name, 0);
            hourTypes.Add(weekend.Name, 0);
            hourTypes.Add(na.Name, 0);
        }

        public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!ValidateGetEmployeeMonth(year, month)) throw new Exception("Invalid data! Check year and month again.");
            DayType future = new DayType { Id = 10, Name = "Future" }; // svaki dan naredni od danasnjeg
            DayType weekend = new DayType { Id = 11, Name = "Weekend" };
            DayType empty = new DayType { Id = 12, Name = "Empty" };
            //to rethink name
            DayType na = new DayType { Id = 13, Name = "N/A" };
            DateTime day = new DateTime(year, month, 1);
            Employee emp = Unit.Employees.Get(empId);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = emp.Master(),
                    Date = day,
                    DayType = empty.Master()
                };
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();
                if (day < emp.BeginDate || (emp.EndDate != null && emp.EndDate != new DateTime(1, 1, 1) && day > emp.EndDate)) newDay.DayType = na.Master();
                calendar.Add(newDay);
                day = day.AddDays(1);
            }
            List<DayModel> employeeDays = Unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).ToList().Select(x => x.Create()).ToList();
            foreach (var d in employeeDays)
            {
                calendar[d.Date.Day - 1] = d;
            }
            return calendar;
        }

        //public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        //{
        //    List<DayModel> calendar = new List<DayModel>();

        //    if (!ValidateGetEmployeeMonth(year, month)) throw new Exception("Invalid data! Check year and month again!");


        //    DayType future = new DayType { Id = 10, Name = "Future" };
        //    DayType weekend = new DayType { Id = 11, Name = "Weekend" };
        //    DayType empty = new DayType { Id = 12, Name = "Empty" };
        //    //REFACTOR
        //    DayType na = new DayType { Id = 13, Name = "N/A" };

        //    DateTime day = new DateTime(year, month, 1);

        //    Employee emp = Unit.Employees.Get(empId);

        //    while (day.Month == month)
        //    {
        //        DayModel newDay = new DayModel
        //        {
        //            Employee = emp.Master(),
        //            Date = day,
        //            DayType = empty.Master()
        //        };

        //        if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) newDay.DayType = weekend.Master();

        //        if (day > DateTime.Today) newDay.DayType = future.Master(); else newDay.DayType = empty.Master();

        //        if (day < emp.BeginDate || (emp.EndDate != null && emp.EndDate != new DateTime(1 / 1 / 1) && day > emp.EndDate)) newDay.DayType = na.Master();

        //        //TODO: Uslov kad je uposlenik poceo raditi
        //        calendar.Add(newDay);
        //        day = day.AddDays(1);
        //    }

        //    List<DayModel> employeeDays = Unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).ToList().Select(x => x.Create()).ToList();

        //    foreach (var d in employeeDays)
        //    {
        //        calendar[d.Date.Day - 1] = d;
        //    }
        //    return calendar;
        //}

        //private Dictionary<EmployeeTimeModel, decimal> GetPaidTimeOff(List<EmployeeTimeModel> employeeTimes)
        //{
        //    Dictionary<EmployeeModel, decimal> paidTimeOff = new Dictionary<EmployeeModel, decimal>();
        //    foreach (EmployeeTimeModel employeeTime in employeeTimes)
        //    {
        //        decimal PTO = employeeTime.HourTypes["Total Hours"]
        //                    - employeeTime.HourTypes["Missing entries"]
        //                    - employeeTime.HourTypes["Workday"];
        //        paidTimeOff.Add(employeeTime.Employee, PTO);
        //    }

        //    return paidTimeOff;
        //}

        //private Dictionary<EmployeeTimeModel, decimal> GetOvertime(List<EmployeeTimeModel> employeeTimes)
        //{
        //    Dictionary<EmployeeModel, decimal> overtime = new Dictionary<EmployeeModel, decimal>();
        //    foreach (EmployeeTimeModel employeeTime in employeeTimes)
        //    {
        //        decimal PTO = employeeTime.HourTypes["Total Hours"]
        //                    - employeeTime.HourTypes["Missing entries"]
        //                    - employeeTime.HourTypes["Workday"];
        //        overtime.Add(employeeTime.Employee, PTO);
        //    }

        //    return overtime;
        //}

        private decimal GetMonthlyWorkingHours(int empId, int year, int month)
        {
            //List<DayModel> calendar = new List<DayModel>();
            int daysInMonth = DateTime.DaysInMonth(year, month);

            int workingDays = 0;
            for (int i = 1; i < daysInMonth; i++)
            {
                DateTime thisDay = new DateTime(year, month, i);
                if (thisDay.DayOfWeek != DayOfWeek.Saturday && thisDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays += 1;
                }
            }

            return workingDays;
        }
        public bool ValidateGetEmployeeMonth(int year, int month)
        {
            if (month > 12 || month < 1)
                return false;

            if (year < 2010)//Founding year
                return false;

            DateTime date = new DateTime(year, month, 1);

            //User can not see more than 6 months in advance
            if (date > DateTime.Today.AddMonths(6))
                return false;

            return true;
        }
    }
}
