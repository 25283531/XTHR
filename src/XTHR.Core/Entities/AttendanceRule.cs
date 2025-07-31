using System;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 考勤规则
    /// </summary>
    public class AttendanceRule : BaseEntity<int>
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 规则类型
        /// </summary>
        public string RuleType { get; set; } = string.Empty;

        /// <summary>
        /// 上班时间
        /// </summary>
        public TimeSpan WorkStartTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public TimeSpan WorkEndTime { get; set; }

        /// <summary>
        /// 迟到容忍分钟数
        /// </summary>
        public int LateToleranceMinutes { get; set; }

        /// <summary>
        /// 早退容忍分钟数
        /// </summary>
        public int EarlyLeaveToleranceMinutes { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}