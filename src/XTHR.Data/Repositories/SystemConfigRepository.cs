using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 系统配置仓储接口
    /// </summary>
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {
        /// <summary>
        /// 根据配置键获取配置
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns>配置</returns>
        Task<SystemConfig> GetByKeyAsync(string configKey);
        
        /// <summary>
        /// 根据配置分类获取配置列表
        /// </summary>
        /// <param name="category">配置分类</param>
        /// <returns>配置列表</returns>
        Task<IEnumerable<SystemConfig>> GetByCategoryAsync(string category);
        
        /// <summary>
        /// 获取所有有效配置
        /// </summary>
        /// <returns>有效配置列表</returns>
        Task<IEnumerable<SystemConfig>> GetActiveConfigsAsync();
        
        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值</returns>
        Task<string> GetConfigValueAsync(string configKey, string defaultValue = null);
        
        /// <summary>
        /// 根据配置键获取配置值（泛型）
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="configKey">配置键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值</returns>
        Task<T> GetConfigValueAsync<T>(string configKey, T defaultValue = default(T));
        
        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>是否成功</returns>
        Task<bool> SetConfigValueAsync(string configKey, string configValue, string updatedBy);
        
        /// <summary>
        /// 批量设置配置值
        /// </summary>
        /// <param name="configs">配置字典</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>更新的配置数量</returns>
        Task<int> BatchSetConfigValuesAsync(Dictionary<string, string> configs, string updatedBy);
        
        /// <summary>
        /// 搜索配置
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="category">配置分类（可选）</param>
        /// <returns>配置列表</returns>
        Task<IEnumerable<SystemConfig>> SearchConfigsAsync(string keyword, string category = null);
        
        /// <summary>
        /// 检查配置键是否存在
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>是否存在</returns>
        Task<bool> ConfigKeyExistsAsync(string configKey, int? excludeId = null);
        
        /// <summary>
        /// 获取配置分类统计
        /// </summary>
        /// <returns>配置分类统计列表</returns>
        Task<IEnumerable<ConfigCategoryStatistics>> GetCategoryStatisticsAsync();
        
        /// <summary>
        /// 获取配置变更历史
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="days">天数</param>
        /// <returns>变更历史列表</returns>
        Task<IEnumerable<ConfigChangeHistory>> GetConfigChangeHistoryAsync(string configKey, int days = 30);
        
        /// <summary>
        /// 重置配置为默认值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>是否成功</returns>
        Task<bool> ResetToDefaultAsync(string configKey, string updatedBy);
        
        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="category">配置分类（可选）</param>
        /// <returns>配置字典</returns>
        Task<Dictionary<string, string>> ExportConfigsAsync(string category = null);
        
        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="configs">配置字典</param>
        /// <param name="updatedBy">更新人</param>
        /// <param name="overwrite">是否覆盖现有配置</param>
        /// <returns>导入的配置数量</returns>
        Task<int> ImportConfigsAsync(Dictionary<string, string> configs, string updatedBy, bool overwrite = false);
    }
    
    /// <summary>
    /// 系统配置仓储实现
    /// </summary>
    public class SystemConfigRepository : BaseRepository<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDatabaseService databaseService) 
            : base(databaseService, "SystemConfigs", "ConfigId")
        {
        }
        
        protected override object GetInsertParameters(SystemConfig entity)
        {
            return new
            {
                entity.ConfigKey,
                entity.ConfigValue,
                entity.ConfigDescription,
                entity.Category,
                entity.DataType,
                entity.DefaultValue,
                entity.IsRequired,
                entity.IsEncrypted,
                entity.IsActive,
                entity.SortOrder,
                entity.CreatedBy,
                CreatedAt = DateTime.Now
            };
        }
        
        protected override object GetUpdateParameters(SystemConfig entity)
        {
            return new
            {
                entity.ConfigKey,
                entity.ConfigValue,
                entity.ConfigDescription,
                entity.Category,
                entity.DataType,
                entity.DefaultValue,
                entity.IsRequired,
                entity.IsEncrypted,
                entity.IsActive,
                entity.SortOrder,
                entity.UpdatedBy,
                UpdatedAt = DateTime.Now,
                entity.ConfigId
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO SystemConfigs 
                (ConfigKey, ConfigValue, ConfigDescription, Category, DataType, DefaultValue, 
                 IsRequired, IsEncrypted, IsActive, SortOrder, CreatedBy, CreatedAt)
                VALUES 
                (@ConfigKey, @ConfigValue, @ConfigDescription, @Category, @DataType, @DefaultValue, 
                 @IsRequired, @IsEncrypted, @IsActive, @SortOrder, @CreatedBy, @CreatedAt)";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE SystemConfigs SET 
                    ConfigKey = @ConfigKey,
                    ConfigValue = @ConfigValue,
                    ConfigDescription = @ConfigDescription,
                    Category = @Category,
                    DataType = @DataType,
                    DefaultValue = @DefaultValue,
                    IsRequired = @IsRequired,
                    IsEncrypted = @IsEncrypted,
                    IsActive = @IsActive,
                    SortOrder = @SortOrder,
                    UpdatedBy = @UpdatedBy,
                    UpdatedAt = @UpdatedAt
                WHERE ConfigId = @ConfigId";
        }
        
        public async Task<SystemConfig> GetByKeyAsync(string configKey)
        {
            var sql = "SELECT * FROM SystemConfigs WHERE ConfigKey = @ConfigKey";
            return await QuerySingleOrDefaultAsync(sql, new { ConfigKey = configKey });
        }
        
        public async Task<IEnumerable<SystemConfig>> GetByCategoryAsync(string category)
        {
            var sql = "SELECT * FROM SystemConfigs WHERE Category = @Category ORDER BY SortOrder, ConfigKey";
            return await QueryAsync(sql, new { Category = category });
        }
        
        public async Task<IEnumerable<SystemConfig>> GetActiveConfigsAsync()
        {
            var sql = "SELECT * FROM SystemConfigs WHERE IsActive = 1 ORDER BY Category, SortOrder, ConfigKey";
            return await QueryAsync(sql);
        }
        
        public async Task<string> GetConfigValueAsync(string configKey, string defaultValue = null)
        {
            var config = await GetByKeyAsync(configKey);
            if (config != null && config.IsActive)
            {
                return !string.IsNullOrEmpty(config.ConfigValue) ? config.ConfigValue : config.DefaultValue;
            }
            return defaultValue;
        }
        
        public async Task<T> GetConfigValueAsync<T>(string configKey, T defaultValue = default(T))
        {
            var stringValue = await GetConfigValueAsync(configKey);
            if (string.IsNullOrEmpty(stringValue))
                return defaultValue;
            
            try
            {
                return (T)Convert.ChangeType(stringValue, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
        
        public async Task<bool> SetConfigValueAsync(string configKey, string configValue, string updatedBy)
        {
            var config = await GetByKeyAsync(configKey);
            if (config == null)
                return false;
            
            config.ConfigValue = configValue;
            config.UpdatedBy = updatedBy;
            
            await UpdateAsync(config);
            return true;
        }
        
        public async Task<int> BatchSetConfigValuesAsync(Dictionary<string, string> configs, string updatedBy)
        {
            int updatedCount = 0;
            
            foreach (var kvp in configs)
            {
                var success = await SetConfigValueAsync(kvp.Key, kvp.Value, updatedBy);
                if (success)
                    updatedCount++;
            }
            
            return updatedCount;
        }
        
        public async Task<IEnumerable<SystemConfig>> SearchConfigsAsync(string keyword, string category = null)
        {
            var sql = @"
                SELECT * FROM SystemConfigs 
                WHERE (ConfigKey LIKE @Keyword OR ConfigDescription LIKE @Keyword)";
            
            var parameters = new { Keyword = $"%{keyword}%", Category = category };
            
            if (!string.IsNullOrEmpty(category))
            {
                sql += " AND Category = @Category";
            }
            
            sql += " ORDER BY Category, SortOrder, ConfigKey";
            return await QueryAsync(sql, parameters);
        }
        
        public async Task<bool> ConfigKeyExistsAsync(string configKey, int? excludeId = null)
        {
            var sql = "SELECT COUNT(1) FROM SystemConfigs WHERE ConfigKey = @ConfigKey";
            var parameters = new { ConfigKey = configKey, ExcludeId = excludeId };
            
            if (excludeId.HasValue)
            {
                sql += " AND ConfigId != @ExcludeId";
            }
            
            var count = await QuerySingleOrDefaultAsync<int>(sql, parameters);
            return count > 0;
        }
        
        public async Task<IEnumerable<ConfigCategoryStatistics>> GetCategoryStatisticsAsync()
        {
            var sql = @"
                SELECT 
                    Category,
                    COUNT(*) as TotalCount,
                    SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) as ActiveCount,
                    SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END) as InactiveCount,
                    SUM(CASE WHEN IsRequired = 1 THEN 1 ELSE 0 END) as RequiredCount,
                    SUM(CASE WHEN IsEncrypted = 1 THEN 1 ELSE 0 END) as EncryptedCount
                FROM SystemConfigs 
                GROUP BY Category
                ORDER BY Category";
            
            var result = await QueryAsync(sql);
            return result.Select(r => new ConfigCategoryStatistics
            {
                Category = r.Category,
                TotalCount = r.TotalCount,
                ActiveCount = r.ActiveCount,
                InactiveCount = r.InactiveCount,
                RequiredCount = r.RequiredCount,
                EncryptedCount = r.EncryptedCount
            });
        }
        
        public async Task<IEnumerable<ConfigChangeHistory>> GetConfigChangeHistoryAsync(string configKey, int days = 30)
        {
            // 这里假设有一个审计表记录配置变更历史
            var sql = @"
                SELECT 
                    ChangeId,
                    ConfigKey,
                    OldValue,
                    NewValue,
                    ChangedBy,
                    ChangedAt,
                    ChangeReason
                FROM SystemConfigChangeHistory 
                WHERE ConfigKey = @ConfigKey 
                  AND ChangedAt >= DATEADD(DAY, -@Days, GETDATE())
                ORDER BY ChangedAt DESC";
            
            var result = await QueryAsync(sql, new { ConfigKey = configKey, Days = days });
            return result.Select(r => new ConfigChangeHistory
            {
                ChangeId = r.ChangeId,
                ConfigKey = r.ConfigKey,
                OldValue = r.OldValue,
                NewValue = r.NewValue,
                ChangedBy = r.ChangedBy,
                ChangedAt = r.ChangedAt,
                ChangeReason = r.ChangeReason
            });
        }
        
        public async Task<bool> ResetToDefaultAsync(string configKey, string updatedBy)
        {
            var config = await GetByKeyAsync(configKey);
            if (config == null)
                return false;
            
            config.ConfigValue = config.DefaultValue;
            config.UpdatedBy = updatedBy;
            
            await UpdateAsync(config);
            return true;
        }
        
        public async Task<Dictionary<string, string>> ExportConfigsAsync(string category = null)
        {
            var sql = "SELECT ConfigKey, ConfigValue FROM SystemConfigs WHERE IsActive = 1";
            var parameters = new { Category = category };
            
            if (!string.IsNullOrEmpty(category))
            {
                sql += " AND Category = @Category";
            }
            
            sql += " ORDER BY ConfigKey";
            
            var result = await QueryAsync(sql, parameters);
            return result.ToDictionary(r => r.ConfigKey, r => r.ConfigValue ?? string.Empty);
        }
        
        public async Task<int> ImportConfigsAsync(Dictionary<string, string> configs, string updatedBy, bool overwrite = false)
        {
            int importedCount = 0;
            
            foreach (var kvp in configs)
            {
                var existingConfig = await GetByKeyAsync(kvp.Key);
                
                if (existingConfig != null)
                {
                    if (overwrite)
                    {
                        existingConfig.ConfigValue = kvp.Value;
                        existingConfig.UpdatedBy = updatedBy;
                        await UpdateAsync(existingConfig);
                        importedCount++;
                    }
                }
                else
                {
                    // 如果配置不存在，创建新配置（需要更多信息，这里简化处理）
                    var newConfig = new SystemConfig
                    {
                        ConfigKey = kvp.Key,
                        ConfigValue = kvp.Value,
                        ConfigDescription = $"导入的配置: {kvp.Key}",
                        Category = "导入",
                        DataType = "string",
                        IsActive = true,
                        CreatedBy = updatedBy
                    };
                    
                    await AddAsync(newConfig);
                    importedCount++;
                }
            }
            
            return importedCount;
        }
    }
    
    /// <summary>
    /// 配置分类统计
    /// </summary>
    public class ConfigCategoryStatistics
    {
        public string Category { get; set; }
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int RequiredCount { get; set; }
        public int EncryptedCount { get; set; }
    }
    
    /// <summary>
    /// 配置变更历史
    /// </summary>
    public class ConfigChangeHistory
    {
        public int ChangeId { get; set; }
        public string ConfigKey { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangeReason { get; set; }
    }
}