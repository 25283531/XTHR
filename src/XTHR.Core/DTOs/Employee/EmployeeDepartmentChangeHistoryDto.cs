using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工部门变更历史DTO
    /// </summary>
    public class EmployeeDepartmentChangeHistoryDto
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
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 原部门ID
        /// </summary>
        public int OriginalDepartmentId { get; set; }

        /// <summary>
        /// 原部门名称
        /// </summary>
        public string OriginalDepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 新部门ID
        /// </summary>
        public int NewDepartmentId { get; set; }

        /// <summary>
        /// 新部门名称
        /// </summary>
        public string NewDepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 原职位ID
        /// </summary>
        public int? OriginalPositionId { get; set; }

        /// <summary>
        /// 原职位名称
        /// </summary>
        public string? OriginalPositionName { get; set; }

        /// <summary>
        /// 新职位ID
        /// </summary>
        public int? NewPositionId { get; set; }

        /// <summary>
        /// 新职位名称
        /// </summary>
        public string? NewPositionName { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; } = string.Empty;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string OperationIP { get; set; } = string.Empty;
    }
}