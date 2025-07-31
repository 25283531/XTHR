using System.Collections.Generic;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置导入结果
    /// </summary>
    public class ConfigImportResult
    {
        /// <summary>
        /// 导入是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 导入成功的配置项数量
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// 导入失败的配置项数量
        /// </summary>
        public int FailureCount { get; set; }

        /// <summary>
        /// 跳过的配置项数量
        /// </summary>
        public int SkipCount { get; set; }

        /// <summary>
        /// 错误消息列表
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new List<string>();

        /// <summary>
        /// 警告消息列表
        /// </summary>
        public List<string> WarningMessages { get; set; } = new List<string>();

        /// <summary>
        /// 导入的配置项
        /// </summary>
        public List<ConfigItemDto> ImportedConfigs { get; set; } = new List<ConfigItemDto>();
    }

    /// <summary>
    /// 配置项DTO
    /// </summary>
    public class ConfigItemDto
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
        /// 配置描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 配置类别
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }
}