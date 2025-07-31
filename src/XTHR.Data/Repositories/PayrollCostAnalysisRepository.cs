using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 工资成本分析仓储接口
    /// </summary>
    public interface IPayrollCostAnalysisRepository : IRepository<PayrollCostAnalysis>
    {
        /// <summary>
        /// 根据分析期间获取成本分析记录
        /// </summary>
        /// <param name="analysisPeriod">分析期间</param>
        /// <returns>成本分析记录列表</returns>
        Task<IEnumerable<PayrollCostAnalysis>> GetByAnalysisPeriodAsync(string analysisPeriod);
        
        /// <summary>
        /// 根据部门获取成本分析记录
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="startPeriod">开始期间</param>
        /// <param name="endPeriod">结束期间</param>
        /// <returns>成本分析记录列表</returns>
        Task<IEnumerable<PayrollCostAnalysis>> GetByDepartmentAsync(string department, string startPeriod = null, string endPeriod = null);
        
        /// <summary>
        /// 根据期间范围获取成本分析记录
        /// </summary>
        /// <param name="startPeriod">开始期间</param>
        /// <param name="endPeriod">结束期间</param>
        /// <returns>成本分析记录列表</returns>
        Task<IEnumerable<PayrollCostAnalysis>> GetByPeriodRangeAsync(string startPeriod, string endPeriod);
        
        /// <summary>
        /// 获取部门成本统计
        /// </summary>
        /// <param name="analysisPeriod">分析期间</param>
        /// <returns>部门成本统计列表</returns>
        Task<IEnumerable<DepartmentCostStatistics>> GetDepartmentCostStatisticsAsync(string analysisPeriod);
        
        /// <summary>
        /// 获取成本趋势分析
        /// </summary>
        /// <param name="department">部门（可选）</param>
        /// <param name="months">月份数</param>
        /// <returns>成本趋势数据</returns>
        Task<IEnumerable<CostTrendData>> GetCostTrendAsync(string department = null, int months = 12);
        
        /// <summary>
        /// 获取成本分布统计
        /// </summary>
        /// <param name="analysisPeriod">分析期间</param>
        /// <returns>成本分布统计</returns>
        Task<CostDistributionStatistics> GetCostDistributionAsync(string analysisPeriod);
        
        /// <summary>
        /// 获取成本汇总统计
        /// </summary>
        /// <param name="startPeriod">开始期间</param>
        /// <param name="endPeriod">结束期间</param>
        /// <returns>成本汇总统计</returns>
        Task<CostSummaryStatistics> GetCostSummaryAsync(string startPeriod, string endPeriod);
        
        /// <summary>
        /// 检查指定期间是否已有成本分析记录
        /// </summary>
        /// <param name="analysisPeriod">分析期间</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsForPeriodAsync(string analysisPeriod);
        
        /// <summary>
        /// 批量删除指定期间的成本分析记录
        /// </summary>
        /// <param name="analysisPeriod">分析期间</param>
        /// <returns>删除的记录数</returns>
        Task<int> DeleteByPeriodAsync(string analysisPeriod);
        
        /// <summary>
        /// 获取成本预算对比
        /// </summary>
        /// <param name="analysisPeriod">分析期间</param>
        /// <returns>预算对比数据</returns>
        Task<IEnumerable<BudgetComparisonData>> GetBudgetComparisonAsync(string analysisPeriod);
    }
    
    /// <summary>
    /// 工资成本分析仓储实现
    /// </summary>
    public class PayrollCostAnalysisRepository : BaseRepository<PayrollCostAnalysis>, IPayrollCostAnalysisRepository
    {
        public PayrollCostAnalysisRepository(IDatabaseService databaseService) 
            : base(databaseService, "PayrollCostAnalyses", "CostAnalysisId")
        {
        }
        
        protected override object GetInsertParameters(PayrollCostAnalysis entity)
        {
            return new
            {
                entity.AnalysisPeriod,
                entity.Department,
                entity.TotalEmployees,
                entity.TotalBaseSalary,
                entity.TotalAllowances,
                entity.TotalDeductions,
                entity.TotalNetSalary,
                entity.TotalSocialSecurity,
                entity.TotalOtherCosts,
                entity.TotalCost,
                entity.AverageCostPerEmployee,
                entity.BudgetAmount,
                entity.BudgetVariance,
                entity.BudgetVariancePercentage,
                entity.Notes,
                entity.CreatedBy,
                CreatedAt = DateTime.Now
            };
        }
        
        protected override object GetUpdateParameters(PayrollCostAnalysis entity)
        {
            return new
            {
                entity.AnalysisPeriod,
                entity.Department,
                entity.TotalEmployees,
                entity.TotalBaseSalary,
                entity.TotalAllowances,
                entity.TotalDeductions,
                entity.TotalNetSalary,
                entity.TotalSocialSecurity,
                entity.TotalOtherCosts,
                entity.TotalCost,
                entity.AverageCostPerEmployee,
                entity.BudgetAmount,
                entity.BudgetVariance,
                entity.BudgetVariancePercentage,
                entity.Notes,
                entity.UpdatedBy,
                UpdatedAt = DateTime.Now,
                entity.CostAnalysisId
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO PayrollCostAnalyses 
                (AnalysisPeriod, Department, TotalEmployees, TotalBaseSalary, TotalAllowances, 
                 TotalDeductions, TotalNetSalary, TotalSocialSecurity, TotalOtherCosts, TotalCost, 
                 AverageCostPerEmployee, BudgetAmount, BudgetVariance, BudgetVariancePercentage, 
                 Notes, CreatedBy, CreatedAt)
                VALUES 
                (@AnalysisPeriod, @Department, @TotalEmployees, @TotalBaseSalary, @TotalAllowances, 
                 @TotalDeductions, @TotalNetSalary, @TotalSocialSecurity, @TotalOtherCosts, @TotalCost, 
                 @AverageCostPerEmployee, @BudgetAmount, @BudgetVariance, @BudgetVariancePercentage, 
                 @Notes, @CreatedBy, @CreatedAt)";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE PayrollCostAnalyses SET 
                    AnalysisPeriod = @AnalysisPeriod,
                    Department = @Department,
                    TotalEmployees = @TotalEmployees,
                    TotalBaseSalary = @TotalBaseSalary,
                    TotalAllowances = @TotalAllowances,
                    TotalDeductions = @TotalDeductions,
                    TotalNetSalary = @TotalNetSalary,
                    TotalSocialSecurity = @TotalSocialSecurity,
                    TotalOtherCosts = @TotalOtherCosts,
                    TotalCost = @TotalCost,
                    AverageCostPerEmployee = @AverageCostPerEmployee,
                    BudgetAmount = @BudgetAmount,
                    BudgetVariance = @BudgetVariance,
                    BudgetVariancePercentage = @BudgetVariancePercentage,
                    Notes = @Notes,
                    UpdatedBy = @UpdatedBy,
                    UpdatedAt = @UpdatedAt
                WHERE CostAnalysisId = @CostAnalysisId";
        }
        
        public async Task<IEnumerable<PayrollCostAnalysis>> GetByAnalysisPeriodAsync(string analysisPeriod)
        {
            var sql = "SELECT * FROM PayrollCostAnalyses WHERE AnalysisPeriod = @AnalysisPeriod ORDER BY Department";
            return await QueryAsync(sql, new { AnalysisPeriod = analysisPeriod });
        }
        
        public async Task<IEnumerable<PayrollCostAnalysis>> GetByDepartmentAsync(string department, string startPeriod = null, string endPeriod = null)
        {
            var sql = "SELECT * FROM PayrollCostAnalyses WHERE Department = @Department";
            var parameters = new { Department = department, StartPeriod = startPeriod, EndPeriod = endPeriod };
            
            if (!string.IsNullOrEmpty(startPeriod) && !string.IsNullOrEmpty(endPeriod))
            {
                sql += " AND AnalysisPeriod BETWEEN @StartPeriod AND @EndPeriod";
            }
            else if (!string.IsNullOrEmpty(startPeriod))
            {
                sql += " AND AnalysisPeriod >= @StartPeriod";
            }
            else if (!string.IsNullOrEmpty(endPeriod))
            {
                sql += " AND AnalysisPeriod <= @EndPeriod";
            }
            
            sql += " ORDER BY AnalysisPeriod DESC";
            return await QueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<PayrollCostAnalysis>> GetByPeriodRangeAsync(string startPeriod, string endPeriod)
        {
            var sql = @"
                SELECT * FROM PayrollCostAnalyses 
                WHERE AnalysisPeriod BETWEEN @StartPeriod AND @EndPeriod 
                ORDER BY AnalysisPeriod DESC, Department";
            return await QueryAsync(sql, new { StartPeriod = startPeriod, EndPeriod = endPeriod });
        }
        
        public async Task<IEnumerable<DepartmentCostStatistics>> GetDepartmentCostStatisticsAsync(string analysisPeriod)
        {
            var sql = @"
                SELECT 
                    Department,
                    COUNT(*) as RecordCount,
                    SUM(TotalEmployees) as TotalEmployees,
                    SUM(TotalCost) as TotalCost,
                    AVG(AverageCostPerEmployee) as AverageCostPerEmployee,
                    SUM(BudgetAmount) as TotalBudget,
                    SUM(BudgetVariance) as TotalVariance,
                    AVG(BudgetVariancePercentage) as AverageVariancePercentage
                FROM PayrollCostAnalyses 
                WHERE AnalysisPeriod = @AnalysisPeriod
                GROUP BY Department
                ORDER BY TotalCost DESC";
            
            var result = await QueryAsync(sql, new { AnalysisPeriod = analysisPeriod });
            return result.Select(r => new DepartmentCostStatistics
            {
                Department = r.Department,
                RecordCount = r.RecordCount,
                TotalEmployees = r.TotalEmployees,
                TotalCost = r.TotalCost,
                AverageCostPerEmployee = r.AverageCostPerEmployee,
                TotalBudget = r.TotalBudget,
                TotalVariance = r.TotalVariance,
                AverageVariancePercentage = r.AverageVariancePercentage
            });
        }
        
        public async Task<IEnumerable<CostTrendData>> GetCostTrendAsync(string department = null, int months = 12)
        {
            var sql = @"
                SELECT 
                    AnalysisPeriod,
                    SUM(TotalCost) as TotalCost,
                    SUM(TotalEmployees) as TotalEmployees,
                    AVG(AverageCostPerEmployee) as AverageCostPerEmployee
                FROM PayrollCostAnalyses 
                WHERE AnalysisPeriod >= FORMAT(DATEADD(MONTH, -@Months, GETDATE()), 'yyyy-MM')";
            
            var parameters = new { Months = months, Department = department };
            
            if (!string.IsNullOrEmpty(department))
            {
                sql += " AND Department = @Department";
            }
            
            sql += @"
                GROUP BY AnalysisPeriod
                ORDER BY AnalysisPeriod";
            
            var result = await QueryAsync(sql, parameters);
            return result.Select(r => new CostTrendData
            {
                Period = r.AnalysisPeriod,
                TotalCost = r.TotalCost,
                TotalEmployees = r.TotalEmployees,
                AverageCostPerEmployee = r.AverageCostPerEmployee
            });
        }
        
        public async Task<CostDistributionStatistics> GetCostDistributionAsync(string analysisPeriod)
        {
            var sql = @"
                SELECT 
                    SUM(TotalBaseSalary) as TotalBaseSalary,
                    SUM(TotalAllowances) as TotalAllowances,
                    SUM(TotalDeductions) as TotalDeductions,
                    SUM(TotalSocialSecurity) as TotalSocialSecurity,
                    SUM(TotalOtherCosts) as TotalOtherCosts,
                    SUM(TotalCost) as TotalCost
                FROM PayrollCostAnalyses 
                WHERE AnalysisPeriod = @AnalysisPeriod";
            
            var result = await QuerySingleOrDefaultAsync(sql, new { AnalysisPeriod = analysisPeriod });
            return new CostDistributionStatistics
            {
                TotalBaseSalary = result?.TotalBaseSalary ?? 0,
                TotalAllowances = result?.TotalAllowances ?? 0,
                TotalDeductions = result?.TotalDeductions ?? 0,
                TotalSocialSecurity = result?.TotalSocialSecurity ?? 0,
                TotalOtherCosts = result?.TotalOtherCosts ?? 0,
                TotalCost = result?.TotalCost ?? 0
            };
        }
        
        public async Task<CostSummaryStatistics> GetCostSummaryAsync(string startPeriod, string endPeriod)
        {
            var sql = @"
                SELECT 
                    COUNT(DISTINCT AnalysisPeriod) as PeriodCount,
                    COUNT(DISTINCT Department) as DepartmentCount,
                    SUM(TotalEmployees) as TotalEmployees,
                    SUM(TotalCost) as TotalCost,
                    AVG(AverageCostPerEmployee) as AverageCostPerEmployee,
                    MIN(TotalCost) as MinCost,
                    MAX(TotalCost) as MaxCost,
                    SUM(BudgetAmount) as TotalBudget,
                    SUM(BudgetVariance) as TotalVariance
                FROM PayrollCostAnalyses 
                WHERE AnalysisPeriod BETWEEN @StartPeriod AND @EndPeriod";
            
            var result = await QuerySingleOrDefaultAsync(sql, new { StartPeriod = startPeriod, EndPeriod = endPeriod });
            return new CostSummaryStatistics
            {
                PeriodCount = result?.PeriodCount ?? 0,
                DepartmentCount = result?.DepartmentCount ?? 0,
                TotalEmployees = result?.TotalEmployees ?? 0,
                TotalCost = result?.TotalCost ?? 0,
                AverageCostPerEmployee = result?.AverageCostPerEmployee ?? 0,
                MinCost = result?.MinCost ?? 0,
                MaxCost = result?.MaxCost ?? 0,
                TotalBudget = result?.TotalBudget ?? 0,
                TotalVariance = result?.TotalVariance ?? 0
            };
        }
        
        public async Task<bool> ExistsForPeriodAsync(string analysisPeriod)
        {
            var sql = "SELECT COUNT(1) FROM PayrollCostAnalyses WHERE AnalysisPeriod = @AnalysisPeriod";
            var count = await QuerySingleOrDefaultAsync<int>(sql, new { AnalysisPeriod = analysisPeriod });
            return count > 0;
        }
        
        public async Task<int> DeleteByPeriodAsync(string analysisPeriod)
        {
            var sql = "DELETE FROM PayrollCostAnalyses WHERE AnalysisPeriod = @AnalysisPeriod";
            return await ExecuteAsync(sql, new { AnalysisPeriod = analysisPeriod });
        }
        
        public async Task<IEnumerable<BudgetComparisonData>> GetBudgetComparisonAsync(string analysisPeriod)
        {
            var sql = @"
                SELECT 
                    Department,
                    BudgetAmount,
                    TotalCost as ActualCost,
                    BudgetVariance,
                    BudgetVariancePercentage,
                    CASE 
                        WHEN BudgetVariance > 0 THEN '超预算'
                        WHEN BudgetVariance < 0 THEN '低于预算'
                        ELSE '符合预算'
                    END as VarianceStatus
                FROM PayrollCostAnalyses 
                WHERE AnalysisPeriod = @AnalysisPeriod
                ORDER BY ABS(BudgetVariancePercentage) DESC";
            
            var result = await QueryAsync(sql, new { AnalysisPeriod = analysisPeriod });
            return result.Select(r => new BudgetComparisonData
            {
                Department = r.Department,
                BudgetAmount = r.BudgetAmount,
                ActualCost = r.ActualCost,
                Variance = r.BudgetVariance,
                VariancePercentage = r.BudgetVariancePercentage,
                VarianceStatus = r.VarianceStatus
            });
        }
    }
    
    /// <summary>
    /// 部门成本统计
    /// </summary>
    public class DepartmentCostStatistics
    {
        public string Department { get; set; }
        public int RecordCount { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalCost { get; set; }
        public decimal AverageCostPerEmployee { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalVariance { get; set; }
        public decimal AverageVariancePercentage { get; set; }
    }
    
    /// <summary>
    /// 成本趋势数据
    /// </summary>
    public class CostTrendData
    {
        public string Period { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalEmployees { get; set; }
        public decimal AverageCostPerEmployee { get; set; }
    }
    
    /// <summary>
    /// 成本分布统计
    /// </summary>
    public class CostDistributionStatistics
    {
        public decimal TotalBaseSalary { get; set; }
        public decimal TotalAllowances { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalSocialSecurity { get; set; }
        public decimal TotalOtherCosts { get; set; }
        public decimal TotalCost { get; set; }
    }
    
    /// <summary>
    /// 成本汇总统计
    /// </summary>
    public class CostSummaryStatistics
    {
        public int PeriodCount { get; set; }
        public int DepartmentCount { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalCost { get; set; }
        public decimal AverageCostPerEmployee { get; set; }
        public decimal MinCost { get; set; }
        public decimal MaxCost { get; set; }
        public decimal TotalBudget { get; set; }
        public decimal TotalVariance { get; set; }
    }
    
    /// <summary>
    /// 预算对比数据
    /// </summary>
    public class BudgetComparisonData
    {
        public string Department { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal ActualCost { get; set; }
        public decimal Variance { get; set; }
        public decimal VariancePercentage { get; set; }
        public string VarianceStatus { get; set; }
    }
}