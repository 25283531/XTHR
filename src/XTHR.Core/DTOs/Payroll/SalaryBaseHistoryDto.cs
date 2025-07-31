using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 工资基础信息变更历史DTO
    /// </summary>
    public class SalaryBaseHistoryDto
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
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; } = string.Empty;

        /// <summary>
        /// 变更前数据
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// 变更后数据
        /// </summary>
        public string NewValues { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ChangedAt { get; set; }
    }
}