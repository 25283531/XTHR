using System;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置版本DTO
    /// </summary>
    public class ConfigVersionDto
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 是否为当前版本
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变更描述
        /// </summary>
        public string ChangeDescription { get; set; }
    }
}