using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工报表请求DTO
    /// </summary>
    public class EmployeeReportRequest
    {
        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int>? DepartmentIds { get; set; }

        /// <summary>
        /// 员工状态列表
        /// </summary>
        public List<string>? Statuses { get; set; }

        /// <summary>
        /// 入职日期开始
        /// </summary>
        public DateTime? HireDateStart { get; set; }

        /// <summary>
        /// 入职日期结束
        /// </summary>
        public DateTime? HireDateEnd { get; set; }

        /// <summary>
        /// 是否包含离职员工
        /// </summary>
        public bool IncludeInactive { get; set; } = false;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortBy { get; set; } = "EmployeeId";

        /// <summary>
        /// 排序方向
        /// </summary>
        public string SortDirection { get; set; } = "ASC";
    }
}