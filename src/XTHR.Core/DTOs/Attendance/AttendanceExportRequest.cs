using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤数据导出请求
    /// </summary>
    public class AttendanceExportRequest
    {
        /// <summary>
        /// 员工ID列表，为空表示导出所有员工
        /// </summary>
        public List<int>? EmployeeIds { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        public List<int>? DepartmentIds { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 导出类型：1-考勤记录，2-异常记录，3-统计汇总
        /// </summary>
        public int ExportType { get; set; } = 1;

        /// <summary>
        /// 导出格式：Excel、CSV
        /// </summary>
        public string ExportFormat { get; set; } = "Excel";

        /// <summary>
        /// 是否包含详细信息
        /// </summary>
        public bool IncludeDetails { get; set; } = true;

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