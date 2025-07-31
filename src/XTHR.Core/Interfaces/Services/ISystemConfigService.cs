using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Common;
using XTHR.Core.DTOs.Requests;
using XTHR.Core.DTOs.SystemConfig;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Services
{
    /// <summary>
    /// 系统配置管理服务接口
    /// </summary>
    public interface ISystemConfigService : IBaseService<SystemConfig, int, XTHR.Core.DTOs.SystemConfig.SystemConfigDetailDto, XTHR.Core.DTOs.SystemConfig.SystemConfigListDto, XTHR.Core.DTOs.SystemConfig.CreateSystemConfigRequest, XTHR.Core.DTOs.SystemConfig.UpdateSystemConfigRequest>
    {
        #region 配置基础操作
        
        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>配置值</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<string>> GetConfigValueAsync(string key);
        
        /// <summary>
        /// 根据配置键获取强类型配置值
        /// </summary>
        /// <typeparam name="T">配置值类型</typeparam>
        /// <param name="key">配置键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>强类型配置值</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<T>> GetConfigValueAsync<T>(string key, T defaultValue = default);
        
        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        /// <param name="description">配置描述</param>
        /// <returns>设置结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SetConfigValueAsync(string key, string value, string description = null);
        
        /// <summary>
        /// 设置强类型配置值
        /// </summary>
        /// <typeparam name="T">配置值类型</typeparam>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        /// <param name="description">配置描述</param>
        /// <returns>设置结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SetConfigValueAsync<T>(string key, T value, string description = null);
        
        /// <summary>
        /// 批量设置配置值
        /// </summary>
        /// <param name="configs">配置字典</param>
        /// <returns>设置结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchConfigUpdateResult>> BatchSetConfigValuesAsync(Dictionary<string, string> configs);
        
        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> DeleteConfigAsync(string key);
        
        /// <summary>
        /// 重置配置为默认值
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>重置结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ResetConfigToDefaultAsync(string key);
        
        #endregion
        
        #region 配置查询
        
        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>配置列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<XTHR.Core.DTOs.SystemConfig.SystemConfigListDto>>> GetConfigsAsync(SystemConfigQueryRequest request);
        
        /// <summary>
        /// 根据分类获取配置
        /// </summary>
        /// <param name="category">配置分类</param>
        /// <param name="includeDisabled">是否包含禁用的配置</param>
        /// <returns>配置列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<XTHR.Core.DTOs.SystemConfig.SystemConfigListDto>>> GetConfigsByCategoryAsync(string category, bool includeDisabled = false);
        
        /// <summary>
        /// 根据数据类型获取配置
        /// </summary>
        /// <param name="dataType">数据类型</param>
        /// <param name="includeDisabled">是否包含禁用的配置</param>
        /// <returns>配置列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<XTHR.Core.DTOs.SystemConfig.SystemConfigListDto>>> GetConfigsByDataTypeAsync(string dataType, bool includeDisabled = false);
        
        /// <summary>
        /// 搜索配置
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="searchInValue">是否在配置值中搜索</param>
        /// <returns>配置列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<XTHR.Core.DTOs.SystemConfig.SystemConfigListDto>>> SearchConfigsAsync(string keyword, bool searchInValue = false);
        
        /// <summary>
        /// 获取所有配置分类
        /// </summary>
        /// <returns>配置分类列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<string>>> GetConfigCategoriesAsync();
        
        /// <summary>
        /// 获取配置分类统计
        /// </summary>
        /// <returns>分类统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<ConfigCategoryStatisticsDto>>> GetConfigCategoryStatisticsAsync();
        
        #endregion
        
        #region 配置状态管理
        
        /// <summary>
        /// 启用配置
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>启用结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> EnableConfigAsync(string key);
        
        /// <summary>
        /// 禁用配置
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>禁用结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> DisableConfigAsync(string key);
        
        /// <summary>
        /// 批量更新配置状态
        /// </summary>
        /// <param name="keys">配置键列表</param>
        /// <param name="isEnabled">是否启用</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchConfigStatusUpdateResult>> BatchUpdateConfigStatusAsync(IEnumerable<string> keys, bool isEnabled);
        
        /// <summary>
        /// 检查配置是否存在
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>是否存在</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ConfigExistsAsync(string key);
        
        /// <summary>
        /// 检查配置是否启用
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>是否启用</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsConfigEnabledAsync(string key);
        
        #endregion
        
        #region 配置历史和审计
        
        /// <summary>
        /// 获取配置变更历史
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>变更历史</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<ConfigChangeHistoryDto>>> GetConfigChangeHistoryAsync(string key, int pageIndex = 1, int pageSize = 20);
        
        /// <summary>
        /// 获取配置审计日志
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>审计日志</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<ConfigAuditLogDto>>> GetConfigAuditLogsAsync(ConfigAuditLogQueryRequest request);
        
        /// <summary>
        /// 记录配置操作日志
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="operation">操作类型</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="operatorId">操作人ID</param>
        /// <param name="remark">备注</param>
        /// <returns>记录结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> LogConfigOperationAsync(string key, string operation, string oldValue, string newValue, int operatorId, string remark = null);
        
        #endregion
        
        #region 配置回滚
        
        /// <summary>
        /// 回滚配置到指定版本
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="versionId">版本ID</param>
        /// <returns>回滚结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> RollbackConfigAsync(string key, int versionId);
        
        /// <summary>
        /// 获取配置历史版本
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>历史版本列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<ConfigVersionDto>>> GetConfigVersionsAsync(string key);
        
        /// <summary>
        /// 比较配置版本
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="fromVersionId">源版本ID</param>
        /// <param name="toVersionId">目标版本ID</param>
        /// <returns>版本比较结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigVersionComparisonDto>> CompareConfigVersionsAsync(string key, int fromVersionId, int toVersionId);
        
        #endregion
        
        #region 配置验证
        
        /// <summary>
        /// 验证配置值格式
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<SystemConfigValidationResult>> ValidateConfigValueAsync(string key, string value);
        
        /// <summary>
        /// 验证配置数据完整性
        /// </summary>
        /// <param name="category">配置分类（可选）</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<SystemConfigValidationResult>> ValidateConfigDataIntegrityAsync(string category = null);
        
        /// <summary>
        /// 批量验证配置
        /// </summary>
        /// <param name="configs">配置字典</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<SystemConfigValidationResult>> BatchValidateConfigsAsync(Dictionary<string, string> configs);
        
        /// <summary>
        /// 检查配置依赖关系
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>依赖检查结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigDependencyCheckResult>> CheckConfigDependenciesAsync(string key);
        
        #endregion
        
        #region 配置导入导出
        
        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="request">导入请求</param>
        /// <returns>导入结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigImportResult>> ImportConfigsAsync(ConfigImportRequest request);
        
        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="request">导出请求</param>
        /// <returns>导出结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<XTHR.Core.DTOs.SystemConfig.SystemConfigListDto>>> ExportConfigsAsync(ConfigExportRequest request);
        
        /// <summary>
        /// 备份配置
        /// </summary>
        /// <param name="category">配置分类（可选）</param>
        /// <param name="description">备份描述</param>
        /// <returns>备份结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigBackupResult>> BackupConfigsAsync(string category = null, string description = null);
        
        /// <summary>
        /// 恢复配置
        /// </summary>
        /// <param name="backupId">备份ID</param>
        /// <param name="overwriteExisting">是否覆盖现有配置</param>
        /// <returns>恢复结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigRestoreResult>> RestoreConfigsAsync(int backupId, bool overwriteExisting = false);
        
        /// <summary>
        /// 获取配置备份列表
        /// </summary>
        /// <returns>备份列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<ConfigBackupDto>>> GetConfigBackupsAsync();
        
        #endregion
        
        #region 配置缓存管理
        
        /// <summary>
        /// 刷新配置缓存
        /// </summary>
        /// <param name="key">配置键（可选，为空则刷新所有）</param>
        /// <returns>刷新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> RefreshConfigCacheAsync(string key = null);
        
        /// <summary>
        /// 清除配置缓存
        /// </summary>
        /// <param name="key">配置键（可选，为空则清除所有）</param>
        /// <returns>清除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ClearConfigCacheAsync(string key = null);
        
        /// <summary>
        /// 预热配置缓存
        /// </summary>
        /// <param name="category">配置分类（可选）</param>
        /// <returns>预热结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigCacheWarmupResult>> WarmupConfigCacheAsync(string category = null);
        
        /// <summary>
        /// 获取配置缓存统计
        /// </summary>
        /// <returns>缓存统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigCacheStatisticsDto>> GetConfigCacheStatisticsAsync();
        
        /// <summary>
        /// 获取配置缓存状态
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>缓存状态</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigCacheStatusDto>> GetConfigCacheStatusAsync(string key);
        
        #endregion
        
        #region 系统配置预设
        
        /// <summary>
        /// 初始化系统默认配置
        /// </summary>
        /// <param name="overwriteExisting">是否覆盖现有配置</param>
        /// <returns>初始化结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<SystemConfigInitializationResult>> InitializeSystemConfigsAsync(bool overwriteExisting = false);
        
        /// <summary>
        /// 重置系统配置为默认值
        /// </summary>
        /// <param name="category">配置分类（可选）</param>
        /// <returns>重置结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<SystemConfigResetResult>> ResetSystemConfigsAsync(string category = null);
        
        /// <summary>
        /// 检查系统配置健康状态
        /// </summary>
        /// <returns>健康检查结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<SystemConfigHealthCheckResult>> CheckSystemConfigHealthAsync();
        
        /// <summary>
        /// 获取系统配置模板
        /// </summary>
        /// <param name="templateType">模板类型</param>
        /// <returns>配置模板</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<SystemConfigTemplateDto>>> GetSystemConfigTemplatesAsync(string templateType = null);
        
        /// <summary>
        /// 应用配置模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="overwriteExisting">是否覆盖现有配置</param>
        /// <returns>应用结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigTemplateApplicationResult>> ApplyConfigTemplateAsync(int templateId, bool overwriteExisting = false);
        
        #endregion
        
        #region 配置监控和通知
        
        /// <summary>
        /// 监控配置变更
        /// </summary>
        /// <param name="keys">要监控的配置键列表</param>
        /// <param name="callback">变更回调</param>
        /// <returns>监控结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> MonitorConfigChangesAsync(IEnumerable<string> keys, Action<string, string, string> callback);
        
        /// <summary>
        /// 停止监控配置变更
        /// </summary>
        /// <param name="keys">要停止监控的配置键列表</param>
        /// <returns>停止结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> StopMonitoringConfigChangesAsync(IEnumerable<string> keys);
        
        /// <summary>
        /// 获取配置变更通知设置
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns>通知设置</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ConfigChangeNotificationSettingDto>> GetConfigChangeNotificationSettingAsync(string key);
        
        /// <summary>
        /// 设置配置变更通知
        /// </summary>
        /// <param name="request">通知设置请求</param>
        /// <returns>设置结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SetConfigChangeNotificationAsync(ConfigChangeNotificationRequest request);
        
        #endregion
    }
    
    /// <summary>
    /// 批量配置更新结果
    /// </summary>
    public class BatchConfigUpdateResult
    {
        /// <summary>
        /// 更新是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 成功更新数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败更新数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 更新的配置
        /// </summary>
        public List<string> UpdatedConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置
        /// </summary>
        public List<BatchConfigUpdateFailure> Failures { get; set; } = new List<BatchConfigUpdateFailure>();
    }
    
    /// <summary>
    /// 批量配置更新失败记录
    /// </summary>
    public class BatchConfigUpdateFailure
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 批量配置状态更新结果
    /// </summary>
    public class BatchConfigStatusUpdateResult
    {
        /// <summary>
        /// 更新是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 成功更新数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败更新数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 更新的配置键
        /// </summary>
        public List<string> UpdatedKeys { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置键
        /// </summary>
        public List<string> FailedKeys { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 系统配置验证结果
    /// </summary>
    public class SystemConfigValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 验证错误
        /// </summary>
        public List<ConfigValidationError> Errors { get; set; } = new List<ConfigValidationError>();
        
        /// <summary>
        /// 验证警告
        /// </summary>
        public List<ConfigValidationWarning> Warnings { get; set; } = new List<ConfigValidationWarning>();
    }
    
    /// <summary>
    /// 配置验证错误
    /// </summary>
    public class ConfigValidationError
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }
    }
    
    /// <summary>
    /// 配置验证警告
    /// </summary>
    public class ConfigValidationWarning
    {
        /// <summary>
        /// 警告类型
        /// </summary>
        public string WarningType { get; set; }
        
        /// <summary>
        /// 警告信息
        /// </summary>
        public string WarningMessage { get; set; }
        
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
    }
    
    /// <summary>
    /// 配置数据完整性验证结果
    /// </summary>
    public class ConfigDataIntegrityResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 有效配置数
        /// </summary>
        public int ValidConfigs { get; set; }
        
        /// <summary>
        /// 无效配置数
        /// </summary>
        public int InvalidConfigs { get; set; }
        
        /// <summary>
        /// 缺失的必需配置
        /// </summary>
        public List<string> MissingRequiredConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 无效的配置详情
        /// </summary>
        public List<ConfigValidationError> InvalidConfigDetails { get; set; } = new List<ConfigValidationError>();
        
        /// <summary>
        /// 孤立的配置（无分类）
        /// </summary>
        public List<string> OrphanedConfigs { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 批量配置验证结果
    /// </summary>
    public class BatchConfigValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 有效配置数
        /// </summary>
        public int ValidConfigs { get; set; }
        
        /// <summary>
        /// 无效配置数
        /// </summary>
        public int InvalidConfigs { get; set; }
        
        /// <summary>
        /// 验证结果详情
        /// </summary>
        public Dictionary<string, SystemConfigValidationResult> ValidationResults { get; set; } = new Dictionary<string, SystemConfigValidationResult>();
    }
    
    /// <summary>
    /// 配置导入结果
    /// </summary>
    public class ConfigImportResult
    {
        /// <summary>
        /// 导入是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 成功导入数
        /// </summary>
        public int ImportedCount { get; set; }
        
        /// <summary>
        /// 更新数
        /// </summary>
        public int UpdatedCount { get; set; }
        
        /// <summary>
        /// 新增数
        /// </summary>
        public int CreatedCount { get; set; }
        
        /// <summary>
        /// 跳过数
        /// </summary>
        public int SkippedCount { get; set; }
        
        /// <summary>
        /// 失败数
        /// </summary>
        public int FailedCount { get; set; }
        
        /// <summary>
        /// 导入的配置
        /// </summary>
        public List<string> ImportedConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置
        /// </summary>
        public List<ConfigImportFailure> Failures { get; set; } = new List<ConfigImportFailure>();
    }
    
    /// <summary>
    /// 配置导入失败记录
    /// </summary>
    public class ConfigImportFailure
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex { get; set; }
    }
    
    /// <summary>
    /// 配置备份结果
    /// </summary>
    public class ConfigBackupResult
    {
        /// <summary>
        /// 备份是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 备份ID
        /// </summary>
        public int BackupId { get; set; }
        
        /// <summary>
        /// 备份文件路径
        /// </summary>
        public string BackupFilePath { get; set; }
        
        /// <summary>
        /// 备份配置数
        /// </summary>
        public int BackupConfigCount { get; set; }
        
        /// <summary>
        /// 备份时间
        /// </summary>
        public DateTime BackupTime { get; set; }
        
        /// <summary>
        /// 备份大小（字节）
        /// </summary>
        public long BackupSize { get; set; }
    }
    
    /// <summary>
    /// 配置恢复结果
    /// </summary>
    public class ConfigRestoreResult
    {
        /// <summary>
        /// 恢复是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 恢复配置数
        /// </summary>
        public int RestoredCount { get; set; }
        
        /// <summary>
        /// 跳过配置数
        /// </summary>
        public int SkippedCount { get; set; }
        
        /// <summary>
        /// 失败配置数
        /// </summary>
        public int FailedCount { get; set; }
        
        /// <summary>
        /// 恢复的配置
        /// </summary>
        public List<string> RestoredConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置
        /// </summary>
        public List<ConfigRestoreFailure> Failures { get; set; } = new List<ConfigRestoreFailure>();
    }
    
    /// <summary>
    /// 配置恢复失败记录
    /// </summary>
    public class ConfigRestoreFailure
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 配置缓存预热结果
    /// </summary>
    public class ConfigCacheWarmupResult
    {
        /// <summary>
        /// 预热是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 预热配置数
        /// </summary>
        public int WarmedUpCount { get; set; }
        
        /// <summary>
        /// 预热耗时（毫秒）
        /// </summary>
        public long ElapsedMilliseconds { get; set; }
        
        /// <summary>
        /// 预热的配置键
        /// </summary>
        public List<string> WarmedUpKeys { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 系统配置初始化结果
    /// </summary>
    public class SystemConfigInitializationResult
    {
        /// <summary>
        /// 初始化是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 初始化配置数
        /// </summary>
        public int InitializedCount { get; set; }
        
        /// <summary>
        /// 跳过配置数
        /// </summary>
        public int SkippedCount { get; set; }
        
        /// <summary>
        /// 失败配置数
        /// </summary>
        public int FailedCount { get; set; }
        
        /// <summary>
        /// 初始化的配置
        /// </summary>
        public List<string> InitializedConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置
        /// </summary>
        public List<string> FailedConfigs { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 系统配置重置结果
    /// </summary>
    public class SystemConfigResetResult
    {
        /// <summary>
        /// 重置是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 重置配置数
        /// </summary>
        public int ResetCount { get; set; }
        
        /// <summary>
        /// 失败配置数
        /// </summary>
        public int FailedCount { get; set; }
        
        /// <summary>
        /// 重置的配置
        /// </summary>
        public List<string> ResetConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置
        /// </summary>
        public List<string> FailedConfigs { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 系统配置健康检查结果
    /// </summary>
    public class SystemConfigHealthCheckResult
    {
        /// <summary>
        /// 健康状态
        /// </summary>
        public ConfigHealthStatus Status { get; set; }
        
        /// <summary>
        /// 总配置数
        /// </summary>
        public int TotalConfigs { get; set; }
        
        /// <summary>
        /// 健康配置数
        /// </summary>
        public int HealthyConfigs { get; set; }
        
        /// <summary>
        /// 不健康配置数
        /// </summary>
        public int UnhealthyConfigs { get; set; }
        
        /// <summary>
        /// 缺失的必需配置
        /// </summary>
        public List<string> MissingRequiredConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 无效的配置
        /// </summary>
        public List<ConfigValidationError> InvalidConfigs { get; set; } = new List<ConfigValidationError>();
        
        /// <summary>
        /// 健康检查详情
        /// </summary>
        public List<ConfigHealthCheckDetail> Details { get; set; } = new List<ConfigHealthCheckDetail>();
    }
    
    /// <summary>
    /// 配置健康状态
    /// </summary>
    public enum ConfigHealthStatus
    {
        /// <summary>
        /// 健康
        /// </summary>
        Healthy,
        
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        
        /// <summary>
        /// 不健康
        /// </summary>
        Unhealthy,
        
        /// <summary>
        /// 严重
        /// </summary>
        Critical
    }
    
    /// <summary>
    /// 配置健康检查详情
    /// </summary>
    public class ConfigHealthCheckDetail
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 健康状态
        /// </summary>
        public ConfigHealthStatus Status { get; set; }
        
        /// <summary>
        /// 检查项
        /// </summary>
        public string CheckItem { get; set; }
        
        /// <summary>
        /// 检查结果
        /// </summary>
        public string CheckResult { get; set; }
        
        /// <summary>
        /// 建议
        /// </summary>
        public string Recommendation { get; set; }
    }
    
    /// <summary>
    /// 配置模板应用结果
    /// </summary>
    public class ConfigTemplateApplicationResult
    {
        /// <summary>
        /// 应用是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 应用配置数
        /// </summary>
        public int AppliedCount { get; set; }
        
        /// <summary>
        /// 跳过配置数
        /// </summary>
        public int SkippedCount { get; set; }
        
        /// <summary>
        /// 失败配置数
        /// </summary>
        public int FailedCount { get; set; }
        
        /// <summary>
        /// 应用的配置
        /// </summary>
        public List<string> AppliedConfigs { get; set; } = new List<string>();
        
        /// <summary>
        /// 失败的配置
        /// </summary>
        public List<string> FailedConfigs { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 配置版本比较结果
    /// </summary>
    public class ConfigVersionComparisonDto
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 源版本
        /// </summary>
        public ConfigVersionDto FromVersion { get; set; }
        
        /// <summary>
        /// 目标版本
        /// </summary>
        public ConfigVersionDto ToVersion { get; set; }
        
        /// <summary>
        /// 是否有变更
        /// </summary>
        public bool HasChanges { get; set; }
        
        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; }
        
        /// <summary>
        /// 变更描述
        /// </summary>
        public string ChangeDescription { get; set; }
    }
}