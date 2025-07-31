using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工查询请求DTO
    /// </summary>
    public class EmployeeQueryRequest
    {
        /// <summary>
        /// 员工姓名关键字
        /// </summary>
        public string? EmployeeName { get; set; }

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
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; } = 20;

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