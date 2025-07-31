using System;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 考勤记录查询请求
    /// </summary>
    public class AttendanceRecordQueryRequest
    {
        /// <summary>
        /// 员工ID（可选）
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 部门ID（可选）
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 考勤状态（可选）
        /// </summary>
        public string AttendanceStatus { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; } = "AttendanceDate";

        /// <summary>
        /// 是否降序
        /// </summary>
        public bool Descending { get; set; } = true;
    }
}