using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 其他补偿奖惩Repository接口
    /// </summary>
    public interface IOtherCompensationPenaltyRepository : IRepository<OtherCompensationPenalty>
    {
        /// <summary>
        /// 根据员工ID获取补偿奖惩记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID和期间获取补偿奖惩记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取指定期间的所有补偿奖惩记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 根据类型获取记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetByTypeAsync(string type);
        
        /// <summary>
        /// 根据类型和期间获取记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetByTypeAndPeriodAsync(string type, int year, int month);
        
        /// <summary>
        /// 获取指定期间范围的记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取部门补偿奖惩统计
        /// </summary>
        Task<IEnumerable<DepartmentCompensationPenaltyStatistics>> GetDepartmentStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取员工补偿奖惩历史
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetEmployeeHistoryAsync(int employeeId, int months = 12);
        
        /// <summary>
        /// 获取待审批的记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetPendingApprovalAsync();
        
        /// <summary>
        /// 批量更新审批状态
        /// </summary>
        Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> ids, string approvalStatus, string approvedBy);
        
        /// <summary>
        /// 获取补偿奖惩汇总统计
        /// </summary>
        Task<CompensationPenaltySummaryStatistics> GetSummaryStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取类型分布统计
        /// </summary>
        Task<IEnumerable<CompensationPenaltyTypeDistribution>> GetTypeDistributionAsync(int year, int month);
        
        /// <summary>
        /// 获取员工年度补偿奖惩汇总
        /// </summary>
        Task<IEnumerable<EmployeeAnnualCompensationPenalty>> GetEmployeeAnnualSummaryAsync(int employeeId, int year);
        
        /// <summary>
        /// 获取即将生效的记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetUpcomingRecordsAsync(DateTime effectiveDate);
        
        /// <summary>
        /// 获取即将过期的记录
        /// </summary>
        Task<IEnumerable<OtherCompensationPenalty>> GetExpiringRecordsAsync(DateTime expirationDate);
    }
    
    /// <summary>
    /// 其他补偿奖惩Repository实现
    /// </summary>
    public class OtherCompensationPenaltyRepository : BaseRepository<OtherCompensationPenalty>, IOtherCompensationPenaltyRepository
    {
        public OtherCompensationPenaltyRepository(IDatabaseService databaseService)
            : base(databaseService, "OtherCompensationPenalties", "CompensationPenaltyId")
        {
        }
        
        protected override object EntityToInsertParameters(OtherCompensationPenalty entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.Type,
                entity.Category,
                entity.Name,
                entity.Description,
                entity.Amount,
                entity.Quantity,
                entity.UnitPrice,
                entity.TotalAmount,
                entity.TaxableAmount,
                entity.TaxExemptAmount,
                entity.CalculationMethod,
                entity.CalculationFormula,
                entity.ApplicableYear,
                entity.ApplicableMonth,
                entity.ApplicablePeriod,
                entity.EffectiveDate,
                entity.ExpirationDate,
                entity.PaymentDate,
                entity.PaymentMethod,
                entity.PaymentStatus,
                entity.ApprovalStatus,
                entity.ApprovedBy,
                entity.ApprovedAt,
                entity.AppliedBy,
                entity.AppliedAt,
                entity.ProcessedBy,
                entity.ProcessedAt,
                entity.RelatedDocuments,
                entity.Reason,
                entity.Evidence,
                entity.Priority,
                entity.IsRecurring,
                entity.RecurrencePattern,
                entity.NextOccurrence,
                entity.Remarks,
                entity.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override object EntityToUpdateParameters(OtherCompensationPenalty entity)
        {
            return new
            {
                entity.CompensationPenaltyId,
                entity.EmployeeId,
                entity.Type,
                entity.Category,
                entity.Name,
                entity.Description,
                entity.Amount,
                entity.Quantity,
                entity.UnitPrice,
                entity.TotalAmount,
                entity.TaxableAmount,
                entity.TaxExemptAmount,
                entity.CalculationMethod,
                entity.CalculationFormula,
                entity.ApplicableYear,
                entity.ApplicableMonth,
                entity.ApplicablePeriod,
                entity.EffectiveDate,
                entity.ExpirationDate,
                entity.PaymentDate,
                entity.PaymentMethod,
                entity.PaymentStatus,
                entity.ApprovalStatus,
                entity.ApprovedBy,
                entity.ApprovedAt,
                entity.AppliedBy,
                entity.AppliedAt,
                entity.ProcessedBy,
                entity.ProcessedAt,
                entity.RelatedDocuments,
                entity.Reason,
                entity.Evidence,
                entity.Priority,
                entity.IsRecurring,
                entity.RecurrencePattern,
                entity.NextOccurrence,
                entity.Remarks,
                entity.IsActive,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO OtherCompensationPenalties (
                    EmployeeId, Type, Category, Name, Description, Amount, Quantity, UnitPrice,
                    TotalAmount, TaxableAmount, TaxExemptAmount, CalculationMethod, CalculationFormula,
                    ApplicableYear, ApplicableMonth, ApplicablePeriod, EffectiveDate, ExpirationDate,
                    PaymentDate, PaymentMethod, PaymentStatus, ApprovalStatus, ApprovedBy, ApprovedAt,
                    AppliedBy, AppliedAt, ProcessedBy, ProcessedAt, RelatedDocuments, Reason, Evidence,
                    Priority, IsRecurring, RecurrencePattern, NextOccurrence, Remarks, IsActive,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeId, @Type, @Category, @Name, @Description, @Amount, @Quantity, @UnitPrice,
                    @TotalAmount, @TaxableAmount, @TaxExemptAmount, @CalculationMethod, @CalculationFormula,
                    @ApplicableYear, @ApplicableMonth, @ApplicablePeriod, @EffectiveDate, @ExpirationDate,
                    @PaymentDate, @PaymentMethod, @PaymentStatus, @ApprovalStatus, @ApprovedBy, @ApprovedAt,
                    @AppliedBy, @AppliedAt, @ProcessedBy, @ProcessedAt, @RelatedDocuments, @Reason, @Evidence,
                    @Priority, @IsRecurring, @RecurrencePattern, @NextOccurrence, @Remarks, @IsActive,
                    @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE OtherCompensationPenalties SET
                    EmployeeId = @EmployeeId, Type = @Type, Category = @Category, Name = @Name,
                    Description = @Description, Amount = @Amount, Quantity = @Quantity, UnitPrice = @UnitPrice,
                    TotalAmount = @TotalAmount, TaxableAmount = @TaxableAmount, TaxExemptAmount = @TaxExemptAmount,
                    CalculationMethod = @CalculationMethod, CalculationFormula = @CalculationFormula,
                    ApplicableYear = @ApplicableYear, ApplicableMonth = @ApplicableMonth, ApplicablePeriod = @ApplicablePeriod,
                    EffectiveDate = @EffectiveDate, ExpirationDate = @ExpirationDate, PaymentDate = @PaymentDate,
                    PaymentMethod = @PaymentMethod, PaymentStatus = @PaymentStatus, ApprovalStatus = @ApprovalStatus,
                    ApprovedBy = @ApprovedBy, ApprovedAt = @ApprovedAt, AppliedBy = @AppliedBy, AppliedAt = @AppliedAt,
                    ProcessedBy = @ProcessedBy, ProcessedAt = @ProcessedAt, RelatedDocuments = @RelatedDocuments,
                    Reason = @Reason, Evidence = @Evidence, Priority = @Priority, IsRecurring = @IsRecurring,
                    RecurrencePattern = @RecurrencePattern, NextOccurrence = @NextOccurrence, Remarks = @Remarks,
                    IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                WHERE CompensationPenaltyId = @CompensationPenaltyId";
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetByEmployeeIdAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY ApplicableYear DESC, ApplicableMonth DESC, CreatedAt DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { EmployeeId = employeeId });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE EmployeeId = @EmployeeId AND ApplicableYear = @Year AND ApplicableMonth = @Month AND IsActive = 1
                ORDER BY Type, Category";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetByPeriodAsync(int year, int month)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE ApplicableYear = @Year AND ApplicableMonth = @Month AND IsActive = 1
                ORDER BY EmployeeId, Type";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetByTypeAsync(string type)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE Type = @Type AND IsActive = 1
                ORDER BY ApplicableYear DESC, ApplicableMonth DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { Type = type });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetByTypeAndPeriodAsync(string type, int year, int month)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE Type = @Type AND ApplicableYear = @Year AND ApplicableMonth = @Month AND IsActive = 1
                ORDER BY EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { Type = type, Year = year, Month = month });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE EffectiveDate >= @StartDate AND EffectiveDate <= @EndDate AND IsActive = 1
                ORDER BY EffectiveDate DESC, EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { StartDate = startDate, EndDate = endDate });
        }
        
        public async Task<IEnumerable<DepartmentCompensationPenaltyStatistics>> GetDepartmentStatisticsAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    e.Department,
                    COUNT(*) as TotalRecords,
                    COUNT(DISTINCT o.EmployeeId) as EmployeeCount,
                    SUM(CASE WHEN o.Type = '补偿' THEN o.TotalAmount ELSE 0 END) as TotalCompensationAmount,
                    SUM(CASE WHEN o.Type = '奖励' THEN o.TotalAmount ELSE 0 END) as TotalRewardAmount,
                    SUM(CASE WHEN o.Type = '扣款' THEN o.TotalAmount ELSE 0 END) as TotalPenaltyAmount,
                    SUM(o.TotalAmount) as NetAmount,
                    AVG(o.TotalAmount) as AverageAmount
                FROM OtherCompensationPenalties o
                INNER JOIN Employees e ON o.EmployeeId = e.EmployeeId
                WHERE o.ApplicableYear = @Year AND o.ApplicableMonth = @Month AND o.IsActive = 1
                GROUP BY e.Department
                ORDER BY e.Department";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<DepartmentCompensationPenaltyStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetEmployeeHistoryAsync(int employeeId, int months = 12)
        {
            const string sql = @"
                SELECT * FROM OtherCompensationPenalties 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY ApplicableYear DESC, ApplicableMonth DESC, CreatedAt DESC
                LIMIT @Limit";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { EmployeeId = employeeId, Limit = months * 10 });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetPendingApprovalAsync()
        {
            const string sql = @"
                SELECT o.*, e.EmployeeName, e.Department, e.Position
                FROM OtherCompensationPenalties o
                INNER JOIN Employees e ON o.EmployeeId = e.EmployeeId
                WHERE o.ApprovalStatus = '待审批' AND o.IsActive = 1
                ORDER BY o.Priority DESC, o.AppliedAt";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql);
        }
        
        public async Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> ids, string approvalStatus, string approvedBy)
        {
            const string sql = @"
                UPDATE OtherCompensationPenalties 
                SET ApprovalStatus = @ApprovalStatus, ApprovedBy = @ApprovedBy, ApprovedAt = @ApprovedAt, UpdatedAt = @UpdatedAt
                WHERE CompensationPenaltyId IN @Ids";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, new 
            { 
                ApprovalStatus = approvalStatus, 
                ApprovedBy = approvedBy, 
                ApprovedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Ids = ids 
            });
        }
        
        public async Task<CompensationPenaltySummaryStatistics> GetSummaryStatisticsAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    COUNT(*) as TotalRecords,
                    COUNT(DISTINCT EmployeeId) as TotalEmployees,
                    SUM(CASE WHEN Type = '补偿' THEN TotalAmount ELSE 0 END) as TotalCompensationAmount,
                    SUM(CASE WHEN Type = '奖励' THEN TotalAmount ELSE 0 END) as TotalRewardAmount,
                    SUM(CASE WHEN Type = '扣款' THEN TotalAmount ELSE 0 END) as TotalPenaltyAmount,
                    SUM(TotalAmount) as NetAmount,
                    AVG(TotalAmount) as AverageAmount,
                    SUM(TaxableAmount) as TotalTaxableAmount,
                    SUM(TaxExemptAmount) as TotalTaxExemptAmount,
                    COUNT(CASE WHEN ApprovalStatus = '待审批' THEN 1 END) as PendingApprovalCount,
                    COUNT(CASE WHEN ApprovalStatus = '已审批' THEN 1 END) as ApprovedCount
                FROM OtherCompensationPenalties 
                WHERE ApplicableYear = @Year AND ApplicableMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<CompensationPenaltySummaryStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<CompensationPenaltyTypeDistribution>> GetTypeDistributionAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    Type,
                    Category,
                    COUNT(*) as Count,
                    SUM(TotalAmount) as TotalAmount,
                    AVG(TotalAmount) as AverageAmount,
                    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM OtherCompensationPenalties WHERE ApplicableYear = @Year AND ApplicableMonth = @Month AND IsActive = 1), 2) as Percentage
                FROM OtherCompensationPenalties 
                WHERE ApplicableYear = @Year AND ApplicableMonth = @Month AND IsActive = 1
                GROUP BY Type, Category
                ORDER BY Type, Category";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<CompensationPenaltyTypeDistribution>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<EmployeeAnnualCompensationPenalty>> GetEmployeeAnnualSummaryAsync(int employeeId, int year)
        {
            const string sql = @"
                SELECT 
                    ApplicableMonth,
                    SUM(CASE WHEN Type = '补偿' THEN TotalAmount ELSE 0 END) as CompensationAmount,
                    SUM(CASE WHEN Type = '奖励' THEN TotalAmount ELSE 0 END) as RewardAmount,
                    SUM(CASE WHEN Type = '扣款' THEN TotalAmount ELSE 0 END) as PenaltyAmount,
                    SUM(TotalAmount) as NetAmount,
                    COUNT(*) as RecordCount
                FROM OtherCompensationPenalties 
                WHERE EmployeeId = @EmployeeId AND ApplicableYear = @Year AND IsActive = 1
                GROUP BY ApplicableMonth
                ORDER BY ApplicableMonth";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<EmployeeAnnualCompensationPenalty>(sql, new { EmployeeId = employeeId, Year = year });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetUpcomingRecordsAsync(DateTime effectiveDate)
        {
            const string sql = @"
                SELECT o.*, e.EmployeeName, e.Department
                FROM OtherCompensationPenalties o
                INNER JOIN Employees e ON o.EmployeeId = e.EmployeeId
                WHERE o.EffectiveDate <= @EffectiveDate AND o.PaymentStatus = '未支付' AND o.IsActive = 1
                ORDER BY o.EffectiveDate, e.EmployeeName";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { EffectiveDate = effectiveDate });
        }
        
        public async Task<IEnumerable<OtherCompensationPenalty>> GetExpiringRecordsAsync(DateTime expirationDate)
        {
            const string sql = @"
                SELECT o.*, e.EmployeeName, e.Department
                FROM OtherCompensationPenalties o
                INNER JOIN Employees e ON o.EmployeeId = e.EmployeeId
                WHERE o.ExpirationDate <= @ExpirationDate AND o.IsActive = 1
                ORDER BY o.ExpirationDate, e.EmployeeName";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<OtherCompensationPenalty>(sql, new { ExpirationDate = expirationDate });
        }
    }
    
    /// <summary>
    /// 部门补偿奖惩统计
    /// </summary>
    public class DepartmentCompensationPenaltyStatistics
    {
        public string Department { get; set; }
        public int TotalRecords { get; set; }
        public int EmployeeCount { get; set; }
        public decimal TotalCompensationAmount { get; set; }
        public decimal TotalRewardAmount { get; set; }
        public decimal TotalPenaltyAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AverageAmount { get; set; }
    }
    
    /// <summary>
    /// 补偿奖惩汇总统计
    /// </summary>
    public class CompensationPenaltySummaryStatistics
    {
        public int TotalRecords { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalCompensationAmount { get; set; }
        public decimal TotalRewardAmount { get; set; }
        public decimal TotalPenaltyAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal TotalTaxableAmount { get; set; }
        public decimal TotalTaxExemptAmount { get; set; }
        public int PendingApprovalCount { get; set; }
        public int ApprovedCount { get; set; }
    }
    
    /// <summary>
    /// 补偿奖惩类型分布
    /// </summary>
    public class CompensationPenaltyTypeDistribution
    {
        public string Type { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal Percentage { get; set; }
    }
    
    /// <summary>
    /// 员工年度补偿奖惩
    /// </summary>
    public class EmployeeAnnualCompensationPenalty
    {
        public int ApplicableMonth { get; set; }
        public decimal CompensationAmount { get; set; }
        public decimal RewardAmount { get; set; }
        public decimal PenaltyAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int RecordCount { get; set; }
    }
}