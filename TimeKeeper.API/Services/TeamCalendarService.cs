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
    public class TeamCalendarService
    {
        protected UnitOfWork Unit;
        public TeamCalendarService(UnitOfWork unit)
        {
            Unit = unit;
        }
        public List<TeamTimeTrackingModel> TeamMonthReport(int teamId, int month, int year)
        {
            List<TeamTimeTrackingModel> teamTimeTracking = new List<TeamTimeTrackingModel>();

            List<Day> days = Unit.Calendar.Get(x => x.Date.Year == year && x.Date.Month == month).ToList();
            List<DayType> dayTypes = Unit.DayTypes.Get().ToList();
            Team team = Unit.Teams.Get(x => x.Id == teamId).FirstOrDefault();

            foreach(Member member in team.TeamMembers)
            {
                teamTimeTracking.Add(new TeamTimeTrackingModel { Employee = member.Employee.Master() });

                List<Day> employeeDays = days.FindAll(x => x.Employee.Id == member.Employee.Id);

                int missingEntries = employeeDays.Count * 8;

                foreach(DayType dt in dayTypes)
                {
                    List<Day> dayTypeDays = employeeDays.FindAll(x => x.DayType.Id == dt.Id);

                    int sum = (int)dayTypeDays.Sum(x => x.TotalHours);
                    missingEntries -= sum;
                    teamTimeTracking[teamTimeTracking.Count() - 1].hourTypes.Add(dt.Name, sum);
                }
                teamTimeTracking[teamTimeTracking.Count() - 1].hourTypes.Add("Missing entries", missingEntries);
            }
            return teamTimeTracking;
        }


    }
}
