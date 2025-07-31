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
    /// 绩效评分Repository接口
    /// </summary>
    public interface IPerformanceRepository : IRepository<PerformanceScore>
    {
        /// <summary>
        /// 根据员工ID获取绩效记录
        /// </summary>
        Task<IEnumerable<PerformanceScore>> GetByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID和评估期间获取绩效记录
        /// </summary>
        Task<PerformanceScore> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取指定期间的所有绩效记录
        /// </summary>
        Task<IEnumerable<PerformanceScore>> GetByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 获取指定期间范围的绩效记录
        /// </summary>
        Task<IEnumerable<PerformanceScore>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取部门绩效统计
        /// </summary>
        Task<IEnumerable<DepartmentPerformanceStatistics>> GetDepartmentStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取员工绩效趋势
        /// </summary>
        Task<IEnumerable<PerformanceScore>> GetEmployeePerformanceTrendAsync(int employeeId, int months = 12);
        
        /// <summary>
        /// 获取待审批的绩效记录
        /// </summary>
        Task<IEnumerable<PerformanceScore>> GetPendingApprovalAsync();
        
        /// <summary>
        /// 批量更新审批状态
        /// </summary>
        Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> performanceIds, string approvalStatus, string approvedBy);
        
        /// <summary>
        /// 检查员工在指定期间是否已有绩效记录
        /// </summary>
        Task<bool> ExistsForEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取绩效分布统计
        /// </summary>
        Task<IEnumerable<PerformanceDistribution>> GetPerformanceDistributionAsync(int year, int month);
    }
    
    /// <summary>
    /// 绩效评分Repository实现
    /// </summary>
    public class PerformanceRepository : BaseRepository<PerformanceScore>, IPerformanceRepository
    {
        public PerformanceRepository(IDatabaseService databaseService)
            : base(databaseService, "PerformanceScores", "PerformanceId")
        {
        }
        
        protected override object EntityToInsertParameters(PerformanceScore entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.EvaluationYear,
                entity.EvaluationMonth,
                entity.EvaluationPeriod,
                entity.EvaluationType,
                entity.EvaluatorId,
                entity.EvaluatorName,
                entity.WorkQualityScore,
                entity.WorkEfficiencyScore,
                entity.WorkAttitudeScore,
                entity.TeamworkScore,
                entity.InnovationScore,
                entity.LeadershipScore,
                entity.CommunicationScore,
                entity.ProfessionalSkillScore,
                entity.GoalAchievementScore,
                entity.OverallScore,
                entity.PerformanceLevel,
                entity.PerformanceCoefficient,
                entity.SelfEvaluation,
                entity.SupervisorEvaluation,
                entity.HrEvaluation,
                entity.ImprovementSuggestions,
                entity.Goals,
                entity.Achievements,
                entity.Challenges,
                entity.DevelopmentPlan,
                entity.ApprovalStatus,
                entity.ApprovedBy,
                entity.ApprovedAt,
                entity.EvaluationDate,
                entity.SubmittedAt,
                entity.Remarks,
                entity.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override object EntityToUpdateParameters(PerformanceScore entity)
        {
            return new
            {
                entity.PerformanceId,
                entity.EmployeeId,
                entity.EvaluationYear,
                entity.EvaluationMonth,
                entity.EvaluationPeriod,
                entity.EvaluationType,
                entity.EvaluatorId,
                entity.EvaluatorName,
                entity.WorkQualityScore,
                entity.WorkEfficiencyScore,
                entity.WorkAttitudeScore,
                entity.TeamworkScore,
                entity.InnovationScore,
                entity.LeadershipScore,
                entity.CommunicationScore,
                entity.ProfessionalSkillScore,
                entity.GoalAchievementScore,
                entity.OverallScore,
                entity.PerformanceLevel,
                entity.PerformanceCoefficient,
                entity.SelfEvaluation,
                entity.SupervisorEvaluation,
                entity.HrEvaluation,
                entity.ImprovementSuggestions,
                entity.Goals,
                entity.Achievements,
                entity.Challenges,
                entity.DevelopmentPlan,
                entity.ApprovalStatus,
                entity.ApprovedBy,
                entity.ApprovedAt,
                entity.EvaluationDate,
                entity.SubmittedAt,
                entity.Remarks,
                entity.IsActive,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO PerformanceScores (
                    EmployeeId, EvaluationYear, EvaluationMonth, EvaluationPeriod, EvaluationType,
                    EvaluatorId, EvaluatorName, WorkQualityScore, WorkEfficiencyScore, WorkAttitudeScore,
                    TeamworkScore, InnovationScore, LeadershipScore, CommunicationScore, ProfessionalSkillScore,
                    GoalAchievementScore, OverallScore, PerformanceLevel, PerformanceCoefficient,
                    SelfEvaluation, SupervisorEvaluation, HrEvaluation, ImprovementSuggestions,
                    Goals, Achievements, Challenges, DevelopmentPlan, ApprovalStatus, ApprovedBy, ApprovedAt,
                    EvaluationDate, SubmittedAt, Remarks, IsActive, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeId, @EvaluationYear, @EvaluationMonth, @EvaluationPeriod, @EvaluationType,
                    @EvaluatorId, @EvaluatorName, @WorkQualityScore, @WorkEfficiencyScore, @WorkAttitudeScore,
                    @TeamworkScore, @InnovationScore, @LeadershipScore, @CommunicationScore, @ProfessionalSkillScore,
                    @GoalAchievementScore, @OverallScore, @PerformanceLevel, @PerformanceCoefficient,
                    @SelfEvaluation, @SupervisorEvaluation, @HrEvaluation, @ImprovementSuggestions,
                    @Goals, @Achievements, @Challenges, @DevelopmentPlan, @ApprovalStatus, @ApprovedBy, @ApprovedAt,
                    @EvaluationDate, @SubmittedAt, @Remarks, @IsActive, @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE PerformanceScores SET
                    EmployeeId = @EmployeeId, EvaluationYear = @EvaluationYear, EvaluationMonth = @EvaluationMonth,
                    EvaluationPeriod = @EvaluationPeriod, EvaluationType = @EvaluationType, EvaluatorId = @EvaluatorId,
                    EvaluatorName = @EvaluatorName, WorkQualityScore = @WorkQualityScore, WorkEfficiencyScore = @WorkEfficiencyScore,
                    WorkAttitudeScore = @WorkAttitudeScore, TeamworkScore = @TeamworkScore, InnovationScore = @InnovationScore,
                    LeadershipScore = @LeadershipScore, CommunicationScore = @CommunicationScore, ProfessionalSkillScore = @ProfessionalSkillScore,
                    GoalAchievementScore = @GoalAchievementScore, OverallScore = @OverallScore, PerformanceLevel = @PerformanceLevel,
                    PerformanceCoefficient = @PerformanceCoefficient, SelfEvaluation = @SelfEvaluation, SupervisorEvaluation = @SupervisorEvaluation,
                    HrEvaluation = @HrEvaluation, ImprovementSuggestions = @ImprovementSuggestions, Goals = @Goals,
                    Achievements = @Achievements, Challenges = @Challenges, DevelopmentPlan = @DevelopmentPlan,
                    ApprovalStatus = @ApprovalStatus, ApprovedBy = @ApprovedBy, ApprovedAt = @ApprovedAt,
                    EvaluationDate = @EvaluationDate, SubmittedAt = @SubmittedAt, Remarks = @Remarks,
                    IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                WHERE PerformanceId = @PerformanceId";
        }
        
        public async Task<IEnumerable<PerformanceScore>> GetByEmployeeIdAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM PerformanceScores 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY EvaluationYear DESC, EvaluationMonth DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PerformanceScore>(sql, new { EmployeeId = employeeId });
        }
        
        public async Task<PerformanceScore> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT * FROM PerformanceScores 
                WHERE EmployeeId = @EmployeeId AND EvaluationYear = @Year AND EvaluationMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<PerformanceScore>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
        }
        
        public async Task<IEnumerable<PerformanceScore>> GetByPeriodAsync(int year, int month)
        {
            const string sql = @"
                SELECT * FROM PerformanceScores 
                WHERE EvaluationYear = @Year AND EvaluationMonth = @Month AND IsActive = 1
                ORDER BY EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PerformanceScore>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<PerformanceScore>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM PerformanceScores 
                WHERE EvaluationDate >= @StartDate AND EvaluationDate <= @EndDate AND IsActive = 1
                ORDER BY EvaluationDate DESC, EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PerformanceScore>(sql, new { StartDate = startDate, EndDate = endDate });
        }
        
        public async Task<IEnumerable<DepartmentPerformanceStatistics>> GetDepartmentStatisticsAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    e.Department,
                    COUNT(*) as TotalCount,
                    AVG(p.OverallScore) as AverageScore,
                    MIN(p.OverallScore) as MinScore,
                    MAX(p.OverallScore) as MaxScore,
                    SUM(CASE WHEN p.PerformanceLevel = '优秀' THEN 1 ELSE 0 END) as ExcellentCount,
                    SUM(CASE WHEN p.PerformanceLevel = '良好' THEN 1 ELSE 0 END) as GoodCount,
                    SUM(CASE WHEN p.PerformanceLevel = '合格' THEN 1 ELSE 0 END) as QualifiedCount,
                    SUM(CASE WHEN p.PerformanceLevel = '待改进' THEN 1 ELSE 0 END) as NeedsImprovementCount
                FROM PerformanceScores p
                INNER JOIN Employees e ON p.EmployeeId = e.EmployeeId
                WHERE p.EvaluationYear = @Year AND p.EvaluationMonth = @Month AND p.IsActive = 1
                GROUP BY e.Department
                ORDER BY e.Department";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<DepartmentPerformanceStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<PerformanceScore>> GetEmployeePerformanceTrendAsync(int employeeId, int months = 12)
        {
            const string sql = @"
                SELECT * FROM PerformanceScores 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY EvaluationYear DESC, EvaluationMonth DESC
                LIMIT @Months";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PerformanceScore>(sql, new { EmployeeId = employeeId, Months = months });
        }
        
        public async Task<IEnumerable<PerformanceScore>> GetPendingApprovalAsync()
        {
            const string sql = @"
                SELECT p.*, e.EmployeeName, e.Department, e.Position
                FROM PerformanceScores p
                INNER JOIN Employees e ON p.EmployeeId = e.EmployeeId
                WHERE p.ApprovalStatus = '待审批' AND p.IsActive = 1
                ORDER BY p.SubmittedAt";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PerformanceScore>(sql);
        }
        
        public async Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> performanceIds, string approvalStatus, string approvedBy)
        {
            const string sql = @"
                UPDATE PerformanceScores 
                SET ApprovalStatus = @ApprovalStatus, ApprovedBy = @ApprovedBy, ApprovedAt = @ApprovedAt, UpdatedAt = @UpdatedAt
                WHERE PerformanceId IN @PerformanceIds";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, new 
            { 
                ApprovalStatus = approvalStatus, 
                ApprovedBy = approvedBy, 
                ApprovedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PerformanceIds = performanceIds 
            });
        }
        
        public async Task<bool> ExistsForEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT COUNT(*) FROM PerformanceScores 
                WHERE EmployeeId = @EmployeeId AND EvaluationYear = @Year AND EvaluationMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
            return count > 0;
        }
        
        public async Task<IEnumerable<PerformanceDistribution>> GetPerformanceDistributionAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    PerformanceLevel,
                    COUNT(*) as Count,
                    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM PerformanceScores WHERE EvaluationYear = @Year AND EvaluationMonth = @Month AND IsActive = 1), 2) as Percentage
                FROM PerformanceScores 
                WHERE EvaluationYear = @Year AND EvaluationMonth = @Month AND IsActive = 1
                GROUP BY PerformanceLevel
                ORDER BY 
                    CASE PerformanceLevel 
                        WHEN '优秀' THEN 1 
                        WHEN '良好' THEN 2 
                        WHEN '合格' THEN 3 
                        WHEN '待改进' THEN 4 
                        ELSE 5 
                    END";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PerformanceDistribution>(sql, new { Year = year, Month = month });
        }
    }
    
    /// <summary>
    /// 部门绩效统计
    /// </summary>
    public class DepartmentPerformanceStatistics
    {
        public string Department { get; set; }
        public int TotalCount { get; set; }
        public decimal AverageScore { get; set; }
        public decimal MinScore { get; set; }
        public decimal MaxScore { get; set; }
        public int ExcellentCount { get; set; }
        public int GoodCount { get; set; }
        public int QualifiedCount { get; set; }
        public int NeedsImprovementCount { get; set; }
    }
    
    /// <summary>
    /// 绩效分布统计
    /// </summary>
    public class PerformanceDistribution
    {
        public string PerformanceLevel { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }
}