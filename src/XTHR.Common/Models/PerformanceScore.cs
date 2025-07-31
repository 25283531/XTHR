using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 绩效考核结果实体类
    /// </summary>
    public class PerformanceScore
    {
        /// <summary>
        /// 绩效考核ID（主键）
        /// </summary>
        public int PerformanceID { get; set; }

        /// <summary>
        /// 员工ID（外键）
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 考核年份
        /// </summary>
        [Required(ErrorMessage = "考核年份不能为空")]
        [Range(2000, 3000, ErrorMessage = "考核年份必须在2000-3000之间")]
        public int Year { get; set; }

        /// <summary>
        /// 考核月份
        /// </summary>
        [Required(ErrorMessage = "考核月份不能为空")]
        [Range(1, 12, ErrorMessage = "考核月份必须在1-12之间")]
        public int Month { get; set; }

        /// <summary>
        /// 绩效等级
        /// </summary>
        [Required(ErrorMessage = "绩效等级不能为空")]
        [StringLength(10, ErrorMessage = "绩效等级长度不能超过10个字符")]
        public string PerformanceGrade { get; set; } = string.Empty;

        /// <summary>
        /// 绩效分数
        /// </summary>
        [Required(ErrorMessage = "绩效分数不能为空")]
        [Range(0, 100, ErrorMessage = "绩效分数必须在0-100之间")]
        public decimal Score { get; set; }

        /// <summary>
        /// 绩效系数
        /// </summary>
        [Required(ErrorMessage = "绩效系数不能为空")]
        [Range(0, 5, ErrorMessage = "绩效系数必须在0-5之间")]
        public decimal PerformanceCoefficient { get; set; }

        /// <summary>
        /// 绩效工资基数
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "绩效工资基数不能为负数")]
        public decimal PerformanceBase { get; set; } = 0;

        /// <summary>
        /// 计算得出的绩效工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "绩效工资不能为负数")]
        public decimal PerformanceSalary { get; set; } = 0;

        /// <summary>
        /// 目标完成率（%）
        /// </summary>
        [Range(0, 1000, ErrorMessage = "目标完成率必须在0-1000%之间")]
        public decimal? TargetCompletionRate { get; set; }

        /// <summary>
        /// KPI得分
        /// </summary>
        [Range(0, 100, ErrorMessage = "KPI得分必须在0-100之间")]
        public decimal? KPIScore { get; set; }

        /// <summary>
        /// 能力素质得分
        /// </summary>
        [Range(0, 100, ErrorMessage = "能力素质得分必须在0-100之间")]
        public decimal? CompetencyScore { get; set; }

        /// <summary>
        /// 行为表现得分
        /// </summary>
        [Range(0, 100, ErrorMessage = "行为表现得分必须在0-100之间")]
        public decimal? BehaviorScore { get; set; }

        /// <summary>
        /// 创新贡献得分
        /// </summary>
        [Range(0, 100, ErrorMessage = "创新贡献得分必须在0-100之间")]
        public decimal? InnovationScore { get; set; }

        /// <summary>
        /// 团队协作得分
        /// </summary>
        [Range(0, 100, ErrorMessage = "团队协作得分必须在0-100之间")]
        public decimal? TeamworkScore { get; set; }

        /// <summary>
        /// 考核状态
        /// </summary>
        [Required(ErrorMessage = "考核状态不能为空")]
        [StringLength(20, ErrorMessage = "考核状态长度不能超过20个字符")]
        public string Status { get; set; } = "待考核";

        /// <summary>
        /// 考核人
        /// </summary>
        [StringLength(50, ErrorMessage = "考核人长度不能超过50个字符")]
        public string? Evaluator { get; set; }

        /// <summary>
        /// 考核日期
        /// </summary>
        public DateTime? EvaluationDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [StringLength(50, ErrorMessage = "审核人长度不能超过50个字符")]
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 考核备注
        /// </summary>
        [StringLength(1000, ErrorMessage = "考核备注长度不能超过1000个字符")]
        public string? EvaluationRemarks { get; set; }

        /// <summary>
        /// 改进建议
        /// </summary>
        [StringLength(1000, ErrorMessage = "改进建议长度不能超过1000个字符")]
        public string? ImprovementSuggestions { get; set; }

        /// <summary>
        /// 是否已确认
        /// </summary>
        public bool IsConfirmed { get; set; } = false;

        /// <summary>
        /// 员工确认日期
        /// </summary>
        public DateTime? EmployeeConfirmDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        [StringLength(50, ErrorMessage = "创建人长度不能超过50个字符")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [StringLength(50, ErrorMessage = "更新人长度不能超过50个字符")]
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// 导航属性：员工信息
        /// </summary>
        public Employee? Employee { get; set; }

        /// <summary>
        /// 考核期间（年月字符串表示）
        /// </summary>
        public string PeriodString => $"{Year:0000}-{Month:00}";

        /// <summary>
        /// 考核期间开始日期
        /// </summary>
        public DateTime PeriodStartDate => new DateTime(Year, Month, 1);

        /// <summary>
        /// 考核期间结束日期
        /// </summary>
        public DateTime PeriodEndDate => PeriodStartDate.AddMonths(1).AddDays(-1);

        /// <summary>
        /// 是否已完成考核
        /// </summary>
        public bool IsCompleted => Status == "已完成" || Status == "已审核";

        /// <summary>
        /// 是否已审核
        /// </summary>
        public bool IsReviewed => Status == "已审核";

        /// <summary>
        /// 综合得分（各项得分的加权平均）
        /// </summary>
        public decimal? ComprehensiveScore
        {
            get
            {
                var scores = new List<decimal?> { KPIScore, CompetencyScore, BehaviorScore, InnovationScore, TeamworkScore };
                var validScores = scores.Where(s => s.HasValue).Select(s => s!.Value).ToList();
                
                if (!validScores.Any())
                    return null;
                    
                return validScores.Average();
            }
        }

        /// <summary>
        /// 计算绩效工资
        /// </summary>
        /// <param name="baseAmount">绩效工资基数，如果不提供则使用对象的PerformanceBase</param>
        /// <returns>计算得出的绩效工资</returns>
        public decimal CalculatePerformanceSalary(decimal? baseAmount = null)
        {
            var baseValue = baseAmount ?? PerformanceBase;
            var calculatedSalary = baseValue * PerformanceCoefficient;
            
            // 更新对象的绩效工资字段
            PerformanceSalary = calculatedSalary;
            
            return calculatedSalary;
        }

        /// <summary>
        /// 根据绩效分数获取绩效等级
        /// </summary>
        /// <param name="score">绩效分数</param>
        /// <returns>绩效等级</returns>
        public static string GetGradeByScore(decimal score)
        {
            return score switch
            {
                >= 95 => "S",
                >= 90 => "A+",
                >= 85 => "A",
                >= 80 => "A-",
                >= 75 => "B+",
                >= 70 => "B",
                >= 65 => "B-",
                >= 60 => "C+",
                >= 55 => "C",
                >= 50 => "C-",
                _ => "D"
            };
        }

        /// <summary>
        /// 根据绩效等级获取绩效系数
        /// </summary>
        /// <param name="grade">绩效等级</param>
        /// <returns>绩效系数</returns>
        public static decimal GetCoefficientByGrade(string grade)
        {
            return grade?.ToUpper() switch
            {
                "S" => 1.5m,
                "A+" => 1.3m,
                "A" => 1.2m,
                "A-" => 1.1m,
                "B+" => 1.0m,
                "B" => 0.9m,
                "B-" => 0.8m,
                "C+" => 0.7m,
                "C" => 0.6m,
                "C-" => 0.5m,
                "D" => 0.3m,
                _ => 1.0m
            };
        }

        /// <summary>
        /// 验证绩效考核信息
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidatePerformance()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (EmployeeID <= 0)
                result.AddError("员工ID必须大于0");

            if (Year < 2000 || Year > 3000)
                result.AddError("考核年份必须在2000-3000之间");

            if (Month < 1 || Month > 12)
                result.AddError("考核月份必须在1-12之间");

            if (string.IsNullOrWhiteSpace(PerformanceGrade))
                result.AddError("绩效等级不能为空");

            if (Score < 0 || Score > 100)
                result.AddError("绩效分数必须在0-100之间");

            if (PerformanceCoefficient < 0 || PerformanceCoefficient > 5)
                result.AddError("绩效系数必须在0-5之间");

            // 检查考核期间是否合理
            var periodDate = new DateTime(Year, Month, 1);
            if (periodDate > DateTime.Today)
                result.AddError("考核期间不能晚于当前月份");

            // 检查绩效等级与分数的一致性
            var expectedGrade = GetGradeByScore(Score);
            if (!string.Equals(PerformanceGrade, expectedGrade, StringComparison.OrdinalIgnoreCase))
                result.AddWarning($"绩效等级({PerformanceGrade})与分数({Score})不匹配，建议等级为{expectedGrade}");

            // 检查绩效系数与等级的一致性
            var expectedCoefficient = GetCoefficientByGrade(PerformanceGrade);
            if (Math.Abs(PerformanceCoefficient - expectedCoefficient) > 0.1m)
                result.AddWarning($"绩效系数({PerformanceCoefficient})与等级({PerformanceGrade})不匹配，建议系数为{expectedCoefficient}");

            // 检查状态逻辑
            if (IsCompleted && !EvaluationDate.HasValue)
                result.AddError("已完成的考核必须有考核日期");

            if (IsReviewed && !ReviewDate.HasValue)
                result.AddError("已审核的考核必须有审核日期");

            if (IsConfirmed && !EmployeeConfirmDate.HasValue)
                result.AddError("已确认的考核必须有员工确认日期");

            // 检查日期逻辑
            if (EvaluationDate.HasValue && ReviewDate.HasValue && ReviewDate.Value < EvaluationDate.Value)
                result.AddError("审核日期不能早于考核日期");

            return result;
        }

        /// <summary>
        /// 自动设置绩效等级和系数
        /// </summary>
        public void AutoSetGradeAndCoefficient()
        {
            PerformanceGrade = GetGradeByScore(Score);
            PerformanceCoefficient = GetCoefficientByGrade(PerformanceGrade);
        }

        /// <summary>
        /// 克隆绩效考核对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public PerformanceScore Clone()
        {
            return new PerformanceScore
            {
                PerformanceID = this.PerformanceID,
                EmployeeID = this.EmployeeID,
                Year = this.Year,
                Month = this.Month,
                PerformanceGrade = this.PerformanceGrade,
                Score = this.Score,
                PerformanceCoefficient = this.PerformanceCoefficient,
                PerformanceBase = this.PerformanceBase,
                PerformanceSalary = this.PerformanceSalary,
                TargetCompletionRate = this.TargetCompletionRate,
                KPIScore = this.KPIScore,
                CompetencyScore = this.CompetencyScore,
                BehaviorScore = this.BehaviorScore,
                InnovationScore = this.InnovationScore,
                TeamworkScore = this.TeamworkScore,
                Status = this.Status,
                Evaluator = this.Evaluator,
                EvaluationDate = this.EvaluationDate,
                Reviewer = this.Reviewer,
                ReviewDate = this.ReviewDate,
                EvaluationRemarks = this.EvaluationRemarks,
                ImprovementSuggestions = this.ImprovementSuggestions,
                IsConfirmed = this.IsConfirmed,
                EmployeeConfirmDate = this.EmployeeConfirmDate,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy
            };
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"Performance[{EmployeeID}]: {PeriodString} - {PerformanceGrade}({Score}分) - {Status}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is PerformanceScore other)
            {
                return PerformanceID == other.PerformanceID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return PerformanceID.GetHashCode();
        }
    }
}