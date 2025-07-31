using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 部门薪资汇总DTO
    /// </summary>
    public class DepartmentPayrollSummaryDto
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
        public decimal TotalShouldPay { get; set; }

        /// <summary>
        /// 实发工资总额
        /// </summary>
        public decimal TotalActualPay { get; set; }

        /// <summary>
        /// 社保扣除总额
        /// </summary>
        public decimal TotalSocialSecurity { get; set; }

        /// <summary>
        /// 公积金扣除总额
        /// </summary>
        public decimal TotalProvidentFund { get; set; }

        /// <summary>
        /// 个税扣除总额
        /// </summary>
        public decimal TotalPersonalTax { get; set; }

        /// <summary>
        /// 工资月份
        /// </summary>
        public string PayrollMonth { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}