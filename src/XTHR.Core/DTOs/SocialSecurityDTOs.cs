using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    public class SocialSecurityItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal DefaultContributionBase { get; set; }
        public decimal CompanyContributionRatio { get; set; }
        public decimal PersonalContributionRatio { get; set; }
    }

    public class SocialSecurityItemCreateDto
    {
        public string ItemName { get; set; }
        public decimal DefaultContributionBase { get; set; }
        public decimal CompanyContributionRatio { get; set; }
        public decimal PersonalContributionRatio { get; set; }
    }

    public class SocialSecurityItemUpdateDto
    {
        public string ItemName { get; set; }
        public decimal DefaultContributionBase { get; set; }
        public decimal CompanyContributionRatio { get; set; }
        public decimal PersonalContributionRatio { get; set; }
    }

    public class EmployeeSocialSecurityDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public List<SocialSecurityDetailDto> Items { get; set; } = new List<SocialSecurityDetailDto>();
    }

    public class SocialSecurityDetailDto
    {
        public string ItemName { get; set; }
        public decimal ContributionBase { get; set; }
        public decimal CompanyContributionAmount { get; set; }
        public decimal PersonalContributionAmount { get; set; }
    }
}