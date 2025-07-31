using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Models;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Requests;
using XTHR.Core.DTOs.Responses;
using XTHR.Core.DTOs.SystemConfig;

namespace XTHR.Core.Interfaces
{
    /// <summary>
    /// 系统配置服务接口
    /// 提供系统配置管理的业务逻辑
    /// </summary>
    public interface ISystemConfigService
    {
        #region 配置管理
        
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>配置值</returns>
        Task<string> GetConfigValueAsync(string configKey);
        
        /// <summary>
        /// 获取配置值（泛型）
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="configKey">配置键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值</returns>
        Task<T> GetConfigValueAsync<T>(string configKey, T defaultValue = default!);
        
        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="description">描述</param>
        /// <returns>设置结果</returns>
        Task<ServiceResult> SetConfigValueAsync(string configKey, string configValue, string? description = null);
        
        /// <summary>
        /// 设置配置值（泛型）
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="description">描述</param>
        /// <returns>设置结果</returns>
        Task<ServiceResult> SetConfigValueAsync<T>(string configKey, T configValue, string? description = null);
        
        /// <summary>
        /// 获取配置详情
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>配置详情</returns>
        Task<XTHR.Core.DTOs.SystemConfig.SystemConfigDetailDto> GetConfigDetailAsync(int configId);
        
        /// <summary>
        /// 根据配置键获取配置详情
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>配置详情</returns>
        Task<XTHR.Core.DTOs.SystemConfig.SystemConfigDetailDto> GetConfigDetailByKeyAsync(string configKey);
        
        #endregion
        
        #region 配置列表管理
        
        /// <summary>
        /// 获取配置列表（分页）
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页配置列表</returns>
        Task<PagedResult<XTHR.Core.DTOs.SystemConfig.SystemConfigListDto>> GetConfigListAsync(SystemConfigQueryRequest request);
        
        /// <summary>
        /// 根据分类获取配置列表
        /// </summary>
        /// <param name="category">配置分类</param>
        /// <param name="includeInactive">是否包含非活动配置</param>
        /// <returns>配置列表</returns>
        Task<List<SystemConfigDto>> GetConfigsByCategoryAsync(string category, bool includeInactive = false);
        
        /// <summary>
        /// 获取所有有效配置
        /// </summary>
        /// <returns>有效配置列表</returns>
        Task<List<SystemConfigDto>> GetActiveConfigsAsync();
        
        /// <summary>
        /// 搜索配置
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="searchFields">搜索字段</param>
        /// <returns>搜索结果</returns>
        Task<List<SystemConfigDto>> SearchConfigsAsync(string keyword, ConfigSearchFields searchFields = ConfigSearchFields.All);
        
        #endregion
        
        #region 配置CRUD操作
        
        /// <summary>
        /// 创建配置
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<ServiceResult<int>> CreateConfigAsync(XTHR.Core.DTOs.SystemConfig.CreateSystemConfigRequest request);
        
        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<ServiceResult> UpdateConfigAsync(int configId, XTHR.Core.DTOs.SystemConfig.UpdateSystemConfigRequest request);
        
        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <returns>删除结果</returns>
        Task<ServiceResult> DeleteConfigAsync(int configId);
        
        /// <summary>
        /// 启用/禁用配置
        /// </summary>
        /// <param name="configId">配置ID</param>
        /// <param name="isActive">是否启用</param>
        /// <returns>操作结果</returns>
        Task<ServiceResult> ToggleConfigStatusAsync(int configId, bool isActive);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量设置配置值
        /// </summary>
        /// <param name="configs">配置键值对</param>
        /// <returns>设置结果</returns>
        Task<ServiceResult> BatchSetConfigValuesAsync(Dictionary<string, string> configs);
        
        /// <summary>
        /// 批量更新配置状态
        /// </summary>
        /// <param name="configIds">配置ID列表</param>
        /// <param name="isActive">是否启用</param>
        /// <returns>更新结果</returns>
        Task<ServiceResult> BatchUpdateConfigStatusAsync(List<int> configIds, bool isActive);
        
        /// <summary>
        /// 批量导入配置
        /// </summary>
        /// <param name="configs">配置列表</param>
        /// <returns>导入结果</returns>
        Task<BatchImportResult<SystemConfigImportDto>> BatchImportConfigsAsync(List<SystemConfigImportDto> configs);
        
        #endregion
        
        #region 配置分类管理
        
        /// <summary>
        /// 获取配置分类列表
        /// </summary>
        /// <returns>分类列表</returns>
        Task<List<ConfigCategoryDto>> GetConfigCategoriesAsync();
        
        /// <summary>
        /// 获取配置分类统计
        /// </summary>
        /// <returns>分类统计</returns>
        Task<List<ConfigCategoryStatisticsDto>> GetConfigCategoryStatisticsAsync();
        
        /// <summary>
        /// 创建配置分类
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <param name="description">分类描述</param>
        /// <returns>创建结果</returns>
        Task<ServiceResult> CreateConfigCategoryAsync(string category, string? description = null);
        
        #endregion
        
        #region 配置历史和审计
        
        /// <summary>
        /// 获取配置变更历史
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>变更历史</returns>
        Task<List<ConfigChangeHistoryDto>> GetConfigChangeHistoryAsync(string configKey, DateTime? startDate = null, DateTime? endDate = null);
        
        /// <summary>
        /// 获取配置操作日志
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>操作日志</returns>
        Task<PagedResult<ConfigAuditLogDto>> GetConfigAuditLogsAsync(ConfigAuditLogQueryRequest request);
        
        /// <summary>
        /// 回滚配置到指定版本
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="targetVersion">目标版本</param>
        /// <returns>回滚结果</returns>
        Task<ServiceResult> RollbackConfigAsync(string configKey, int targetVersion);
        
        #endregion
        
        #region 配置验证
        
        /// <summary>
        /// 验证配置键是否存在
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="excludeConfigId">排除的配置ID</param>
        /// <returns>是否存在</returns>
        Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeConfigId = null);
        
        /// <summary>
        /// 验证配置值格式
        /// </summary>
        /// <param name="configValue">配置值</param>
        /// <param name="dataType">数据类型</param>
        /// <returns>验证结果</returns>
        Task<SystemConfigValidationResult> ValidateConfigValueAsync(string configValue, string dataType);

        /// <summary>
        /// 验证配置数据
        /// </summary>
        /// <param name="config">配置数据</param>
        /// <returns>验证结果</returns>
        Task<SystemConfigValidationResult> ValidateConfigAsync(SystemConfig config);
        
        #endregion
        
        #region 配置导入导出
        
        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="categories">导出的分类（可选）</param>
        /// <param name="includeInactive">是否包含非活动配置</param>
        /// <returns>导出数据</returns>
        Task<XTHR.Core.DTOs.SystemConfig.ConfigExportDto> ExportConfigsAsync(List<string>? categories = null, bool includeInactive = false);
        
        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="importData">导入数据</param>
        /// <param name="overwriteExisting">是否覆盖已存在的配置</param>
        /// <returns>导入结果</returns>
        Task<XTHR.Core.DTOs.SystemConfig.ConfigImportResult> ImportConfigsAsync(XTHR.Core.DTOs.SystemConfig.ConfigImportDto importData, bool overwriteExisting = false);
        
        #endregion
        
        #region 配置缓存管理
        
        /// <summary>
        /// 刷新配置缓存
        /// </summary>
        /// <param name="configKey">配置键（可选，为空则刷新全部）</param>
        /// <returns>刷新结果</returns>
        Task<ServiceResult> RefreshConfigCacheAsync(string? configKey = null);
        
        /// <summary>
        /// 预热配置缓存
        /// </summary>
        /// <returns>预热结果</returns>
        Task<ServiceResult> WarmupConfigCacheAsync();
        
        /// <summary>
        /// 获取缓存统计信息
        /// </summary>
        /// <returns>缓存统计</returns>
        Task<ConfigCacheStatisticsDto> GetConfigCacheStatisticsAsync();
        
        #endregion
        
        #region 系统配置预设
        
        /// <summary>
        /// 初始化默认配置
        /// </summary>
        /// <returns>初始化结果</returns>
        Task<ServiceResult> InitializeDefaultConfigsAsync();
        
        /// <summary>
        /// 重置配置为默认值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>重置结果</returns>
        Task<ServiceResult> ResetConfigToDefaultAsync(string configKey);
        
        /// <summary>
        /// 获取系统健康检查配置
        /// </summary>
        /// <returns>健康检查配置</returns>
        Task<SystemHealthCheckConfigDto> GetHealthCheckConfigAsync();
        
        #endregion
    }
    
    /// <summary>
    /// 配置搜索字段枚举
    /// </summary>
    [Flags]
    public enum ConfigSearchFields
    {
        ConfigKey = 1,
        ConfigValue = 2,
        Description = 4,
        Category = 8,
        All = ConfigKey | ConfigValue | Description | Category
    }
}