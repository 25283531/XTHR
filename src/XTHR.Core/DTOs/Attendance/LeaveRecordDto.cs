using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 请假记录DTO
    /// </summary>
    public class LeaveRecordDto
    {
        /// <summary>
        /// 记录ID
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
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal LeaveDays { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }
    }
}