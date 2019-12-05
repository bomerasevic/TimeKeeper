using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.Domain;
using TimeKeeper.DTO.Models;

namespace TimeKeeper.BLL.Services
{
    public static class Validators
    {
        public static bool IsAbsence(this Day day)
        {
            return day.DayType.Name != "Workday";
        }
        public static bool ValidateGetEmployeeMonth(int year, int month)
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
        public static bool IsWeekend(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) return true;
            return false;
        }
        public static bool IsDuplicateEmployeeInMonthlyOverview(List<EmployeeModel> employees, Employee employee)
        {
            foreach (EmployeeModel emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsDuplicateProject(List<Project> projects, Project project)
        {
            foreach (Project p in projects)
            {
                if (p.Id == project.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsDuplicateEmployee(List<Employee> employees, Employee employee)
        {
            if (employees.Count == 0) return false;
            foreach (Employee emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
