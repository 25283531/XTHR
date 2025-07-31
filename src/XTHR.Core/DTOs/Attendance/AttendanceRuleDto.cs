using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤规则DTO
    /// </summary>
    public class AttendanceRuleDto
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; } = string.Empty;

        /// <summary>
        /// 规则类型：1-固定班次，2-弹性班次，3-轮班制
        /// </summary>
        public int RuleType { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        public TimeSpan WorkStartTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public TimeSpan WorkEndTime { get; set; }

        /// <summary>
        /// 允许迟到分钟数
        /// </summary>
        public int LateAllowanceMinutes { get; set; }

        /// <summary>
        /// 允许早退分钟数
        /// </summary>
        public int EarlyLeaveAllowanceMinutes { get; set; }

        /// <summary>
        /// 工作时长（小时）
        /// </summary>
        public decimal WorkHours { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;
    }
}