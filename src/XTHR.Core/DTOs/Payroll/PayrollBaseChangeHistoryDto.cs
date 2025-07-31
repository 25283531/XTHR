using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 薪资基础变更历史DTO
    /// </summary>
    public class PayrollBaseChangeHistoryDto
    {
        /// <summary>
        /// 变更ID
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
        /// 变更字段
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// 旧值
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string? NewValue { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatedBy { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string? OperatedByName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime? EffectiveDate { get; set; }
    }
}