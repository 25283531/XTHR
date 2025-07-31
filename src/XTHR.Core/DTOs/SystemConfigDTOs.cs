using System;
using System.Collections.Generic;
using XTHR.Core.DTOs.Requests;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 系统配置详情DTO
    /// </summary>
    public class SystemConfigDetailDto : BaseDto
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 配置分类
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        
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
        /// 验证规则
        /// </summary>
        public string ValidationRule { get; set; }
        
        /// <summary>
        /// 可选值列表（JSON格式）
        /// </summary>
        public string OptionValues { get; set; }
        
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastModifiedBy { get; set; }
        
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedAt { get; set; }
        
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
    }
    
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
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置值（敏感数据会被掩码）
        /// </summary>
        public string ConfigValue { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 配置分类
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        
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
    
    /// <summary>
    /// 配置分类DTO
    /// </summary>
    public class ConfigCategoryDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 配置数量
        /// </summary>
        public int ConfigCount { get; set; }
        
        /// <summary>
        /// 启用配置数量
        /// </summary>
        public int EnabledConfigCount { get; set; }
        
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortOrder { get; set; }
    }
    
    /// <summary>
    /// 配置变更历史DTO
    /// </summary>
    public class ConfigChangeHistoryDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 配置ID
        /// </summary>
        public int ConfigId { get; set; }
        
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }
        
        /// <summary>
        /// 旧值
        /// </summary>
        public string OldValue { get; set; }
        
        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }
        
        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }
        
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatedBy { get; set; }
        
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatedAt { get; set; }
        
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
        
        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }
    }
    
    /// <summary>
    /// 配置操作日志DTO
    /// </summary>
    public class ConfigOperationLogDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }
        
        /// <summary>
        /// 操作对象
        /// </summary>
        public string OperationTarget { get; set; }
        
        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperationDescription { get; set; }
        
        /// <summary>
        /// 操作结果
        /// </summary>
        public string OperationResult { get; set; }
        
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatedBy { get; set; }
        
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperatedAt { get; set; }
        
        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
        
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 配置导入结果DTO
    /// </summary>
    public class ConfigImportResultDto
    {
        /// <summary>
        /// 导入总数
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// 成功导入数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败导入数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 跳过数量
        /// </summary>
        public int SkippedCount { get; set; }
        
        /// <summary>
        /// 更新数量
        /// </summary>
        public int UpdatedCount { get; set; }
        
        /// <summary>
        /// 新增数量
        /// </summary>
        public int CreatedCount { get; set; }
        
        /// <summary>
        /// 成功导入的配置
        /// </summary>
        public List<SystemConfigListDto> SuccessConfigs { get; set; } = new List<SystemConfigListDto>();
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<ConfigImportFailure> FailureRecords { get; set; } = new List<ConfigImportFailure>();
        
        /// <summary>
        /// 导入时间
        /// </summary>
        public DateTime ImportTime { get; set; }
        
        /// <summary>
        /// 导入人
        /// </summary>
        public string ImportedBy { get; set; }
        
        /// <summary>
        /// 导入文件名
        /// </summary>
        public string ImportFileName { get; set; }
    }
    
    /// <summary>
    /// 配置导入失败记录
    /// </summary>
    public class ConfigImportFailure
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }
        
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; }
        
        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> OriginalData { get; set; } = new Dictionary<string, string>();
    }
    
    /// <summary>
    /// 配置导出结果DTO
    /// </summary>
    public class ConfigExportResultDto
    {
        /// <summary>
        /// 导出文件名
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// 导出格式
        /// </summary>
        public string ExportFormat { get; set; }
        
        /// <summary>
        /// 导出配置数量
        /// </summary>
        public int ConfigCount { get; set; }
        
        /// <summary>
        /// 导出时间
        /// </summary>
        public DateTime ExportTime { get; set; }
        
        /// <summary>
        /// 导出人
        /// </summary>
        public string ExportedBy { get; set; }
        
        /// <summary>
        /// 导出范围
        /// </summary>
        public string ExportScope { get; set; }
    }
    
    /// <summary>
    /// 配置缓存统计DTO
    /// </summary>
    public class ConfigCacheStatisticsDto
    {
        /// <summary>
        /// 缓存总数
        /// </summary>
        public int TotalCacheCount { get; set; }
        
        /// <summary>
        /// 命中次数
        /// </summary>
        public long HitCount { get; set; }
        
        /// <summary>
        /// 未命中次数
        /// </summary>
        public long MissCount { get; set; }
        
        /// <summary>
        /// 命中率
        /// </summary>
        public decimal HitRate { get; set; }
        
        /// <summary>
        /// 缓存大小（字节）
        /// </summary>
        public long CacheSize { get; set; }
        
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        
        /// <summary>
        /// 缓存详情
        /// </summary>
        public List<ConfigCacheDetail> CacheDetails { get; set; } = new List<ConfigCacheDetail>();
    }
    
    /// <summary>
    /// 配置缓存详情
    /// </summary>
    public class ConfigCacheDetail
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 缓存值
        /// </summary>
        public string CacheValue { get; set; }
        
        /// <summary>
        /// 缓存时间
        /// </summary>
        public DateTime CacheTime { get; set; }
        
        /// <summary>
        /// 访问次数
        /// </summary>
        public long AccessCount { get; set; }
        
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastAccessTime { get; set; }
        
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiryTime { get; set; }
    }
    
    /// <summary>
    /// 系统健康检查配置DTO
    /// </summary>
    public class SystemHealthCheckConfigDto
    {
        /// <summary>
        /// 数据库连接检查
        /// </summary>
        public bool DatabaseConnectionCheck { get; set; }
        
        /// <summary>
        /// 缓存服务检查
        /// </summary>
        public bool CacheServiceCheck { get; set; }
        
        /// <summary>
        /// 文件系统检查
        /// </summary>
        public bool FileSystemCheck { get; set; }
        
        /// <summary>
        /// 外部服务检查
        /// </summary>
        public bool ExternalServiceCheck { get; set; }
        
        /// <summary>
        /// 内存使用检查
        /// </summary>
        public bool MemoryUsageCheck { get; set; }
        
        /// <summary>
        /// CPU使用检查
        /// </summary>
        public bool CpuUsageCheck { get; set; }
        
        /// <summary>
        /// 磁盘空间检查
        /// </summary>
        public bool DiskSpaceCheck { get; set; }
        
        /// <summary>
        /// 检查间隔（秒）
        /// </summary>
        public int CheckInterval { get; set; }
        
        /// <summary>
        /// 超时时间（秒）
        /// </summary>
        public int TimeoutSeconds { get; set; }
        
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }
        
        /// <summary>
        /// 告警阈值配置
        /// </summary>
        public Dictionary<string, decimal> AlertThresholds { get; set; } = new Dictionary<string, decimal>();
    }
    
    /// <summary>
    /// 配置验证结果DTO
    /// </summary>
    public class ConfigValidationResultDto
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 验证的配置数量
        /// </summary>
        public int ValidatedCount { get; set; }
        
        /// <summary>
        /// 通过验证的数量
        /// </summary>
        public int PassedCount { get; set; }
        
        /// <summary>
        /// 验证失败的数量
        /// </summary>
        public int FailedCount { get; set; }
        
        /// <summary>
        /// 验证错误列表
        /// </summary>
        public List<ConfigValidationError> ValidationErrors { get; set; } = new List<ConfigValidationError>();
        
        /// <summary>
        /// 验证警告列表
        /// </summary>
        public List<ConfigValidationWarning> ValidationWarnings { get; set; } = new List<ConfigValidationWarning>();
        
        /// <summary>
        /// 验证时间
        /// </summary>
        public DateTime ValidationTime { get; set; }
        
        /// <summary>
        /// 验证人
        /// </summary>
        public string ValidatedBy { get; set; }
    }
    
    /// <summary>
    /// 配置验证错误
    /// </summary>
    public class ConfigValidationError
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 当前值
        /// </summary>
        public string CurrentValue { get; set; }
        
        /// <summary>
        /// 期望值
        /// </summary>
        public string ExpectedValue { get; set; }
        
        /// <summary>
        /// 严重级别
        /// </summary>
        public string Severity { get; set; }
    }
    
    /// <summary>
    /// 配置验证警告
    /// </summary>
    public class ConfigValidationWarning
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 警告类型
        /// </summary>
        public string WarningType { get; set; }
        
        /// <summary>
        /// 警告消息
        /// </summary>
        public string WarningMessage { get; set; }
        
        /// <summary>
        /// 建议操作
        /// </summary>
        public string SuggestedAction { get; set; }
    }
}