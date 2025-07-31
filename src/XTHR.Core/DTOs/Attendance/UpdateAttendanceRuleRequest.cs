using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 更新考勤规则请求
    /// </summary>
    public class UpdateAttendanceRuleRequest
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        [Required(ErrorMessage = "规则ID不能为空")]
        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        [Required(ErrorMessage = "规则名称不能为空")]
        [StringLength(100, ErrorMessage = "规则名称不能超过100个字符")]
        public string RuleName { get; set; } = string.Empty;

        /// <summary>
        /// 规则类型：1-固定班次，2-弹性班次，3-轮班制
        /// </summary>
        [Required(ErrorMessage = "规则类型不能为空")]
        [Range(1, 3, ErrorMessage = "规则类型必须在1-3之间")]
        public int RuleType { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        [Required(ErrorMessage = "上班时间不能为空")]
        public TimeSpan WorkStartTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        [Required(ErrorMessage = "下班时间不能为空")]
        public TimeSpan WorkEndTime { get; set; }

        /// <summary>
        /// 允许迟到分钟数
        /// </summary>
        [Range(0, 60, ErrorMessage = "迟到分钟数必须在0-60之间")]
        public int LateAllowanceMinutes { get; set; } = 5;

        /// <summary>
        /// 允许早退分钟数
        /// </summary>
        [Range(0, 60, ErrorMessage = "早退分钟数必须在0-60之间")]
        public int EarlyLeaveAllowanceMinutes { get; set; } = 5;

        /// <summary>
        /// 工作时长（小时）
        /// </summary>
        [Range(1, 24, ErrorMessage = "工作时长必须在1-24小时之间")]
        public decimal WorkHours { get; set; } = 8;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string? OperatorName { get; set; }
    }
}