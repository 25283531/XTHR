using System;

namespace XTHR.Core.DTOs
{
    public class EmployeeSummaryDto
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }

    public class EmployeeDetailsDto
    {
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public SalaryDto SalaryInfo { get; set; }
        public SocialSecurityDto SocialSecurityInfo { get; set; }
    }

    public class SalaryDto
    {
        public decimal BaseSalary { get; set; }
        public decimal PerformanceBonus { get; set; }
    }

    public class SocialSecurityDto
    {
        public bool IsEnrolled { get; set; }
        public decimal ContributionBase { get; set; }
    }

    public class SalaryUpdateDto
    {
        public decimal BaseSalary { get; set; }
        public decimal PerformanceBonus { get; set; }
    }

    public class SocialSecurityUpdateDto
    {
        public bool IsEnrolled { get; set; }
        public decimal ContributionBase { get; set; }
    }
}