using System.Collections.Generic;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置导入DTO
    /// </summary>
    public class ConfigImportDto
    {
        /// <summary>
        /// 配置列表
        /// </summary>
        public List<ConfigItemDto> Configs { get; set; } = new List<ConfigItemDto>();

        /// <summary>
        /// 导入来源
        /// </summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// 导入描述
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// 系统配置导入DTO
    /// </summary>
    public class SystemConfigImportDto : ConfigItemDto
    {
        /// <summary>
        /// 是否覆盖已存在的配置
        /// </summary>
        public bool OverwriteExisting { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public new bool IsEnabled { get; set; } = true;
    }

    /// <summary>
    /// 配置导出DTO
    /// </summary>
    public class ConfigExportDto
    {
        /// <summary>
        /// 配置列表
        /// </summary>
        public List<ConfigItemDto> Configs { get; set; } = new List<ConfigItemDto>();

        /// <summary>
        /// 导出时间
        /// </summary>
        public DateTime ExportTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 导出人
        /// </summary>
        public string ExportedBy { get; set; } = string.Empty;

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "1.0";
    }
}