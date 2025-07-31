using System;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 系统配置列表DTO
    /// </summary>
    public class SystemConfigListDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置值（敏感数据会被掩码）
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
        /// 数据类型
        /// </summary>
        public string DataType { get; set; } = string.Empty;

        /// <summary>
        /// 是否必需
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 是否敏感数据
        /// </summary>
        public bool IsSensitive { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedAt { get; set; }
    }
}