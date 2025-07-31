using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 部门员工统计DTO
    /// </summary>
    public class DepartmentEmployeeStatisticsDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 员工总数
        /// </summary>
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 在职员工数
        /// </summary>
        public int ActiveEmployees { get; set; }

        /// <summary>
        /// 离职员工数
        /// </summary>
        public int InactiveEmployees { get; set; }

        /// <summary>
        /// 本月新入职员工数
        /// </summary>
        public int NewHires { get; set; }

        /// <summary>
        /// 本月离职员工数
        /// </summary>
        public int Resignations { get; set; }

        /// <summary>
        /// 子部门统计
        /// </summary>
        public List<DepartmentEmployeeStatisticsDto> SubDepartments { get; set; } = new();
    }
}