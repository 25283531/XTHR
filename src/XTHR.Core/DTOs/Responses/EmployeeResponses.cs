using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Responses
{
    /// <summary>
    /// 员工响应DTO
    /// </summary>
    public class EmployeeResponse
    {
        /// <summary>
        /// 响应状态
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 员工数据
        /// </summary>
        public object Data { get; set; } = new();
    }

    /// <summary>
    /// 员工列表响应DTO
    /// </summary>
    public class EmployeeListResponse
    {
        /// <summary>
        /// 员工总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 员工列表
        /// </summary>
        public List<object> Employees { get; set; } = new();

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
    }

    /// <summary>
    /// 员工统计响应DTO
    /// </summary>
    public class EmployeeStatisticsResponse
    {
        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 在职员工数
        /// </summary>
        public int ActiveEmployees { get; set; }

        /// <summary>
        /// 试用期员工数
        /// </summary>
        public int ProbationEmployees { get; set; }

        /// <summary>
        /// 离职员工数
        /// </summary>
        public int ResignedEmployees { get; set; }

        /// <summary>
        /// 部门分布统计
        /// </summary>
        public Dictionary<string, int> DepartmentDistribution { get; set; } = new();

        /// <summary>
        /// 入职趋势数据
        /// </summary>
        public List<object> HireTrend { get; set; } = new();
    }
}