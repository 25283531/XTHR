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
    /// 社保Repository接口
    /// </summary>
    public interface ISocialSecurityRepository : IRepository<SocialSecurity>
    {
        /// <summary>
        /// 根据员工ID获取社保记录
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID和缴费期间获取社保记录
        /// </summary>
        Task<SocialSecurity> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取指定期间的所有社保记录
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 获取指定期间范围的社保记录
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取部门社保统计
        /// </summary>
        Task<IEnumerable<DepartmentSocialSecurityStatistics>> GetDepartmentStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取员工社保缴费历史
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetEmployeeSocialSecurityHistoryAsync(int employeeId, int months = 12);
        
        /// <summary>
        /// 获取社保缴费基数变更记录
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetBaseAmountChangesAsync(int employeeId);
        
        /// <summary>
        /// 检查员工在指定期间是否已有社保记录
        /// </summary>
        Task<bool> ExistsForEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取社保缴费汇总统计
        /// </summary>
        Task<SocialSecuritySummaryStatistics> GetSocialSecuritySummaryAsync(int year, int month);
        
        /// <summary>
        /// 获取员工年度社保缴费汇总
        /// </summary>
        Task<IEnumerable<EmployeeAnnualSocialSecurity>> GetEmployeeAnnualSocialSecurityAsync(int employeeId, int year);
        
        /// <summary>
        /// 批量删除指定期间的社保记录
        /// </summary>
        Task<int> BatchDeleteByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 获取即将到期的社保记录
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetExpiringRecordsAsync(DateTime expirationDate);
        
        /// <summary>
        /// 获取社保缴费率变更历史
        /// </summary>
        Task<IEnumerable<SocialSecurity>> GetRateChangesAsync(DateTime startDate, DateTime endDate);
    }
    
    /// <summary>
    /// 社保Repository实现
    /// </summary>
    public class SocialSecurityRepository : BaseRepository<SocialSecurity>, ISocialSecurityRepository
    {
        public SocialSecurityRepository(IDatabaseService databaseService)
            : base(databaseService, "SocialSecurities", "SocialSecurityId")
        {
        }
        
        protected override object EntityToInsertParameters(SocialSecurity entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.ContributionYear,
                entity.ContributionMonth,
                entity.ContributionPeriod,
                entity.BaseAmount,
                entity.PensionBase,
                entity.MedicalBase,
                entity.UnemploymentBase,
                entity.InjuryBase,
                entity.MaternityBase,
                entity.HousingFundBase,
                entity.PensionPersonalRate,
                entity.MedicalPersonalRate,
                entity.UnemploymentPersonalRate,
                entity.HousingFundPersonalRate,
                entity.PensionCompanyRate,
                entity.MedicalCompanyRate,
                entity.UnemploymentCompanyRate,
                entity.InjuryCompanyRate,
                entity.MaternityCompanyRate,
                entity.HousingFundCompanyRate,
                entity.PensionPersonalAmount,
                entity.MedicalPersonalAmount,
                entity.UnemploymentPersonalAmount,
                entity.HousingFundPersonalAmount,
                entity.TotalPersonalAmount,
                entity.PensionCompanyAmount,
                entity.MedicalCompanyAmount,
                entity.UnemploymentCompanyAmount,
                entity.InjuryCompanyAmount,
                entity.MaternityCompanyAmount,
                entity.HousingFundCompanyAmount,
                entity.TotalCompanyAmount,
                entity.TotalAmount,
                entity.PaymentStatus,
                entity.PaymentDate,
                entity.PaymentMethod,
                entity.PaymentReference,
                entity.EffectiveDate,
                entity.ExpirationDate,
                entity.AdjustmentReason,
                entity.AdjustmentAmount,
                entity.CalculatedBy,
                entity.CalculatedAt,
                entity.Remarks,
                entity.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override object EntityToUpdateParameters(SocialSecurity entity)
        {
            return new
            {
                entity.SocialSecurityId,
                entity.EmployeeId,
                entity.ContributionYear,
                entity.ContributionMonth,
                entity.ContributionPeriod,
                entity.BaseAmount,
                entity.PensionBase,
                entity.MedicalBase,
                entity.UnemploymentBase,
                entity.InjuryBase,
                entity.MaternityBase,
                entity.HousingFundBase,
                entity.PensionPersonalRate,
                entity.MedicalPersonalRate,
                entity.UnemploymentPersonalRate,
                entity.HousingFundPersonalRate,
                entity.PensionCompanyRate,
                entity.MedicalCompanyRate,
                entity.UnemploymentCompanyRate,
                entity.InjuryCompanyRate,
                entity.MaternityCompanyRate,
                entity.HousingFundCompanyRate,
                entity.PensionPersonalAmount,
                entity.MedicalPersonalAmount,
                entity.UnemploymentPersonalAmount,
                entity.HousingFundPersonalAmount,
                entity.TotalPersonalAmount,
                entity.PensionCompanyAmount,
                entity.MedicalCompanyAmount,
                entity.UnemploymentCompanyAmount,
                entity.InjuryCompanyAmount,
                entity.MaternityCompanyAmount,
                entity.HousingFundCompanyAmount,
                entity.TotalCompanyAmount,
                entity.TotalAmount,
                entity.PaymentStatus,
                entity.PaymentDate,
                entity.PaymentMethod,
                entity.PaymentReference,
                entity.EffectiveDate,
                entity.ExpirationDate,
                entity.AdjustmentReason,
                entity.AdjustmentAmount,
                entity.CalculatedBy,
                entity.CalculatedAt,
                entity.Remarks,
                entity.IsActive,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO SocialSecurities (
                    EmployeeId, ContributionYear, ContributionMonth, ContributionPeriod, BaseAmount,
                    PensionBase, MedicalBase, UnemploymentBase, InjuryBase, MaternityBase, HousingFundBase,
                    PensionPersonalRate, MedicalPersonalRate, UnemploymentPersonalRate, HousingFundPersonalRate,
                    PensionCompanyRate, MedicalCompanyRate, UnemploymentCompanyRate, InjuryCompanyRate,
                    MaternityCompanyRate, HousingFundCompanyRate, PensionPersonalAmount, MedicalPersonalAmount,
                    UnemploymentPersonalAmount, HousingFundPersonalAmount, TotalPersonalAmount,
                    PensionCompanyAmount, MedicalCompanyAmount, UnemploymentCompanyAmount, InjuryCompanyAmount,
                    MaternityCompanyAmount, HousingFundCompanyAmount, TotalCompanyAmount, TotalAmount,
                    PaymentStatus, PaymentDate, PaymentMethod, PaymentReference, EffectiveDate, ExpirationDate,
                    AdjustmentReason, AdjustmentAmount, CalculatedBy, CalculatedAt, Remarks, IsActive,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeId, @ContributionYear, @ContributionMonth, @ContributionPeriod, @BaseAmount,
                    @PensionBase, @MedicalBase, @UnemploymentBase, @InjuryBase, @MaternityBase, @HousingFundBase,
                    @PensionPersonalRate, @MedicalPersonalRate, @UnemploymentPersonalRate, @HousingFundPersonalRate,
                    @PensionCompanyRate, @MedicalCompanyRate, @UnemploymentCompanyRate, @InjuryCompanyRate,
                    @MaternityCompanyRate, @HousingFundCompanyRate, @PensionPersonalAmount, @MedicalPersonalAmount,
                    @UnemploymentPersonalAmount, @HousingFundPersonalAmount, @TotalPersonalAmount,
                    @PensionCompanyAmount, @MedicalCompanyAmount, @UnemploymentCompanyAmount, @InjuryCompanyAmount,
                    @MaternityCompanyAmount, @HousingFundCompanyAmount, @TotalCompanyAmount, @TotalAmount,
                    @PaymentStatus, @PaymentDate, @PaymentMethod, @PaymentReference, @EffectiveDate, @ExpirationDate,
                    @AdjustmentReason, @AdjustmentAmount, @CalculatedBy, @CalculatedAt, @Remarks, @IsActive,
                    @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE SocialSecurities SET
                    EmployeeId = @EmployeeId, ContributionYear = @ContributionYear, ContributionMonth = @ContributionMonth,
                    ContributionPeriod = @ContributionPeriod, BaseAmount = @BaseAmount, PensionBase = @PensionBase,
                    MedicalBase = @MedicalBase, UnemploymentBase = @UnemploymentBase, InjuryBase = @InjuryBase,
                    MaternityBase = @MaternityBase, HousingFundBase = @HousingFundBase, PensionPersonalRate = @PensionPersonalRate,
                    MedicalPersonalRate = @MedicalPersonalRate, UnemploymentPersonalRate = @UnemploymentPersonalRate,
                    HousingFundPersonalRate = @HousingFundPersonalRate, PensionCompanyRate = @PensionCompanyRate,
                    MedicalCompanyRate = @MedicalCompanyRate, UnemploymentCompanyRate = @UnemploymentCompanyRate,
                    InjuryCompanyRate = @InjuryCompanyRate, MaternityCompanyRate = @MaternityCompanyRate,
                    HousingFundCompanyRate = @HousingFundCompanyRate, PensionPersonalAmount = @PensionPersonalAmount,
                    MedicalPersonalAmount = @MedicalPersonalAmount, UnemploymentPersonalAmount = @UnemploymentPersonalAmount,
                    HousingFundPersonalAmount = @HousingFundPersonalAmount, TotalPersonalAmount = @TotalPersonalAmount,
                    PensionCompanyAmount = @PensionCompanyAmount, MedicalCompanyAmount = @MedicalCompanyAmount,
                    UnemploymentCompanyAmount = @UnemploymentCompanyAmount, InjuryCompanyAmount = @InjuryCompanyAmount,
                    MaternityCompanyAmount = @MaternityCompanyAmount, HousingFundCompanyAmount = @HousingFundCompanyAmount,
                    TotalCompanyAmount = @TotalCompanyAmount, TotalAmount = @TotalAmount, PaymentStatus = @PaymentStatus,
                    PaymentDate = @PaymentDate, PaymentMethod = @PaymentMethod, PaymentReference = @PaymentReference,
                    EffectiveDate = @EffectiveDate, ExpirationDate = @ExpirationDate, AdjustmentReason = @AdjustmentReason,
                    AdjustmentAmount = @AdjustmentAmount, CalculatedBy = @CalculatedBy, CalculatedAt = @CalculatedAt,
                    Remarks = @Remarks, IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                WHERE SocialSecurityId = @SocialSecurityId";
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetByEmployeeIdAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY ContributionYear DESC, ContributionMonth DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { EmployeeId = employeeId });
        }
        
        public async Task<SocialSecurity> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE EmployeeId = @EmployeeId AND ContributionYear = @Year AND ContributionMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<SocialSecurity>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetByPeriodAsync(int year, int month)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE ContributionYear = @Year AND ContributionMonth = @Month AND IsActive = 1
                ORDER BY EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE EffectiveDate >= @StartDate AND EffectiveDate <= @EndDate AND IsActive = 1
                ORDER BY EffectiveDate DESC, EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { StartDate = startDate, EndDate = endDate });
        }
        
        public async Task<IEnumerable<DepartmentSocialSecurityStatistics>> GetDepartmentStatisticsAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    e.Department,
                    COUNT(*) as EmployeeCount,
                    SUM(s.TotalPersonalAmount) as TotalPersonalAmount,
                    AVG(s.TotalPersonalAmount) as AveragePersonalAmount,
                    SUM(s.TotalCompanyAmount) as TotalCompanyAmount,
                    AVG(s.TotalCompanyAmount) as AverageCompanyAmount,
                    SUM(s.TotalAmount) as TotalAmount,
                    SUM(s.PensionPersonalAmount + s.PensionCompanyAmount) as TotalPensionAmount,
                    SUM(s.MedicalPersonalAmount + s.MedicalCompanyAmount) as TotalMedicalAmount,
                    SUM(s.HousingFundPersonalAmount + s.HousingFundCompanyAmount) as TotalHousingFundAmount
                FROM SocialSecurities s
                INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId
                WHERE s.ContributionYear = @Year AND s.ContributionMonth = @Month AND s.IsActive = 1
                GROUP BY e.Department
                ORDER BY e.Department";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<DepartmentSocialSecurityStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetEmployeeSocialSecurityHistoryAsync(int employeeId, int months = 12)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY ContributionYear DESC, ContributionMonth DESC
                LIMIT @Months";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { EmployeeId = employeeId, Months = months });
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetBaseAmountChangesAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY EffectiveDate DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { EmployeeId = employeeId });
        }
        
        public async Task<bool> ExistsForEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT COUNT(*) FROM SocialSecurities 
                WHERE EmployeeId = @EmployeeId AND ContributionYear = @Year AND ContributionMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
            return count > 0;
        }
        
        public async Task<SocialSecuritySummaryStatistics> GetSocialSecuritySummaryAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    COUNT(*) as TotalEmployees,
                    SUM(TotalPersonalAmount) as TotalPersonalAmount,
                    AVG(TotalPersonalAmount) as AveragePersonalAmount,
                    SUM(TotalCompanyAmount) as TotalCompanyAmount,
                    AVG(TotalCompanyAmount) as AverageCompanyAmount,
                    SUM(TotalAmount) as TotalAmount,
                    SUM(PensionPersonalAmount + PensionCompanyAmount) as TotalPensionAmount,
                    SUM(MedicalPersonalAmount + MedicalCompanyAmount) as TotalMedicalAmount,
                    SUM(UnemploymentPersonalAmount + UnemploymentCompanyAmount) as TotalUnemploymentAmount,
                    SUM(InjuryCompanyAmount) as TotalInjuryAmount,
                    SUM(MaternityCompanyAmount) as TotalMaternityAmount,
                    SUM(HousingFundPersonalAmount + HousingFundCompanyAmount) as TotalHousingFundAmount
                FROM SocialSecurities 
                WHERE ContributionYear = @Year AND ContributionMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<SocialSecuritySummaryStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<EmployeeAnnualSocialSecurity>> GetEmployeeAnnualSocialSecurityAsync(int employeeId, int year)
        {
            const string sql = @"
                SELECT 
                    ContributionMonth,
                    TotalPersonalAmount,
                    TotalCompanyAmount,
                    TotalAmount,
                    PensionPersonalAmount + PensionCompanyAmount as PensionAmount,
                    MedicalPersonalAmount + MedicalCompanyAmount as MedicalAmount,
                    HousingFundPersonalAmount + HousingFundCompanyAmount as HousingFundAmount
                FROM SocialSecurities 
                WHERE EmployeeId = @EmployeeId AND ContributionYear = @Year AND IsActive = 1
                ORDER BY ContributionMonth";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<EmployeeAnnualSocialSecurity>(sql, new { EmployeeId = employeeId, Year = year });
        }
        
        public async Task<int> BatchDeleteByPeriodAsync(int year, int month)
        {
            const string sql = @"
                UPDATE SocialSecurities 
                SET IsActive = 0, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                WHERE ContributionYear = @Year AND ContributionMonth = @Month";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, new 
            { 
                Year = year, 
                Month = month,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            });
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetExpiringRecordsAsync(DateTime expirationDate)
        {
            const string sql = @"
                SELECT s.*, e.EmployeeName, e.Department
                FROM SocialSecurities s
                INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId
                WHERE s.ExpirationDate <= @ExpirationDate AND s.IsActive = 1
                ORDER BY s.ExpirationDate, e.EmployeeName";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { ExpirationDate = expirationDate });
        }
        
        public async Task<IEnumerable<SocialSecurity>> GetRateChangesAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM SocialSecurities 
                WHERE EffectiveDate >= @StartDate AND EffectiveDate <= @EndDate AND IsActive = 1
                ORDER BY EffectiveDate DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<SocialSecurity>(sql, new { StartDate = startDate, EndDate = endDate });
        }
    }
    
    /// <summary>
    /// 部门社保统计
    /// </summary>
    public class DepartmentSocialSecurityStatistics
    {
        public string Department { get; set; }
        public int EmployeeCount { get; set; }
        public decimal TotalPersonalAmount { get; set; }
        public decimal AveragePersonalAmount { get; set; }
        public decimal TotalCompanyAmount { get; set; }
        public decimal AverageCompanyAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPensionAmount { get; set; }
        public decimal TotalMedicalAmount { get; set; }
        public decimal TotalHousingFundAmount { get; set; }
    }
    
    /// <summary>
    /// 社保汇总统计
    /// </summary>
    public class SocialSecuritySummaryStatistics
    {
        public int TotalEmployees { get; set; }
        public decimal TotalPersonalAmount { get; set; }
        public decimal AveragePersonalAmount { get; set; }
        public decimal TotalCompanyAmount { get; set; }
        public decimal AverageCompanyAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPensionAmount { get; set; }
        public decimal TotalMedicalAmount { get; set; }
        public decimal TotalUnemploymentAmount { get; set; }
        public decimal TotalInjuryAmount { get; set; }
        public decimal TotalMaternityAmount { get; set; }
        public decimal TotalHousingFundAmount { get; set; }
    }
    
    /// <summary>
    /// 员工年度社保
    /// </summary>
    public class EmployeeAnnualSocialSecurity
    {
        public int ContributionMonth { get; set; }
        public decimal TotalPersonalAmount { get; set; }
        public decimal TotalCompanyAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PensionAmount { get; set; }
        public decimal MedicalAmount { get; set; }
        public decimal HousingFundAmount { get; set; }
    }
}