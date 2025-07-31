using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工统计信息DTO
    /// </summary>
    public class EmployeeStatisticsDto
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
        /// 本月新入职员工数
        /// </summary>
        public int NewEmployeesThisMonth { get; set; }

        /// <summary>
        /// 本月离职员工数
        /// </summary>
        public int ResignedEmployeesThisMonth { get; set; }

        /// <summary>
        /// 平均在职时长（天）
        /// </summary>
        public double AverageTenureDays { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatisticsDate { get; set; }
    }
}