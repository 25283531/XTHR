using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 部门工资报告DTO
    /// </summary>
    public class DepartmentPayrollReportDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 上级部门名称
        /// </summary>
        public string? ParentDepartmentName { get; set; }

        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 应发工资总额
        /// </summary>
        public decimal TotalGrossPay { get; set; }

        /// <summary>
        /// 实发工资总额
        /// </summary>
        public decimal TotalNetPay { get; set; }

        /// <summary>
        /// 个人所得税总额
        /// </summary>
        public decimal TotalIncomeTax { get; set; }

        /// <summary>
        /// 社保个人部分总额
        /// </summary>
        public decimal TotalSocialSecurityPersonal { get; set; }

        /// <summary>
        /// 公积金个人部分总额
        /// </summary>
        public decimal TotalHousingFundPersonal { get; set; }

        /// <summary>
        /// 社保公司部分总额
        /// </summary>
        public decimal TotalSocialSecurityCompany { get; set; }

        /// <summary>
        /// 公积金公司部分总额
        /// </summary>
        public decimal TotalHousingFundCompany { get; set; }

        /// <summary>
        /// 人工成本总额
        /// </summary>
        public decimal TotalLaborCost { get; set; }

        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }

        /// <summary>
        /// 最高工资
        /// </summary>
        public decimal MaxSalary { get; set; }

        /// <summary>
        /// 最低工资
        /// </summary>
        public decimal MinSalary { get; set; }

        /// <summary>
        /// 工资中位数
        /// </summary>
        public decimal MedianSalary { get; set; }

        /// <summary>
        /// 工资标准差
        /// </summary>
        public decimal SalaryStdDev { get; set; }

        /// <summary>
        /// 子部门报告
        /// </summary>
        public List<DepartmentPayrollReportDto> SubDepartments { get; set; } = new List<DepartmentPayrollReportDto>();
    }

    /// <summary>
    /// 工资统计报告DTO
    /// </summary>
    public class PayrollStatisticsReportDto
    {
        /// <summary>
        /// 报告ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 报告名称
        /// </summary>
        public string ReportName { get; set; } = string.Empty;

        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatisticsDate { get; set; }

        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 总应发工资
        /// </summary>
        public decimal TotalGrossPay { get; set; }

        /// <summary>
        /// 总实发工资
        /// </summary>
        public decimal TotalNetPay { get; set; }

        /// <summary>
        /// 总个人所得税
        /// </summary>
        public decimal TotalIncomeTax { get; set; }

        /// <summary>
        /// 总社保费用
        /// </summary>
        public decimal TotalSocialSecurity { get; set; }

        /// <summary>
        /// 总公积金费用
        /// </summary>
        public decimal TotalHousingFund { get; set; }

        /// <summary>
        /// 总人工成本
        /// </summary>
        public decimal TotalLaborCost { get; set; }

        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }

        /// <summary>
        /// 工资分布
        /// </summary>
        public List<SalaryDistribution> SalaryDistribution { get; set; } = new List<SalaryDistribution>();

        /// <summary>
        /// 部门统计
        /// </summary>
        public List<DepartmentStatistics> DepartmentStatistics { get; set; } = new List<DepartmentStatistics>();

        /// <summary>
        /// 工资趋势
        /// </summary>
        public List<SalaryTrend> SalaryTrends { get; set; } = new List<SalaryTrend>();
    }

    /// <summary>
    /// 工资分布统计
    /// </summary>
    public class SalaryDistribution
    {
        /// <summary>
        /// 区间名称
        /// </summary>
        public string RangeName { get; set; } = string.Empty;

        /// <summary>
        /// 最小值
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 部门统计信息
    /// </summary>
    public class DepartmentStatistics
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 应发工资总额
        /// </summary>
        public decimal TotalGrossPay { get; set; }

        /// <summary>
        /// 实发工资总额
        /// </summary>
        public decimal TotalNetPay { get; set; }

        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 工资趋势
    /// </summary>
    public class SalaryTrend
    {
        /// <summary>
        /// 期间
        /// </summary>
        public string Period { get; set; } = string.Empty;

        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal TotalSalary { get; set; }

        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 增长率
        /// </summary>
        public decimal GrowthRate { get; set; }
    }
}