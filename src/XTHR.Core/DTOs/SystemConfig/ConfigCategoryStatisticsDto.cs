using System.Collections.Generic;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置分类统计DTO
    /// </summary>
    public class ConfigCategoryStatisticsDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 分类描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 配置项数量
        /// </summary>
        public int ConfigCount { get; set; }

        /// <summary>
        /// 启用配置数量
        /// </summary>
        public int EnabledCount { get; set; }

        /// <summary>
        /// 禁用配置数量
        /// </summary>
        public int DisabledCount { get; set; }

        /// <summary>
        /// 启用率
        /// </summary>
        public decimal EnableRate { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        public string? LastUpdatedAt { get; set; }

        /// <summary>
        /// 最近更新人
        /// </summary>
        public string? LastUpdatedBy { get; set; }
    }

    /// <summary>
    /// 配置统计结果
    /// </summary>
    public class ConfigStatisticsResult
    {
        /// <summary>
        /// 总配置数量
        /// </summary>
        public int TotalConfigs { get; set; }

        /// <summary>
        /// 启用配置数量
        /// </summary>
        public int EnabledConfigs { get; set; }

        /// <summary>
        /// 禁用配置数量
        /// </summary>
        public int DisabledConfigs { get; set; }

        /// <summary>
        /// 分类统计列表
        /// </summary>
        public List<ConfigCategoryStatisticsDto> CategoryStatistics { get; set; } = new();

        /// <summary>
        /// 数据类型统计
        /// </summary>
        public Dictionary<string, int> DataTypeStatistics { get; set; } = new();
    }
}