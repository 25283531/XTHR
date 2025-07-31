using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 创建系统配置请求
    /// </summary>
    public class CreateSystemConfigRequest
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required(ErrorMessage = "配置键不能为空")]
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置值
        /// </summary>
        [Required(ErrorMessage = "配置值不能为空")]
        public string ConfigValue { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        [Required(ErrorMessage = "配置名称不能为空")]
        public string ConfigName { get; set; } = string.Empty;

        /// <summary>
        /// 配置描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string? DataType { get; set; } = "string";

        /// <summary>
        /// 默认值
        /// </summary>
        public string? DefaultValue { get; set; }

        /// <summary>
        /// 是否必需
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// 是否敏感数据
        /// </summary>
        public bool IsSensitive { get; set; } = false;

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; set; } = 0;
    }
}