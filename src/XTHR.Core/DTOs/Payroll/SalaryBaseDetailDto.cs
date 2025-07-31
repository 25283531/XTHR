using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 工资基础信息详情DTO
    /// </summary>
    public class SalaryBaseDetailDto
    {
        /// <summary>
        /// 工资基础ID
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
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BaseSalary { get; set; }

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
        /// 交通补贴
        /// </summary>
        public decimal TransportationAllowance { get; set; }

        /// <summary>
        /// 餐补
        /// </summary>
        public decimal MealAllowance { get; set; }

        /// <summary>
        /// 通讯补贴
        /// </summary>
        public decimal CommunicationAllowance { get; set; }

        /// <summary>
        /// 其他补贴
        /// </summary>
        public decimal OtherAllowance { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}