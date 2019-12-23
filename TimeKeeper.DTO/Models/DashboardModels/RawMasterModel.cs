using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.DTO.Models.DashboardModels
{
    public class RawMasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
    public class AdminRawCountModel
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public decimal WorkingHours { get; set; }
    }
    public class AdminRawPTOModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public decimal PaidTimeOff { get; set; }
    }
    public class TeamRawCountModel
    {
        public int ProjectId { get; set; }
    }
    public class TeamRawModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal WorkingHours { get; set; }
    }    
    public class TeamRawNonWorkingHoursModel
    {
        public int MemberId { get; set; }
        public decimal Value { get; set; }
    }
}
