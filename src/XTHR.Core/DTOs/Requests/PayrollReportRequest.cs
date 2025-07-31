using System;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 工资报告请求
    /// </summary>
    public class PayrollReportRequest
    {
        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int> DepartmentIds { get; set; } = new List<int>();

        /// <summary>
        /// 员工ID列表
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();

        /// <summary>
        /// 计算状态
        /// </summary>
        public string CalculationStatus { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; }

        /// <summary>
        /// 发放状态
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// 工资导出请求
    /// </summary>
    public class PayrollExportRequest
    {
        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int> DepartmentIds { get; set; } = new List<int>();

        /// <summary>
        /// 员工ID列表
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();

        /// <summary>
        /// 导出格式
        /// </summary>
        public string ExportFormat { get; set; } = "Excel";

        /// <summary>
        /// 包含列
        /// </summary>
        public List<string> IncludeColumns { get; set; } = new List<string>();

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = string.Empty;
    }

    /// <summary>
    /// 部门工资报告请求
    /// </summary>
    public class DepartmentPayrollReportRequest
    {
        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int> DepartmentIds { get; set; } = new List<int>();

        /// <summary>
        /// 包含子部门
        /// </summary>
        public bool IncludeSubDepartments { get; set; } = true;
    }
}