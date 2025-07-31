using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工年龄分布DTO
    /// </summary>
    public class EmployeeAgeDistributionDto
    {
        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 平均年龄
        /// </summary>
        public decimal AverageAge { get; set; }

        /// <summary>
        /// 年龄分布
        /// </summary>
        public List<AgeGroupStatistics> AgeGroups { get; set; } = new();
    }

    /// <summary>
    /// 年龄组统计DTO
    /// </summary>
    public class AgeGroupStatistics
    {
        /// <summary>
        /// 年龄组名称
        /// </summary>
        public string GroupName { get; set; } = string.Empty;

        /// <summary>
        /// 最小年龄
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// 员工数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }
}