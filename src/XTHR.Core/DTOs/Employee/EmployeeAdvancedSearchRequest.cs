using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工高级搜索请求DTO
    /// </summary>
    public class EmployeeAdvancedSearchRequest
    {
        /// <summary>
        /// 员工姓名关键字
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string? EmployeeNumber { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int>? DepartmentIds { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }

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
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 是否包含离职员工
        /// </summary>
        public bool IncludeInactive { get; set; } = false;

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