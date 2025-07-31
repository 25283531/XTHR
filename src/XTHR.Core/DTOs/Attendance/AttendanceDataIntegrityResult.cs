using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤数据完整性检查结果
    /// </summary>
    public class AttendanceDataIntegrityResult
    {
        /// <summary>
        /// 检查总数
        /// </summary>
        public int TotalChecked { get; set; }

        /// <summary>
        /// 问题数量
        /// </summary>
        public int IssuesFound { get; set; }

        /// <summary>
        /// 数据完整性百分比
        /// </summary>
        public decimal IntegrityPercentage { get; set; }

        /// <summary>
        /// 发现的问题列表
        /// </summary>
        public List<DataIntegrityIssue> Issues { get; set; } = new List<DataIntegrityIssue>();

        /// <summary>
        /// 建议的修复操作
        /// </summary>
        public List<string> SuggestedFixes { get; set; } = new List<string>();

        /// <summary>
        /// 是否通过检查
        /// </summary>
        public bool IsValid => IssuesFound == 0;
    }

    /// <summary>
    /// 数据完整性问题
    /// </summary>
    public class DataIntegrityIssue
    {
        /// <summary>
        /// 问题类型
        /// </summary>
        public string IssueType { get; set; } = string.Empty;

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 受影响的记录数
        /// </summary>
        public int AffectedRecords { get; set; }

        /// <summary>
        /// 严重程度 (High, Medium, Low)
        /// </summary>
        public string Severity { get; set; } = string.Empty;

        /// <summary>
        /// 示例数据
        /// </summary>
        public string SampleData { get; set; } = string.Empty;
    }
}