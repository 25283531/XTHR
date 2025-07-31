using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工薪资基础DTO
    /// </summary>
    public class EmployeePayrollBaseDto
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
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

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
        /// 社保基数
        /// </summary>
        public decimal SocialSecurityBase { get; set; }

        /// <summary>
        /// 公积金基数
        /// </summary>
        public decimal ProvidentFundBase { get; set; }

        /// <summary>
        /// 个税起征点
        /// </summary>
        public decimal TaxThreshold { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}