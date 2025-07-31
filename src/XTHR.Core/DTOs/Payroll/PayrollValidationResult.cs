using System.Collections.Generic;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 薪资验证结果
    /// </summary>
    public class PayrollValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 错误消息列表
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 警告消息列表
        /// </summary>
        public List<string> Warnings { get; set; } = new List<string>();

        /// <summary>
        /// 验证详细信息
        /// </summary>
        public List<ValidationDetail> Details { get; set; } = new List<ValidationDetail>();
    }

    /// <summary>
    /// 验证详细信息
    /// </summary>
    public class ValidationDetail
    {
        /// <summary>
        /// 验证项名称
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// 验证结果
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// 警告消息
        /// </summary>
        public string WarningMessage { get; set; } = string.Empty;
    }
}