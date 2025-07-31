using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Common.Entities;
using XTHR.Data.Context;

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
    public class SystemConfigRepository : EfCoreBaseRepository<SystemConfig, int>, ISystemConfigRepository
    {
        public SystemConfigRepository(ApplicationDbContext context) : base(context)
        {
        }
        }
        
        public async Task<SystemConfig> GetByKeyAsync(string configKey)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.ConfigKey == configKey);
        }

        public async Task<IEnumerable<SystemConfig>> GetByCategoryAsync(string category)
        {
            return await _dbSet.Where(c => c.Category == category).OrderBy(c => c.SortOrder).ThenBy(c => c.ConfigKey).ToListAsync();
        }

        public async Task<IEnumerable<SystemConfig>> GetActiveConfigsAsync()
        {
            return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.Category).ThenBy(c => c.SortOrder).ThenBy(c => c.ConfigKey).ToListAsync();
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
            string stringValue = await GetConfigValueAsync(configKey);
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
            {
                config = new SystemConfig
                {
                    ConfigKey = configKey,
                    ConfigValue = configValue,
                    UpdatedBy = updatedBy,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    CreatedBy = updatedBy,
                    IsActive = true
                };
                await AddAsync(config);
            }
            else
            {
                config.ConfigValue = configValue;
                config.UpdatedBy = updatedBy;
                config.UpdatedAt = DateTime.Now;
                await UpdateAsync(config);
            }
            return true;
        }

        public async Task<int> BatchSetConfigValuesAsync(Dictionary<string, string> configs, string updatedBy)
        {
            int updatedCount = 0;

            foreach (var entry in configs)
            {
                var config = await GetByKeyAsync(entry.Key);
                if (config != null)
                {
                    config.ConfigValue = entry.Value;
                    config.UpdatedBy = updatedBy;
                    config.UpdatedAt = DateTime.Now;
                    await UpdateAsync(config);
                    updatedCount++;
                }
                else
                {
                    var newConfig = new SystemConfig
                    {
                        ConfigKey = entry.Key,
                        ConfigValue = entry.Value,
                        UpdatedBy = updatedBy,
                        UpdatedAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                        CreatedBy = updatedBy,
                        IsActive = true
                    };
                    await AddAsync(newConfig);
                    updatedCount++;
                }
            }
            return updatedCount;
        }

        public async Task<IEnumerable<SystemConfig>> SearchConfigsAsync(string keyword, string category = null)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(c => c.ConfigKey.Contains(keyword) || c.ConfigDescription.Contains(keyword) || c.ConfigValue.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(c => c.Category == category);
            }

            return await query.OrderBy(c => c.Category).ThenBy(c => c.SortOrder).ThenBy(c => c.ConfigKey).ToListAsync();
        }

        public async Task<bool> ConfigKeyExistsAsync(string configKey, int? excludeId = null)
        {
            var query = _dbSet.Where(c => c.ConfigKey == configKey);
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.ConfigId != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        public async Task<IEnumerable<ConfigCategoryStatistics>> GetCategoryStatisticsAsync()
        {
            return await _dbSet.GroupBy(c => c.Category)
                               .Select(g => new ConfigCategoryStatistics
                               {
                                   Category = g.Key,
                                   TotalCount = g.Count(),
                                   ActiveCount = g.Count(c => c.IsActive),
                                   InactiveCount = g.Count(c => !c.IsActive),
                                   RequiredCount = g.Count(c => c.IsRequired),
                                   EncryptedCount = g.Count(c => c.IsEncrypted)
                               })
                               .OrderBy(s => s.Category)
                               .ToListAsync();
        }

        public async Task<IEnumerable<ConfigChangeHistory>> GetConfigChangeHistoryAsync(string configKey, int days = 30)
        {
            // This would typically involve a separate history table or auditing mechanism.
            // For now, returning an empty list or a placeholder.
            return new List<ConfigChangeHistory>();
        }

        public async Task<bool> ResetToDefaultAsync(string configKey, string updatedBy)
        {
            var config = await GetByKeyAsync(configKey);
            if (config == null)
                return false;

            config.ConfigValue = config.DefaultValue;
            config.UpdatedBy = updatedBy;
            config.UpdatedAt = DateTime.Now;
            await UpdateAsync(config);
            return true;
        }

        public async Task<Dictionary<string, string>> ExportConfigsAsync(string category = null)
        {
            var query = _dbSet.Where(c => c.IsActive);
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(c => c.Category == category);
            }
            return await query.OrderBy(c => c.ConfigKey).ToDictionaryAsync(c => c.ConfigKey, c => c.ConfigValue ?? string.Empty);
        }

        public async Task<int> ImportConfigsAsync(Dictionary<string, string> configs, string updatedBy, bool overwrite = false)
        {
            int importedCount = 0;

            foreach (var entry in configs)
            {
                var existingConfig = await GetByKeyAsync(entry.Key);

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