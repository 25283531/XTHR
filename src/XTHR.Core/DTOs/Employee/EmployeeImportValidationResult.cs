using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工导入验证结果
    /// </summary>
    public class EmployeeImportValidationResult
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 验证错误
        /// </summary>
        public List<ValidationError> Errors { get; set; } = new();

        /// <summary>
        /// 验证警告
        /// </summary>
        public List<ValidationWarning> Warnings { get; set; } = new();

        /// <summary>
        /// 验证通过的数据
        /// </summary>
        public List<EmployeeImportData> ValidData { get; set; } = new();
    }

    /// <summary>
    /// 验证错误
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 验证警告
    /// </summary>
    public class ValidationWarning
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// 警告消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}