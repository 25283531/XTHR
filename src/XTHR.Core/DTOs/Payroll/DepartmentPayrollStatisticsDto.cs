using System.Collections.Generic;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 部门工资统计DTO
    /// </summary>
    public class DepartmentPayrollStatisticsDto
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
        /// 总工资
        /// </summary>
        public decimal TotalSalary { get; set; }

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
        /// 基本工资总额
        /// </summary>
        public decimal TotalBaseSalary { get; set; }

        /// <summary>
        /// 岗位工资总额
        /// </summary>
        public decimal TotalPositionSalary { get; set; }

        /// <summary>
        /// 绩效工资总额
        /// </summary>
        public decimal TotalPerformanceSalary { get; set; }

        /// <summary>
        /// 津贴补贴总额
        /// </summary>
        public decimal TotalAllowance { get; set; }

        /// <summary>
        /// 扣款总额
        /// </summary>
        public decimal TotalDeduction { get; set; }

        /// <summary>
        /// 个税总额
        /// </summary>
        public decimal TotalTax { get; set; }

        /// <summary>
        /// 实发工资总额
        /// </summary>
        public decimal TotalNetSalary { get; set; }

        /// <summary>
        /// 员工工资详情
        /// </summary>
        public List<EmployeeSalaryDetailDto> EmployeeDetails { get; set; } = new();
    }

    /// <summary>
    /// 员工工资详情DTO
    /// </summary>
    public class EmployeeSalaryDetailDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal TotalSalary { get; set; }

        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetSalary { get; set; }
    }
}