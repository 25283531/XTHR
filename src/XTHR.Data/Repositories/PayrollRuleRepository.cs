using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 工资规则配置仓储接口
    /// </summary>
    public interface IPayrollRuleRepository : IRepository<PayrollRule>
    {
        /// <summary>
        /// 根据规则类型获取规则
        /// </summary>
        /// <param name="ruleType">规则类型</param>
        /// <returns>规则列表</returns>
        Task<IEnumerable<PayrollRule>> GetByRuleTypeAsync(string ruleType);
        
        /// <summary>
        /// 根据规则名称获取规则
        /// </summary>
        /// <param name="ruleName">规则名称</param>
        /// <returns>规则</returns>
        Task<PayrollRule> GetByRuleNameAsync(string ruleName);
        
        /// <summary>
        /// 获取有效的规则
        /// </summary>
        /// <param name="ruleType">规则类型（可选）</param>
        /// <returns>有效规则列表</returns>
        Task<IEnumerable<PayrollRule>> GetActiveRulesAsync(string ruleType = null);
        
        /// <summary>
        /// 获取指定日期有效的规则
        /// </summary>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="ruleType">规则类型（可选）</param>
        /// <returns>有效规则列表</returns>
        Task<IEnumerable<PayrollRule>> GetRulesEffectiveOnDateAsync(DateTime effectiveDate, string ruleType = null);
        
        /// <summary>
        /// 获取规则类型统计
        /// </summary>
        /// <returns>规则类型统计列表</returns>
        Task<IEnumerable<RuleTypeStatistics>> GetRuleTypeStatisticsAsync();
        
        /// <summary>
        /// 搜索规则
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="ruleType">规则类型（可选）</param>
        /// <param name="isActive">是否有效（可选）</param>
        /// <returns>规则列表</returns>
        Task<IEnumerable<PayrollRule>> SearchRulesAsync(string keyword, string ruleType = null, bool? isActive = null);
        
        /// <summary>
        /// 检查规则名称是否存在
        /// </summary>
        /// <param name="ruleName">规则名称</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>是否存在</returns>
        Task<bool> RuleNameExistsAsync(string ruleName, int? excludeId = null);
        
        /// <summary>
        /// 批量更新规则状态
        /// </summary>
        /// <param name="ruleIds">规则ID列表</param>
        /// <param name="isActive">是否有效</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>更新的记录数</returns>
        Task<int> BatchUpdateStatusAsync(IEnumerable<int> ruleIds, bool isActive, string updatedBy);
        
        /// <summary>
        /// 获取即将过期的规则
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns>即将过期的规则列表</returns>
        Task<IEnumerable<PayrollRule>> GetExpiringRulesAsync(int days = 30);
        
        /// <summary>
        /// 获取规则变更历史
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <returns>变更历史列表</returns>
        Task<IEnumerable<RuleChangeHistory>> GetRuleChangeHistoryAsync(int ruleId);
        
        /// <summary>
        /// 复制规则
        /// </summary>
        /// <param name="sourceRuleId">源规则ID</param>
        /// <param name="newRuleName">新规则名称</param>
        /// <param name="createdBy">创建人</param>
        /// <returns>新规则ID</returns>
        Task<int> CopyRuleAsync(int sourceRuleId, string newRuleName, string createdBy);
    }
    
    /// <summary>
    /// 工资规则配置仓储实现
    /// </summary>
    public class PayrollRuleRepository : BaseRepository<PayrollRule>, IPayrollRuleRepository
    {
        public PayrollRuleRepository(IDatabaseService databaseService) 
            : base(databaseService, "PayrollRules", "RuleId")
        {
        }
        
        protected override object GetInsertParameters(PayrollRule entity)
        {
            return new
            {
                entity.RuleName,
                entity.RuleType,
                entity.RuleDescription,
                entity.RuleFormula,
                entity.Parameters,
                entity.Priority,
                entity.IsActive,
                entity.EffectiveDate,
                entity.ExpiryDate,
                entity.CreatedBy,
                CreatedAt = DateTime.Now
            };
        }
        
        protected override object GetUpdateParameters(PayrollRule entity)
        {
            return new
            {
                entity.RuleName,
                entity.RuleType,
                entity.RuleDescription,
                entity.RuleFormula,
                entity.Parameters,
                entity.Priority,
                entity.IsActive,
                entity.EffectiveDate,
                entity.ExpiryDate,
                entity.UpdatedBy,
                UpdatedAt = DateTime.Now,
                entity.RuleId
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO PayrollRules 
                (RuleName, RuleType, RuleDescription, RuleFormula, Parameters, Priority, 
                 IsActive, EffectiveDate, ExpiryDate, CreatedBy, CreatedAt)
                VALUES 
                (@RuleName, @RuleType, @RuleDescription, @RuleFormula, @Parameters, @Priority, 
                 @IsActive, @EffectiveDate, @ExpiryDate, @CreatedBy, @CreatedAt)";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE PayrollRules SET 
                    RuleName = @RuleName,
                    RuleType = @RuleType,
                    RuleDescription = @RuleDescription,
                    RuleFormula = @RuleFormula,
                    Parameters = @Parameters,
                    Priority = @Priority,
                    IsActive = @IsActive,
                    EffectiveDate = @EffectiveDate,
                    ExpiryDate = @ExpiryDate,
                    UpdatedBy = @UpdatedBy,
                    UpdatedAt = @UpdatedAt
                WHERE RuleId = @RuleId";
        }
        
        public async Task<IEnumerable<PayrollRule>> GetByRuleTypeAsync(string ruleType)
        {
            var sql = "SELECT * FROM PayrollRules WHERE RuleType = @RuleType ORDER BY Priority, RuleName";
            return await QueryAsync(sql, new { RuleType = ruleType });
        }
        
        public async Task<PayrollRule> GetByRuleNameAsync(string ruleName)
        {
            var sql = "SELECT * FROM PayrollRules WHERE RuleName = @RuleName";
            return await QuerySingleOrDefaultAsync(sql, new { RuleName = ruleName });
        }
        
        public async Task<IEnumerable<PayrollRule>> GetActiveRulesAsync(string ruleType = null)
        {
            var sql = @"
                SELECT * FROM PayrollRules 
                WHERE IsActive = 1 
                  AND (EffectiveDate IS NULL OR EffectiveDate <= GETDATE())
                  AND (ExpiryDate IS NULL OR ExpiryDate > GETDATE())";
            
            var parameters = new { RuleType = ruleType };
            
            if (!string.IsNullOrEmpty(ruleType))
            {
                sql += " AND RuleType = @RuleType";
            }
            
            sql += " ORDER BY Priority, RuleName";
            return await QueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<PayrollRule>> GetRulesEffectiveOnDateAsync(DateTime effectiveDate, string ruleType = null)
        {
            var sql = @"
                SELECT * FROM PayrollRules 
                WHERE IsActive = 1 
                  AND (EffectiveDate IS NULL OR EffectiveDate <= @EffectiveDate)
                  AND (ExpiryDate IS NULL OR ExpiryDate > @EffectiveDate)";
            
            var parameters = new { EffectiveDate = effectiveDate, RuleType = ruleType };
            
            if (!string.IsNullOrEmpty(ruleType))
            {
                sql += " AND RuleType = @RuleType";
            }
            
            sql += " ORDER BY Priority, RuleName";
            return await QueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<RuleTypeStatistics>> GetRuleTypeStatisticsAsync()
        {
            var sql = @"
                SELECT 
                    RuleType,
                    COUNT(*) as TotalCount,
                    SUM(CASE WHEN IsActive = 1 THEN 1 ELSE 0 END) as ActiveCount,
                    SUM(CASE WHEN IsActive = 0 THEN 1 ELSE 0 END) as InactiveCount,
                    MIN(CreatedAt) as EarliestCreated,
                    MAX(UpdatedAt) as LatestUpdated
                FROM PayrollRules 
                GROUP BY RuleType
                ORDER BY TotalCount DESC";
            
            var result = await QueryAsync(sql);
            return result.Select(r => new RuleTypeStatistics
            {
                RuleType = r.RuleType,
                TotalCount = r.TotalCount,
                ActiveCount = r.ActiveCount,
                InactiveCount = r.InactiveCount,
                EarliestCreated = r.EarliestCreated,
                LatestUpdated = r.LatestUpdated
            });
        }
        
        public async Task<IEnumerable<PayrollRule>> SearchRulesAsync(string keyword, string ruleType = null, bool? isActive = null)
        {
            var sql = @"
                SELECT * FROM PayrollRules 
                WHERE (RuleName LIKE @Keyword OR RuleDescription LIKE @Keyword)";
            
            var parameters = new 
            { 
                Keyword = $"%{keyword}%", 
                RuleType = ruleType, 
                IsActive = isActive 
            };
            
            if (!string.IsNullOrEmpty(ruleType))
            {
                sql += " AND RuleType = @RuleType";
            }
            
            if (isActive.HasValue)
            {
                sql += " AND IsActive = @IsActive";
            }
            
            sql += " ORDER BY Priority, RuleName";
            return await QueryAsync(sql, parameters);
        }
        
        public async Task<bool> RuleNameExistsAsync(string ruleName, int? excludeId = null)
        {
            var sql = "SELECT COUNT(1) FROM PayrollRules WHERE RuleName = @RuleName";
            var parameters = new { RuleName = ruleName, ExcludeId = excludeId };
            
            if (excludeId.HasValue)
            {
                sql += " AND RuleId != @ExcludeId";
            }
            
            var count = await QuerySingleOrDefaultAsync<int>(sql, parameters);
            return count > 0;
        }
        
        public async Task<int> BatchUpdateStatusAsync(IEnumerable<int> ruleIds, bool isActive, string updatedBy)
        {
            var sql = @"
                UPDATE PayrollRules SET 
                    IsActive = @IsActive,
                    UpdatedBy = @UpdatedBy,
                    UpdatedAt = @UpdatedAt
                WHERE RuleId IN @RuleIds";
            
            return await ExecuteAsync(sql, new 
            { 
                RuleIds = ruleIds, 
                IsActive = isActive, 
                UpdatedBy = updatedBy, 
                UpdatedAt = DateTime.Now 
            });
        }
        
        public async Task<IEnumerable<PayrollRule>> GetExpiringRulesAsync(int days = 30)
        {
            var sql = @"
                SELECT * FROM PayrollRules 
                WHERE IsActive = 1 
                  AND ExpiryDate IS NOT NULL 
                  AND ExpiryDate <= DATEADD(DAY, @Days, GETDATE())
                  AND ExpiryDate > GETDATE()
                ORDER BY ExpiryDate";
            
            return await QueryAsync(sql, new { Days = days });
        }
        
        public async Task<IEnumerable<RuleChangeHistory>> GetRuleChangeHistoryAsync(int ruleId)
        {
            // 这里假设有一个审计表记录变更历史
            var sql = @"
                SELECT 
                    ChangeId,
                    RuleId,
                    ChangeType,
                    OldValue,
                    NewValue,
                    ChangedBy,
                    ChangedAt,
                    ChangeReason
                FROM PayrollRuleChangeHistory 
                WHERE RuleId = @RuleId 
                ORDER BY ChangedAt DESC";
            
            var result = await QueryAsync(sql, new { RuleId = ruleId });
            return result.Select(r => new RuleChangeHistory
            {
                ChangeId = r.ChangeId,
                RuleId = r.RuleId,
                ChangeType = r.ChangeType,
                OldValue = r.OldValue,
                NewValue = r.NewValue,
                ChangedBy = r.ChangedBy,
                ChangedAt = r.ChangedAt,
                ChangeReason = r.ChangeReason
            });
        }
        
        public async Task<int> CopyRuleAsync(int sourceRuleId, string newRuleName, string createdBy)
        {
            var sourceRule = await GetByIdAsync(sourceRuleId);
            if (sourceRule == null)
                throw new ArgumentException("源规则不存在", nameof(sourceRuleId));
            
            var newRule = new PayrollRule
            {
                RuleName = newRuleName,
                RuleType = sourceRule.RuleType,
                RuleDescription = sourceRule.RuleDescription,
                RuleFormula = sourceRule.RuleFormula,
                Parameters = sourceRule.Parameters,
                Priority = sourceRule.Priority,
                IsActive = false, // 复制的规则默认为非活动状态
                EffectiveDate = sourceRule.EffectiveDate,
                ExpiryDate = sourceRule.ExpiryDate,
                CreatedBy = createdBy
            };
            
            return await AddAsync(newRule);
        }
    }
    
    /// <summary>
    /// 规则类型统计
    /// </summary>
    public class RuleTypeStatistics
    {
        public string RuleType { get; set; }
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public DateTime? EarliestCreated { get; set; }
        public DateTime? LatestUpdated { get; set; }
    }
    
    /// <summary>
    /// 规则变更历史
    /// </summary>
    public class RuleChangeHistory
    {
        public int ChangeId { get; set; }
        public int RuleId { get; set; }
        public string ChangeType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangeReason { get; set; }
    }
}