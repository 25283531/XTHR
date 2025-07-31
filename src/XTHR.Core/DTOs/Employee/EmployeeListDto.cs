using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工列表DTO
    /// </summary>
    public class EmployeeListDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}