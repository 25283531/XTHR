namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 系统配置DTO
    /// </summary>
    public class SystemConfigDto
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; } = string.Empty;

        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 配置分类
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}