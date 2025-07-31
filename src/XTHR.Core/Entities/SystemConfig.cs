using System;
using System.ComponentModel.DataAnnotations;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 系统配置实体
    /// </summary>
    public class SystemConfig : BaseEntity<string>
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Key]
        [MaxLength(100)]
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置值
        /// </summary>
        [MaxLength(1000)]
        public string ConfigValue { get; set; } = string.Empty;

        /// <summary>
        /// 配置描述
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 配置分组
        /// </summary>
        [MaxLength(50)]
        public string Group { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;



        /// <summary>
        /// 配置类型
        /// </summary>
        [MaxLength(20)]
        public string ConfigType { get; set; } = "string";

        /// <summary>
        /// 验证规则
        /// </summary>
        [MaxLength(200)]
        public string ValidationRule { get; set; } = string.Empty;
    }
}