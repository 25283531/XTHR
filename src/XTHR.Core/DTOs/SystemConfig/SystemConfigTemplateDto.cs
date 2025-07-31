namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 系统配置模板DTO
    /// </summary>
    public class SystemConfigTemplateDto
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; } = string.Empty;

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        public string ValidationRule { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        public int SortOrder { get; set; }
    }
}