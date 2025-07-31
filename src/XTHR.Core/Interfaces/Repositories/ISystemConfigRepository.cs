using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Common;
using XTHR.Core.DTOs.Requests;
using XTHR.Core.DTOs.SystemConfig;
using XTHR.Common.Entities;
using XTHR.Common.Models;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 系统配置仓储接口
    /// </summary>
    public interface ISystemConfigRepository : IBaseRepository<XTHR.Common.Models.SystemConfig, int>
    {
        #region 配置基础查询
        
        /// <summary>
        /// 根据配置键获取配置
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>配置信息</returns>
        Task<XTHR.Common.Models.SystemConfig> GetByConfigKeyAsync(string configKey);
        
        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>配置值</returns>
        Task<string> GetConfigValueAsync(string configKey);
        
        /// <summary>
        /// 根据配置键获取配置值（泛型）
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="configKey">配置键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值</returns>
        Task<T> GetConfigValueAsync<T>(string configKey, T defaultValue = default!);
        
        /// <summary>
        /// 根据分类获取配置列表
        /// </summary>
        /// <param name="category">配置分类</param>
        /// <returns>配置列表</returns>
        Task<IEnumerable<XTHR.Common.Models.SystemConfig>> GetByCategoryAsync(string category);
        
        /// <summary>
        /// 根据数据类型获取配置列表
        /// </summary>
        /// <param name="dataType">数据类型</param>
        /// <returns>配置列表</returns>
        Task<IEnumerable<SystemConfig>> GetByDataTypeAsync(string dataType);
        
        /// <summary>
        /// 获取启用的配置列表
        /// </summary>
        /// <returns>配置列表</returns>
        Task<IEnumerable<SystemConfig>> GetEnabledConfigsAsync();
        
        /// <summary>
        /// 分页查询配置
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<SystemConfig>> GetConfigsPagedAsync(SystemConfigQueryRequest request);
        
        #endregion
        
        #region 配置值操作
        
        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> SetConfigValueAsync(string configKey, string configValue, string operatedBy);
        
        /// <summary>
        /// 设置配置值（泛型）
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> SetConfigValueAsync<T>(string configKey, T configValue, string operatedBy);
        
        /// <summary>
        /// 批量设置配置值
        /// </summary>
        /// <param name="configItems">配置项列表</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>设置成功的数量</returns>
        Task<int> BatchSetConfigValuesAsync(IEnumerable<ConfigItem> configItems, string operatedBy);
        
        /// <summary>
        /// 重置配置为默认值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> ResetConfigToDefaultAsync(string configKey, string operatedBy);
        
        #endregion
        
        #region 配置分类管理
        
        /// <summary>
        /// 获取所有配置分类
        /// </summary>
        /// <returns>分类列表</returns>
        Task<IEnumerable<ConfigCategoryDto>> GetAllCategoriesAsync();
        
        /// <summary>
        /// 创建配置分类
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <param name="description">分类描述</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateCategoryAsync(string category, string description, string operatedBy);
        
        /// <summary>
        /// 获取分类下的配置数量
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <returns>配置数量</returns>
        Task<int> GetCategoryConfigCountAsync(string category);
        
        #endregion
        
        #region 配置状态管理
        
        /// <summary>
        /// 启用配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> EnableConfigAsync(int configId, string operatedBy);
        
        /// <summary>
        /// 禁用配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> DisableConfigAsync(int configId, string operatedBy);
        
        /// <summary>
        /// 批量更新配置状态
        /// </summary>
        /// <param name="configIds">配置ID列表</param>
        /// <param name="isEnabled">新状态</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>更新的数量</returns>
        Task<int> BatchUpdateConfigStatusAsync(IEnumerable<int> configIds, bool isEnabled, string operatedBy);
        
        #endregion
        
        #region 配置历史和审计
        
        /// <summary>
        /// 获取配置变更历史
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>变更历史列表</returns>
        Task<IEnumerable<ConfigChangeHistoryDto>> GetConfigChangeHistoryAsync(int configId);
        
        /// <summary>
        /// 获取配置变更历史（分页）
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<ConfigChangeHistoryDto>> GetConfigChangeHistoryPagedAsync(ConfigChangeHistoryQueryRequest request);
        
        /// <summary>
        /// 记录配置变更
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="oldValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="changeReason">变更原因</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>变更记录ID</returns>
        Task<int> RecordConfigChangeAsync(int configId, string operationType, string oldValue, string newValue, string changeReason, string operatedBy);
        
        /// <summary>
        /// 获取配置操作日志
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<ConfigOperationLogDto>> GetConfigOperationLogsAsync(ConfigOperationLogQueryRequest request);
        
        /// <summary>
        /// 记录配置操作日志
        /// </summary>
        /// <param name="operationType">操作类型</param>
        /// <param name="operationTarget">操作对象</param>
        /// <param name="operationContent">操作内容</param>
        /// <param name="operationResult">操作结果</param>
        /// <param name="operatedBy">操作人</param>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>日志记录ID</returns>
        Task<int> RecordOperationLogAsync(string operationType, string operationTarget, string operationContent, string operationResult, string operatedBy, string ipAddress);
        
        #endregion
        
        #region 配置回滚
        
        /// <summary>
        /// 回滚配置到指定版本
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="targetVersion">目标版本</param>
        /// <param name="rollbackReason">回滚原因</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> RollbackConfigAsync(int configId, int targetVersion, string rollbackReason, string operatedBy);
        
        /// <summary>
        /// 获取配置的历史版本
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>历史版本列表</returns>
        Task<IEnumerable<ConfigVersionDto>> GetConfigVersionsAsync(int configId);
        
        #endregion
        
        #region 配置验证
        
        /// <summary>
        /// 验证配置键是否存在
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="excludeId">排除的配置ID</param>
        /// <returns>是否存在</returns>
        Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null);
        
        /// <summary>
        /// 验证配置值格式
        /// </summary>
        /// <param name="configValue">配置值</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="validationRule">验证规则</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Common.Models.ValidationResult> ValidateConfigValueAsync(string configValue, string dataType, string? validationRule = null);
        
        /// <summary>
        /// 验证配置数据
        /// </summary>
        /// <param name="config">配置对象</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Common.Models.ValidationResult> ValidateConfigDataAsync(SystemConfig config);
        
        /// <summary>
        /// 批量验证配置
        /// </summary>
        /// <param name="request">验证请求</param>
        /// <returns>验证结果</returns>
        Task<ConfigValidationResultDto> BatchValidateConfigsAsync(ConfigValidationRequest request);
        
        #endregion
        
        #region 配置导入导出
        
        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="request">导入请求</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>导入结果</returns>
        Task<ConfigImportResultDto> ImportConfigsAsync(ConfigImportRequest request, string operatedBy);
        
        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="request">导出请求</param>
        /// <returns>导出结果</returns>
        Task<ConfigExportResultDto> ExportConfigsAsync(ConfigExportRequest request);
        
        /// <summary>
        /// 备份配置
        /// </summary>
        /// <param name="backupName">备份名称</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>备份文件路径</returns>
        Task<string> BackupConfigsAsync(string backupName, string operatedBy);
        
        /// <summary>
        /// 恢复配置
        /// </summary>
        /// <param name="backupFilePath">备份文件路径</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> RestoreConfigsAsync(string backupFilePath, string operatedBy);
        
        #endregion
        
        #region 配置缓存管理
        
        /// <summary>
        /// 刷新配置缓存
        /// </summary>
        /// <param name="configKey">配置键（为空则刷新所有）</param>
        /// <returns>是否成功</returns>
        Task<bool> RefreshConfigCacheAsync(string? configKey = null);
        
        /// <summary>
        /// 清除配置缓存
        /// </summary>
        /// <param name="configKey">配置键（为空则清除所有）</param>
        /// <returns>是否成功</returns>
        Task<bool> ClearConfigCacheAsync(string? configKey = null);
        
        /// <summary>
        /// 预热配置缓存
        /// </summary>
        /// <param name="configKeys">配置键列表（为空则预热所有）</param>
        /// <returns>预热的数量</returns>
        Task<int> WarmupConfigCacheAsync(IEnumerable<string>? configKeys = null);
        
        /// <summary>
        /// 获取配置缓存统计
        /// </summary>
        /// <returns>缓存统计信息</returns>
        Task<ConfigCacheStatisticsDto> GetConfigCacheStatisticsAsync();
        
        #endregion
        
        #region 系统配置预设
        
        /// <summary>
        /// 初始化默认配置
        /// </summary>
        /// <param name="request">初始化请求</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>初始化的数量</returns>
        Task<int> InitializeDefaultConfigsAsync(SystemConfigInitializationRequest request, string operatedBy);
        
        /// <summary>
        /// 重置系统配置
        /// </summary>
        /// <param name="request">重置请求</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>重置的数量</returns>
        Task<int> ResetSystemConfigsAsync(SystemConfigResetRequest request, string operatedBy);
        
        /// <summary>
        /// 获取系统健康检查配置
        /// </summary>
        /// <returns>健康检查配置</returns>
        Task<SystemHealthCheckConfigDto> GetHealthCheckConfigAsync();
        
        /// <summary>
        /// 更新系统健康检查配置
        /// </summary>
        /// <param name="config">健康检查配置</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateHealthCheckConfigAsync(SystemHealthCheckConfigDto config, string operatedBy);
        
        #endregion
        
        #region 配置搜索
        
        /// <summary>
        /// 搜索配置
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="searchField">搜索字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<SystemConfig>> SearchConfigsAsync(string keyword, SystemConfigSearchField searchField, int pageIndex, int pageSize);
        
        /// <summary>
        /// 高级搜索配置
        /// </summary>
        /// <param name="searchCriteria">搜索条件</param>
        /// <returns>搜索结果</returns>
        Task<IEnumerable<SystemConfig>> AdvancedSearchConfigsAsync(Dictionary<string, object> searchCriteria);
        
        #endregion
        
        #region 配置依赖管理
        
        /// <summary>
        /// 获取配置依赖关系
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>依赖的配置列表</returns>
        Task<IEnumerable<string>> GetConfigDependenciesAsync(string configKey);
        
        /// <summary>
        /// 检查配置依赖
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>依赖检查结果</returns>
        Task<ConfigDependencyCheckResult> CheckConfigDependenciesAsync(string configKey);
        
        /// <summary>
        /// 获取被依赖的配置
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>被依赖的配置列表</returns>
        Task<IEnumerable<string>> GetConfigDependentsAsync(string configKey);
        
        #endregion
    }
    
    /// <summary>
    /// 配置版本DTO
    /// </summary>
    public class ConfigVersionDto
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }
        
        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }
        
        /// <summary>
        /// 变更人
        /// </summary>
        public string ChangedBy { get; set; }
        
        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }
    }
    
    /// <summary>
    /// 配置依赖检查结果
    /// </summary>
    public class ConfigDependencyCheckResult
    {
        /// <summary>
        /// 检查是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 缺失的依赖配置
        /// </summary>
        public List<string> MissingDependencies { get; set; } = new List<string>();
        
        /// <summary>
        /// 循环依赖的配置
        /// </summary>
        public List<string> CircularDependencies { get; set; } = new List<string>();
        
        /// <summary>
        /// 检查详情
        /// </summary>
        public List<string> CheckDetails { get; set; } = new List<string>();
    }
}