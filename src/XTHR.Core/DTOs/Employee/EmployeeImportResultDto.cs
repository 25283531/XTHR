using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工导入结果
    /// </summary>
    public class EmployeeImportResultDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 导入总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 成功数量
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// 失败数量
        /// </summary>
        public int FailedCount { get; set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        public EmployeeImportValidationResult ValidationResult { get; set; } = new();

        /// <summary>
        /// 错误消息
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new();
    }
}