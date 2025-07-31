using System.Collections.Generic;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置依赖检查结果
    /// </summary>
    public class ConfigDependencyCheckResult
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 是否检查通过
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 依赖配置列表
        /// </summary>
        public List<ConfigDependency> Dependencies { get; set; } = new();

        /// <summary>
        /// 缺失的依赖配置
        /// </summary>
        public List<string> MissingDependencies { get; set; } = new();

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 警告信息
        /// </summary>
        public List<string> Warnings { get; set; } = new();
    }

    /// <summary>
    /// 配置依赖项
    /// </summary>
    public class ConfigDependency
    {
        /// <summary>
        /// 依赖的配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; } = string.Empty;

        /// <summary>
        /// 依赖关系类型
        /// </summary>
        public string DependencyType { get; set; } = string.Empty;

        /// <summary>
        /// 依赖条件
        /// </summary>
        public string? DependencyCondition { get; set; }

        /// <summary>
        /// 是否必需
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 当前值
        /// </summary>
        public string? CurrentValue { get; set; }
    }
}