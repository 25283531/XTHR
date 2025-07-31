using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工导出请求
    /// </summary>
    public class EmployeeExportRequest
    {
        /// <summary>
        /// 员工ID列表，为空表示导出所有
        /// </summary>
        public List<int>? EmployeeIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int>? DepartmentIds { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// 入职日期范围-开始
        /// </summary>
        public DateTime? HireDateStart { get; set; }

        /// <summary>
        /// 入职日期范围-结束
        /// </summary>
        public DateTime? HireDateEnd { get; set; }

        /// <summary>
        /// 导出字段列表
        /// </summary>
        public List<string>? ExportFields { get; set; }

        /// <summary>
        /// 导出格式：Excel、CSV
        /// </summary>
        public string ExportFormat { get; set; } = "Excel";

        /// <summary>
        /// 是否包含薪资信息
        /// </summary>
        public bool IncludeSalary { get; set; }

        /// <summary>
        /// 是否包含合同信息
        /// </summary>
        public bool IncludeContract { get; set; }

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