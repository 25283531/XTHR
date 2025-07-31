using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤数据完整性检查结果
    /// </summary>
    public class AttendanceIntegrityCheckResult
    {
        /// <summary>
        /// 是否通过检查
        /// </summary>
        public bool IsPassed { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 异常记录数
        /// </summary>
        public int AnomalyRecords { get; set; }

        /// <summary>
        /// 缺失记录数
        /// </summary>
        public int MissingRecords { get; set; }

        /// <summary>
        /// 重复记录数
        /// </summary>
        public int DuplicateRecords { get; set; }

        /// <summary>
        /// 异常详情
        /// </summary>
        public List<IntegrityCheckDetail> Details { get; set; } = new();

        /// <summary>
        /// 建议操作
        /// </summary>
        public List<string> Suggestions { get; set; } = new();
    }

    /// <summary>
    /// 完整性检查详情
    /// </summary>
    public class IntegrityCheckDetail
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 异常类型：1-缺失打卡，2-时间异常，3-重复记录
        /// </summary>
        public int AnomalyType { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 异常日期
        /// </summary>
        public System.DateTime AnomalyDate { get; set; }
    }
}