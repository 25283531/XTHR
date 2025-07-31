using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 系统配置搜索字段枚举
    /// </summary>
    public enum SystemConfigSearchField
    {
        /// <summary>
        /// 全部字段
        /// </summary>
        All,
        
        /// <summary>
        /// 配置键
        /// </summary>
        Key,
        
        /// <summary>
        /// 配置名称
        /// </summary>
        Name,
        
        /// <summary>
        /// 配置描述
        /// </summary>
        Description,
        
        /// <summary>
        /// 配置分类
        /// </summary>
        Category
    }
    /// <summary>
    /// 系统配置查询请求
    /// </summary>
    public class SystemConfigQueryRequest : BaseQueryRequest
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
        /// 配置分类
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; }
        
        /// <summary>
        /// 是否必需
        /// </summary>
        public bool? IsRequired { get; set; }
        
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool? IsReadOnly { get; set; }
        
        /// <summary>
        /// 是否敏感数据
        /// </summary>
        public bool? IsSensitive { get; set; }
        
        /// <summary>
        /// 搜索字段
        /// </summary>
        public SystemConfigSearchField SearchField { get; set; } = SystemConfigSearchField.All;
    }
    
    /// <summary>
    /// 创建系统配置请求
    /// </summary>
    public class CreateSystemConfigRequest
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required(ErrorMessage = "配置键不能为空")]
        [StringLength(100, ErrorMessage = "配置键长度不能超过100字符")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9._-]*$", ErrorMessage = "配置键格式不正确，只能包含字母、数字、点、下划线和连字符，且必须以字母开头")]
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        [Required(ErrorMessage = "配置值不能为空")]
        [StringLength(2000, ErrorMessage = "配置值长度不能超过2000字符")]
        public string ConfigValue { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        [Required(ErrorMessage = "配置名称不能为空")]
        [StringLength(100, ErrorMessage = "配置名称长度不能超过100字符")]
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 配置描述
        /// </summary>
        [StringLength(500, ErrorMessage = "配置描述长度不能超过500字符")]
        public string Description { get; set; }
        
        /// <summary>
        /// 配置分类
        /// </summary>
        [Required(ErrorMessage = "配置分类不能为空")]
        [StringLength(50, ErrorMessage = "配置分类长度不能超过50字符")]
        public string Category { get; set; }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        [Required(ErrorMessage = "数据类型不能为空")]
        [RegularExpression("^(String|Integer|Decimal|Boolean|DateTime|Json)$", ErrorMessage = "数据类型只能是String、Integer、Decimal、Boolean、DateTime或Json")]
        public string DataType { get; set; }
        
        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(2000, ErrorMessage = "默认值长度不能超过2000字符")]
        public string DefaultValue { get; set; }
        
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
        /// 验证规则
        /// </summary>
        [StringLength(500, ErrorMessage = "验证规则长度不能超过500字符")]
        public string ValidationRule { get; set; }
        
        /// <summary>
        /// 可选值列表（JSON格式）
        /// </summary>
        [StringLength(1000, ErrorMessage = "可选值列表长度不能超过1000字符")]
        public string OptionValues { get; set; }
        
        /// <summary>
        /// 排序号
        /// </summary>
        [Range(0, 9999, ErrorMessage = "排序号必须在0-9999之间")]
        public int SortOrder { get; set; } = 0;
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000, ErrorMessage = "备注长度不能超过1000字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 更新系统配置请求
    /// </summary>
    public class UpdateSystemConfigRequest
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "配置ID必须大于0")]
        public int Id { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        [Required(ErrorMessage = "配置值不能为空")]
        [StringLength(2000, ErrorMessage = "配置值长度不能超过2000字符")]
        public string ConfigValue { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        [StringLength(100, ErrorMessage = "配置名称长度不能超过100字符")]
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 配置描述
        /// </summary>
        [StringLength(500, ErrorMessage = "配置描述长度不能超过500字符")]
        public string Description { get; set; }
        
        /// <summary>
        /// 配置分类
        /// </summary>
        [StringLength(50, ErrorMessage = "配置分类长度不能超过50字符")]
        public string Category { get; set; }
        
        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(2000, ErrorMessage = "默认值长度不能超过2000字符")]
        public string DefaultValue { get; set; }
        
        /// <summary>
        /// 是否必需
        /// </summary>
        public bool? IsRequired { get; set; }
        
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnabled { get; set; }
        
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool? IsReadOnly { get; set; }
        
        /// <summary>
        /// 是否敏感数据
        /// </summary>
        public bool? IsSensitive { get; set; }
        
        /// <summary>
        /// 验证规则
        /// </summary>
        [StringLength(500, ErrorMessage = "验证规则长度不能超过500字符")]
        public string ValidationRule { get; set; }
        
        /// <summary>
        /// 可选值列表（JSON格式）
        /// </summary>
        [StringLength(1000, ErrorMessage = "可选值列表长度不能超过1000字符")]
        public string OptionValues { get; set; }
        
        /// <summary>
        /// 排序号
        /// </summary>
        [Range(0, 9999, ErrorMessage = "排序号必须在0-9999之间")]
        public int? SortOrder { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000, ErrorMessage = "备注长度不能超过1000字符")]
        public string Remarks { get; set; }
        
        /// <summary>
        /// 变更原因
        /// </summary>
        [Required(ErrorMessage = "变更原因不能为空")]
        [StringLength(500, ErrorMessage = "变更原因长度不能超过500字符")]
        public string ChangeReason { get; set; }
    }
    
    /// <summary>
    /// 批量设置配置请求
    /// </summary>
    public class BatchSetConfigRequest
    {
        /// <summary>
        /// 配置项列表
        /// </summary>
        [Required(ErrorMessage = "配置项列表不能为空")]
        [MinLength(1, ErrorMessage = "至少设置一个配置项")]
        public List<ConfigItem> ConfigItems { get; set; } = new List<ConfigItem>();
        
        /// <summary>
        /// 变更原因
        /// </summary>
        [Required(ErrorMessage = "变更原因不能为空")]
        [StringLength(500, ErrorMessage = "变更原因长度不能超过500字符")]
        public string ChangeReason { get; set; }
        
        /// <summary>
        /// 是否覆盖已存在的配置
        /// </summary>
        public bool OverrideExisting { get; set; } = false;
    }
    
    /// <summary>
    /// 配置项
    /// </summary>
    public class ConfigItem
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required(ErrorMessage = "配置键不能为空")]
        [StringLength(100, ErrorMessage = "配置键长度不能超过100字符")]
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        [Required(ErrorMessage = "配置值不能为空")]
        [StringLength(2000, ErrorMessage = "配置值长度不能超过2000字符")]
        public string ConfigValue { get; set; }
    }
    
    /// <summary>
    /// 批量更新配置状态请求
    /// </summary>
    public class BatchUpdateConfigStatusRequest
    {
        /// <summary>
        /// 配置ID列表
        /// </summary>
        [Required(ErrorMessage = "配置ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一个配置项")]
        public List<int> ConfigIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 新状态（启用/禁用）
        /// </summary>
        [Required(ErrorMessage = "新状态不能为空")]
        public bool IsEnabled { get; set; }
        
        /// <summary>
        /// 变更原因
        /// </summary>
        [Required(ErrorMessage = "变更原因不能为空")]
        [StringLength(500, ErrorMessage = "变更原因长度不能超过500字符")]
        public string ChangeReason { get; set; }
    }
    
    /// <summary>
    /// 配置导入请求
    /// </summary>
    public class ConfigImportRequest : BaseImportRequest
    {
        /// <summary>
        /// 导入数据
        /// </summary>
        [Required(ErrorMessage = "导入数据不能为空")]
        [MinLength(1, ErrorMessage = "至少导入一条配置")]
        public List<ConfigImportData> ImportData { get; set; } = new List<ConfigImportData>();
        
        /// <summary>
        /// 导入模式
        /// </summary>
        [Required(ErrorMessage = "导入模式不能为空")]
        [RegularExpression("^(新增|更新|覆盖)$", ErrorMessage = "导入模式只能是'新增'、'更新'或'覆盖'")]
        public string ImportMode { get; set; } = "新增";
        
        /// <summary>
        /// 是否跳过验证错误
        /// </summary>
        public bool SkipValidationErrors { get; set; } = false;
        
        /// <summary>
        /// 是否备份现有配置
        /// </summary>
        public bool BackupExisting { get; set; } = true;
    }
    
    /// <summary>
    /// 配置导入数据
    /// </summary>
    public class ConfigImportData
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required(ErrorMessage = "配置键不能为空")]
        [StringLength(100, ErrorMessage = "配置键长度不能超过100字符")]
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        [Required(ErrorMessage = "配置值不能为空")]
        [StringLength(2000, ErrorMessage = "配置值长度不能超过2000字符")]
        public string ConfigValue { get; set; }
        
        /// <summary>
        /// 配置名称
        /// </summary>
        [StringLength(100, ErrorMessage = "配置名称长度不能超过100字符")]
        public string ConfigName { get; set; }
        
        /// <summary>
        /// 配置描述
        /// </summary>
        [StringLength(500, ErrorMessage = "配置描述长度不能超过500字符")]
        public string Description { get; set; }
        
        /// <summary>
        /// 配置分类
        /// </summary>
        [StringLength(50, ErrorMessage = "配置分类长度不能超过50字符")]
        public string Category { get; set; }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        [StringLength(20, ErrorMessage = "数据类型长度不能超过20字符")]
        public string DataType { get; set; }
        
        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(2000, ErrorMessage = "默认值长度不能超过2000字符")]
        public string DefaultValue { get; set; }
        
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
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000, ErrorMessage = "备注长度不能超过1000字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 配置导出请求
    /// </summary>
    public class ConfigExportRequest
    {
        /// <summary>
        /// 配置ID列表（为空则导出所有配置）
        /// </summary>
        public List<int> ConfigIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 配置分类列表（为空则导出所有分类）
        /// </summary>
        public List<string> Categories { get; set; } = new List<string>();
        
        /// <summary>
        /// 导出格式
        /// </summary>
        [Required(ErrorMessage = "导出格式不能为空")]
        [RegularExpression("^(Excel|Json|Xml|Csv)$", ErrorMessage = "导出格式只能是Excel、Json、Xml或Csv")]
        public string ExportFormat { get; set; } = "Excel";
        
        /// <summary>
        /// 是否包含敏感数据
        /// </summary>
        public bool IncludeSensitiveData { get; set; } = false;
        
        /// <summary>
        /// 是否只导出启用的配置
        /// </summary>
        public bool OnlyEnabled { get; set; } = false;
        
        /// <summary>
        /// 导出范围
        /// </summary>
        [StringLength(100, ErrorMessage = "导出范围长度不能超过100字符")]
        public string ExportScope { get; set; } = "全部配置";
    }
    
    /// <summary>
    /// 配置变更历史查询请求
    /// </summary>
    public class ConfigChangeHistoryQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        public int? ConfigId { get; set; }
        
        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }
        
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }
        
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatedBy { get; set; }
    }
    
    /// <summary>
    /// 配置操作日志查询请求
    /// </summary>
    public class ConfigOperationLogQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }
        
        /// <summary>
        /// 操作对象
        /// </summary>
        public string OperationTarget { get; set; }
        
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperatedBy { get; set; }
        
        /// <summary>
        /// 操作结果
        /// </summary>
        public string OperationResult { get; set; }
        
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
    }
    
    /// <summary>
    /// 配置回滚请求
    /// </summary>
    public class ConfigRollbackRequest
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [Required(ErrorMessage = "配置ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "配置ID必须大于0")]
        public int ConfigId { get; set; }
        
        /// <summary>
        /// 目标版本号
        /// </summary>
        [Required(ErrorMessage = "目标版本号不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "目标版本号必须大于0")]
        public int TargetVersion { get; set; }
        
        /// <summary>
        /// 回滚原因
        /// </summary>
        [Required(ErrorMessage = "回滚原因不能为空")]
        [StringLength(500, ErrorMessage = "回滚原因长度不能超过500字符")]
        public string RollbackReason { get; set; }
    }
    
    /// <summary>
    /// 配置验证请求
    /// </summary>
    public class ConfigValidationRequest
    {
        /// <summary>
        /// 配置ID列表（为空则验证所有配置）
        /// </summary>
        public List<int> ConfigIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 配置分类列表（为空则验证所有分类）
        /// </summary>
        public List<string> Categories { get; set; } = new List<string>();
        
        /// <summary>
        /// 验证类型
        /// </summary>
        [Required(ErrorMessage = "验证类型不能为空")]
        [RegularExpression("^(格式验证|业务规则|依赖检查|全面验证)$", ErrorMessage = "验证类型只能是'格式验证'、'业务规则'、'依赖检查'或'全面验证'")]
        public string ValidationType { get; set; }
        
        /// <summary>
        /// 验证规则
        /// </summary>
        public List<string> ValidationRules { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否自动修复
        /// </summary>
        public bool AutoFix { get; set; } = false;
    }
    
    /// <summary>
    /// 配置缓存管理请求
    /// </summary>
    public class ConfigCacheManagementRequest
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        [Required(ErrorMessage = "操作类型不能为空")]
        [RegularExpression("^(刷新|清除|预热|统计)$", ErrorMessage = "操作类型只能是'刷新'、'清除'、'预热'或'统计'")]
        public string OperationType { get; set; }
        
        /// <summary>
        /// 配置键列表（为空则操作所有缓存）
        /// </summary>
        public List<string> ConfigKeys { get; set; } = new List<string>();
        
        /// <summary>
        /// 配置分类列表（为空则操作所有分类）
        /// </summary>
        public List<string> Categories { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 系统配置初始化请求
    /// </summary>
    public class SystemConfigInitializationRequest
    {
        /// <summary>
        /// 初始化类型
        /// </summary>
        [Required(ErrorMessage = "初始化类型不能为空")]
        [RegularExpression("^(默认配置|示例配置|完整配置)$", ErrorMessage = "初始化类型只能是'默认配置'、'示例配置'或'完整配置'")]
        public string InitializationType { get; set; }
        
        /// <summary>
        /// 是否覆盖已存在的配置
        /// </summary>
        public bool OverrideExisting { get; set; } = false;
        
        /// <summary>
        /// 是否备份现有配置
        /// </summary>
        public bool BackupExisting { get; set; } = true;
        
        /// <summary>
        /// 初始化范围
        /// </summary>
        public List<string> InitializationScope { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 系统配置重置请求
    /// </summary>
    public class SystemConfigResetRequest
    {
        /// <summary>
        /// 重置范围
        /// </summary>
        [Required(ErrorMessage = "重置范围不能为空")]
        [RegularExpression("^(全部|分类|指定配置)$", ErrorMessage = "重置范围只能是'全部'、'分类'或'指定配置'")]
        public string ResetScope { get; set; }
        
        /// <summary>
        /// 配置ID列表（重置范围为'指定配置'时必填）
        /// </summary>
        public List<int> ConfigIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 配置分类列表（重置范围为'分类'时必填）
        /// </summary>
        public List<string> Categories { get; set; } = new List<string>();
        
        /// <summary>
        /// 重置原因
        /// </summary>
        [Required(ErrorMessage = "重置原因不能为空")]
        [StringLength(500, ErrorMessage = "重置原因长度不能超过500字符")]
        public string ResetReason { get; set; }
        
        /// <summary>
        /// 是否备份现有配置
        /// </summary>
        public bool BackupExisting { get; set; } = true;
    }
}