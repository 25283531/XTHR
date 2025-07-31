using System;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置备份DTO
    /// </summary>
    public class ConfigBackupDto
    {
        /// <summary>
        /// 备份ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 备份名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 备份描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 备份分类
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// 备份文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 备份时间
        /// </summary>
        public DateTime BackupTime { get; set; }

        /// <summary>
        /// 备份用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 备份用户名称
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 配置数量
        /// </summary>
        public int ConfigCount { get; set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }
    }
}