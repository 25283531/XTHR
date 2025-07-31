using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工变更历史DTO
    /// </summary>
    public class EmployeeChangeHistoryDto
    {
        /// <summary>
        /// 变更历史ID
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
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

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
        public string OperatedByName { get; set; } = string.Empty;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string? OperationIp { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}