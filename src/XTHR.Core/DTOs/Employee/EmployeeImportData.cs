using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工导入数据
    /// </summary>
    public class EmployeeImportData
    {
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
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PositionSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }
    }
}