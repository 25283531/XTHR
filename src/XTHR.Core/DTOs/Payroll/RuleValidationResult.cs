using System.Collections.Generic;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 规则验证结果
    /// </summary>
    public class RuleValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; } = string.Empty;

        /// <summary>
        /// 规则描述
        /// </summary>
        public string RuleDescription { get; set; } = string.Empty;

        /// <summary>
        /// 错误消息列表
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 违反规则的员工列表
        /// </summary>
        public List<int> ViolatingEmployeeIds { get; set; } = new List<int>();

        /// <summary>
        /// 验证时间
        /// </summary>
        public DateTime ValidationTime { get; set; } = DateTime.Now;
    }
}