namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 年龄分布统计DTO
    /// </summary>
    public class AgeDistributionDto
    {
        /// <summary>
        /// 年龄段
        /// </summary>
        public string AgeRange { get; set; }

        /// <summary>
        /// 该年龄段员工数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占总员工比例（%）
        /// </summary>
        public double Percentage { get; set; }
    }
}