using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤异常报告DTO
    /// </summary>
    public class AttendanceAbnormalReportDto
    {
        /// <summary>
        /// 报告ID
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 异常日期
        /// </summary>
        public DateTime AbnormalDate { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public AbnormalType Type { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 严重程度
        /// </summary>
        public SeverityLevel Severity { get; set; }

        /// <summary>
        /// 是否已处理
        /// </summary>
        public bool IsProcessed { get; set; }

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
        public DateTime? ProcessedTime { get; set; }

        /// <summary>
        /// 处理说明
        /// </summary>
        public string? ProcessNote { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }
    }

    /// <summary>
    /// 异常类型枚举
    /// </summary>
    public enum AbnormalType
    {
        /// <summary>
        /// 迟到
        /// </summary>
        Late = 1,

        /// <summary>
        /// 早退
        /// </summary>
        EarlyLeave = 2,

        /// <summary>
        /// 缺勤
        /// </summary>
        Absence = 3,

        /// <summary>
        /// 旷工
        /// </summary>
        Absenteeism = 4,

        /// <summary>
        /// 未打卡
        /// </summary>
        MissingPunch = 5,

        /// <summary>
        /// 异常打卡
        /// </summary>
        AbnormalPunch = 6
    }

    /// <summary>
    /// 严重程度枚举
    /// </summary>
    public enum SeverityLevel
    {
        /// <summary>
        /// 轻微
        /// </summary>
        Low = 1,

        /// <summary>
        /// 中等
        /// </summary>
        Medium = 2,

        /// <summary>
        /// 严重
        /// </summary>
        High = 3,

        /// <summary>
        /// 非常严重
        /// </summary>
        Critical = 4
    }
}