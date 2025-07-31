using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工变动报表DTO
    /// </summary>
    public class EmployeeChangeReportDto
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
        /// 工号
        /// </summary>
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 变动类型
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变动前部门
        /// </summary>
        public string PreviousDepartment { get; set; } = string.Empty;

        /// <summary>
        /// 变动后部门
        /// </summary>
        public string NewDepartment { get; set; } = string.Empty;

        /// <summary>
        /// 变动前职位
        /// </summary>
        public string PreviousPosition { get; set; } = string.Empty;

        /// <summary>
        /// 变动后职位
        /// </summary>
        public string NewPosition { get; set; } = string.Empty;

        /// <summary>
        /// 变动日期
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 变动原因
        /// </summary>
        public string ChangeReason { get; set; } = string.Empty;
    }
}