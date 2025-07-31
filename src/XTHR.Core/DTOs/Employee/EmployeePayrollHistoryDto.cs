using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工薪资历史DTO
    /// </summary>
    public class EmployeePayrollHistoryDto
    {
        /// <summary>
        /// 历史记录ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 工资月份
        /// </summary>
        public DateTime PayrollMonth { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PositionSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴补贴
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetSalary { get; set; }

        /// <summary>
        /// 社保扣除
        /// </summary>
        public decimal SocialSecurityDeduction { get; set; }

        /// <summary>
        /// 公积金扣除
        /// </summary>
        public decimal ProvidentFundDeduction { get; set; }

        /// <summary>
        /// 个税扣除
        /// </summary>
        public decimal TaxDeduction { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}