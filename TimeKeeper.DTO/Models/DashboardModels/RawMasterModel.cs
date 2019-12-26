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
        public decimal Value { get; set; }
    }    
    public class TeamRawNonWorkingHoursModel
    {
        public int MemberId { get; set; }
        public decimal Value { get; set; }
    }
    public class CompanyDashboardRawModel
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string RoleName { get; set; }
        public decimal RoleHourlyPrice { get; set; }
        public decimal RoleMonthlyPrice { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectAmount { get; set; }
        public int ProjectPricingId { get; set; }
        public string ProjectPricingName { get; set; }
        public decimal WorkingHours { get; set; }
    }
    public class CompanyRolesRawModel
    {
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public decimal WorkingHours { get; set; }
    }
    public class PersonalDashboardRawModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal WorkingMonthly { get; set; }
        public decimal WorkingYearly { get; set; }
        public int SickMonthly { get; set; }
        public int SickYearly { get; set; }
    }
    public class RawAbsenceModel
    {
        public decimal AbsenceInstances { get; set; }
    }
}
