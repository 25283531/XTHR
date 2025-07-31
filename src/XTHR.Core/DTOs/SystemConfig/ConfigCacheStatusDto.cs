namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置缓存状态DTO
    /// </summary>
    public class ConfigCacheStatusDto
    {
        /// <summary>
        /// 缓存键
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 缓存值
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// 是否已缓存
        /// </summary>
        public bool IsCached { get; set; }

        /// <summary>
        /// 缓存过期时间（分钟）
        /// </summary>
        public int ExpireMinutes { get; set; }

        /// <summary>
        /// 缓存大小（字节）
        /// </summary>
        public long CacheSize { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int AccessCount { get; set; }

        /// <summary>
        /// 缓存命中率
        /// </summary>
        public double HitRate { get; set; }
    }
}