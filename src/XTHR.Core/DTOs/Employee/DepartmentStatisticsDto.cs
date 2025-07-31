namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 部门统计信息DTO
    /// </summary>
    public class DepartmentStatisticsDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 部门员工总数
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
        /// 部门占比（%）
        /// </summary>
        public double DepartmentRatio { get; set; }
    }
}