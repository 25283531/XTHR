using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤异常报告DTO
    /// </summary>
    public class AttendanceAnomalyReportDto
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
        /// 异常日期
        /// </summary>
        public DateTime AnomalyDate { get; set; }

        /// <summary>
        /// 异常类型：1-迟到，2-早退，3-缺勤，4-未打卡
        /// </summary>
        public int AnomalyType { get; set; }

        /// <summary>
        /// 异常类型名称
        /// </summary>
        public string AnomalyTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 应上班时间
        /// </summary>
        public TimeSpan? ShouldWorkStartTime { get; set; }

        /// <summary>
        /// 应下班时间
        /// </summary>
        public TimeSpan? ShouldWorkEndTime { get; set; }

        /// <summary>
        /// 实际上班时间
        /// </summary>
        public TimeSpan? ActualWorkStartTime { get; set; }

        /// <summary>
        /// 实际下班时间
        /// </summary>
        public TimeSpan? ActualWorkEndTime { get; set; }

        /// <summary>
        /// 迟到/早退分钟数
        /// </summary>
        public int? MinutesLateOrEarly { get; set; }

        /// <summary>
        /// 异常原因
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// 是否已处理
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// 处理状态：0-未处理，1-已处理，2-已忽略
        /// </summary>
        public int ProcessStatus { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public int? ProcessedBy { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string? ProcessedByName { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? ProcessedAt { get; set; }

        /// <summary>
        /// 处理备注
        /// </summary>
        public string? ProcessRemark { get; set; }
    }
}