using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工花名册报表DTO
    /// </summary>
    public class EmployeeRosterReportDto
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
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; } = string.Empty;

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = string.Empty;
    }
}