using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工导入请求
    /// </summary>
    public class EmployeeImportRequest
    {
        /// <summary>
        /// 导入文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件内容（Base64编码）
        /// </summary>
        public string FileContent { get; set; } = string.Empty;

        /// <summary>
        /// 是否覆盖现有数据
        /// </summary>
        public bool OverwriteExisting { get; set; }

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