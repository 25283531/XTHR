using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 其他奖惩信息实体类
    /// </summary>
    public class OtherCompensationPenalty
    {
        /// <summary>
        /// 奖惩信息ID（主键）
        /// </summary>
        public int CompensationPenaltyID { get; set; }

        /// <summary>
        /// 员工ID（外键）
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 奖惩类型
        /// </summary>
        [Required(ErrorMessage = "奖惩类型不能为空")]
        [StringLength(20, ErrorMessage = "奖惩类型长度不能超过20个字符")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 奖惩项目名称
        /// </summary>
        [Required(ErrorMessage = "奖惩项目名称不能为空")]
        [StringLength(100, ErrorMessage = "奖惩项目名称长度不能超过100个字符")]
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// 奖惩金额
        /// </summary>
        [Required(ErrorMessage = "奖惩金额不能为空")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 奖惩原因
        /// </summary>
        [Required(ErrorMessage = "奖惩原因不能为空")]
        [StringLength(500, ErrorMessage = "奖惩原因长度不能超过500个字符")]
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 发生日期
        /// </summary>
        [Required(ErrorMessage = "发生日期不能为空")]
        public DateTime OccurrenceDate { get; set; }

        /// <summary>
        /// 生效年份
        /// </summary>
        [Required(ErrorMessage = "生效年份不能为空")]
        [Range(2000, 3000, ErrorMessage = "生效年份必须在2000-3000之间")]
        public int EffectiveYear { get; set; }

        /// <summary>
        /// 生效月份
        /// </summary>
        [Required(ErrorMessage = "生效月份不能为空")]
        [Range(1, 12, ErrorMessage = "生效月份必须在1-12之间")]
        public int EffectiveMonth { get; set; }

        /// <summary>
        /// 是否一次性
        /// </summary>
        public bool IsOneTime { get; set; } = true;

        /// <summary>
        /// 分摊月数（非一次性时使用）
        /// </summary>
        [Range(1, 60, ErrorMessage = "分摊月数必须在1-60之间")]
        public int? InstallmentMonths { get; set; }

        /// <summary>
        /// 每月分摊金额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "每月分摊金额不能为负数")]
        public decimal? MonthlyAmount { get; set; }

        /// <summary>
        /// 已分摊月数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "已分摊月数不能为负数")]
        public int ProcessedMonths { get; set; } = 0;

        /// <summary>
        /// 剩余分摊金额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "剩余分摊金额不能为负数")]
        public decimal RemainingAmount { get; set; } = 0;

        /// <summary>
        /// 优先级（数字越小优先级越高）
        /// </summary>
        [Range(1, 100, ErrorMessage = "优先级必须在1-100之间")]
        public int Priority { get; set; } = 50;

        /// <summary>
        /// 是否影响个税计算
        /// </summary>
        public bool AffectsTax { get; set; } = true;

        /// <summary>
        /// 是否影响社保计算
        /// </summary>
        public bool AffectsSocialSecurity { get; set; } = false;

        /// <summary>
        /// 审批状态
        /// </summary>
        [Required(ErrorMessage = "审批状态不能为空")]
        [StringLength(20, ErrorMessage = "审批状态长度不能超过20个字符")]
        public string ApprovalStatus { get; set; } = "待审批";

        /// <summary>
        /// 申请人
        /// </summary>
        [StringLength(50, ErrorMessage = "申请人长度不能超过50个字符")]
        public string? Applicant { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime? ApplicationDate { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        [StringLength(50, ErrorMessage = "审批人长度不能超过50个字符")]
        public string? Approver { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [StringLength(500, ErrorMessage = "审批意见长度不能超过500个字符")]
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        [StringLength(20, ErrorMessage = "执行状态长度不能超过20个字符")]
        public string ExecutionStatus { get; set; } = "未执行";

        /// <summary>
        /// 执行人
        /// </summary>
        [StringLength(50, ErrorMessage = "执行人长度不能超过50个字符")]
        public string? Executor { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime? ExecutionDate { get; set; }

        /// <summary>
        /// 相关文档路径
        /// </summary>
        [StringLength(500, ErrorMessage = "相关文档路径长度不能超过500个字符")]
        public string? DocumentPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000, ErrorMessage = "备注长度不能超过1000个字符")]
        public string? Remarks { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; } = true;

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
        /// 生效期间（年月字符串表示）
        /// </summary>
        public string EffectivePeriodString => $"{EffectiveYear:0000}-{EffectiveMonth:00}";

        /// <summary>
        /// 生效期间开始日期
        /// </summary>
        public DateTime EffectivePeriodStartDate => new DateTime(EffectiveYear, EffectiveMonth, 1);

        /// <summary>
        /// 生效期间结束日期
        /// </summary>
        public DateTime EffectivePeriodEndDate => EffectivePeriodStartDate.AddMonths(1).AddDays(-1);

        /// <summary>
        /// 是否为奖励
        /// </summary>
        public bool IsReward => Type == "奖励" || Type == "补贴" || Type == "津贴" || Amount > 0;

        /// <summary>
        /// 是否为惩罚
        /// </summary>
        public bool IsPenalty => Type == "扣款" || Type == "罚款" || Type == "惩罚" || Amount < 0;

        /// <summary>
        /// 是否已审批通过
        /// </summary>
        public bool IsApproved => ApprovalStatus == "已审批" || ApprovalStatus == "通过";

        /// <summary>
        /// 是否已拒绝
        /// </summary>
        public bool IsRejected => ApprovalStatus == "已拒绝" || ApprovalStatus == "拒绝";

        /// <summary>
        /// 是否已执行
        /// </summary>
        public bool IsExecuted => ExecutionStatus == "已执行" || ExecutionStatus == "完成";

        /// <summary>
        /// 是否已完成（已审批且已执行）
        /// </summary>
        public bool IsCompleted => IsApproved && IsExecuted;

        /// <summary>
        /// 是否可以执行
        /// </summary>
        public bool CanExecute => IsApproved && !IsExecuted && IsActive;

        /// <summary>
        /// 是否分摊完毕
        /// </summary>
        public bool IsInstallmentCompleted
        {
            get
            {
                if (IsOneTime)
                    return true;
                    
                return InstallmentMonths.HasValue && ProcessedMonths >= InstallmentMonths.Value;
            }
        }

        /// <summary>
        /// 获取指定期间的应发金额
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>应发金额</returns>
        public decimal GetAmountForPeriod(int year, int month)
        {
            // 检查是否在生效期间
            if (year < EffectiveYear || (year == EffectiveYear && month < EffectiveMonth))
                return 0;

            // 检查是否已审批通过且有效
            if (!IsApproved || !IsActive)
                return 0;

            // 一次性发放
            if (IsOneTime)
            {
                if (year == EffectiveYear && month == EffectiveMonth)
                    return Amount;
                else
                    return 0;
            }

            // 分期发放
            if (!InstallmentMonths.HasValue || !MonthlyAmount.HasValue)
                return 0;

            // 计算当前期间是第几个月
            var effectiveDate = new DateTime(EffectiveYear, EffectiveMonth, 1);
            var currentDate = new DateTime(year, month, 1);
            var monthsDiff = ((currentDate.Year - effectiveDate.Year) * 12) + currentDate.Month - effectiveDate.Month;

            // 检查是否在分摊期间内
            if (monthsDiff < 0 || monthsDiff >= InstallmentMonths.Value)
                return 0;

            return MonthlyAmount.Value;
        }

        /// <summary>
        /// 计算分摊信息
        /// </summary>
        public void CalculateInstallment()
        {
            if (IsOneTime)
            {
                MonthlyAmount = Amount;
                InstallmentMonths = 1;
                RemainingAmount = IsExecuted ? 0 : Amount;
            }
            else if (InstallmentMonths.HasValue && InstallmentMonths.Value > 0)
            {
                MonthlyAmount = Math.Round(Amount / InstallmentMonths.Value, 2);
                RemainingAmount = Amount - (ProcessedMonths * MonthlyAmount.Value);
                
                // 处理最后一个月的余额
                if (ProcessedMonths == InstallmentMonths.Value - 1 && RemainingAmount != MonthlyAmount.Value)
                {
                    MonthlyAmount = RemainingAmount;
                }
            }
        }

        /// <summary>
        /// 更新执行进度
        /// </summary>
        /// <param name="year">执行年份</param>
        /// <param name="month">执行月份</param>
        /// <param name="executedAmount">执行金额</param>
        /// <param name="executor">执行人</param>
        public void UpdateExecutionProgress(int year, int month, decimal executedAmount, string? executor = null)
        {
            if (!IsOneTime)
            {
                ProcessedMonths++;
                RemainingAmount -= executedAmount;
                
                if (RemainingAmount <= 0)
                {
                    RemainingAmount = 0;
                    ExecutionStatus = "已执行";
                    ExecutionDate = DateTime.Now;
                    Executor = executor;
                }
            }
            else if (Math.Abs(executedAmount - Amount) < 0.01m)
            {
                ExecutionStatus = "已执行";
                ExecutionDate = DateTime.Now;
                Executor = executor;
                RemainingAmount = 0;
            }
            
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 验证奖惩信息
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateCompensationPenalty()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (EmployeeID <= 0)
                result.AddError("员工ID必须大于0");

            if (string.IsNullOrWhiteSpace(Type))
                result.AddError("奖惩类型不能为空");

            if (string.IsNullOrWhiteSpace(ItemName))
                result.AddError("奖惩项目名称不能为空");

            if (string.IsNullOrWhiteSpace(Reason))
                result.AddError("奖惩原因不能为空");

            if (OccurrenceDate == default)
                result.AddError("发生日期不能为空");

            if (EffectiveYear < 2000 || EffectiveYear > 3000)
                result.AddError("生效年份必须在2000-3000之间");

            if (EffectiveMonth < 1 || EffectiveMonth > 12)
                result.AddError("生效月份必须在1-12之间");

            // 检查金额逻辑
            if (Amount == 0)
                result.AddError("奖惩金额不能为0");

            // 检查类型与金额的一致性
            if ((Type == "奖励" || Type == "补贴" || Type == "津贴") && Amount < 0)
                result.AddError("奖励类型的金额应为正数");

            if ((Type == "扣款" || Type == "罚款" || Type == "惩罚") && Amount > 0)
                result.AddError("扣款类型的金额应为负数");

            // 检查分期逻辑
            if (!IsOneTime)
            {
                if (!InstallmentMonths.HasValue || InstallmentMonths.Value <= 0)
                    result.AddError("非一次性发放必须设置分摊月数");
                    
                if (InstallmentMonths.HasValue && InstallmentMonths.Value > 60)
                    result.AddWarning("分摊月数超过60个月，请确认是否正确");
            }

            // 检查日期逻辑
            if (OccurrenceDate > DateTime.Today)
                result.AddError("发生日期不能晚于今天");

            var effectiveDate = new DateTime(EffectiveYear, EffectiveMonth, 1);
            if (effectiveDate < OccurrenceDate.Date)
                result.AddWarning("生效日期早于发生日期，请确认是否正确");

            // 检查审批逻辑
            if (IsApproved && !ApprovalDate.HasValue)
                result.AddError("已审批的记录必须有审批日期");

            if (IsExecuted && !ExecutionDate.HasValue)
                result.AddError("已执行的记录必须有执行日期");

            if (ApprovalDate.HasValue && ApplicationDate.HasValue && ApprovalDate.Value < ApplicationDate.Value)
                result.AddError("审批日期不能早于申请日期");

            if (ExecutionDate.HasValue && ApprovalDate.HasValue && ExecutionDate.Value < ApprovalDate.Value)
                result.AddError("执行日期不能早于审批日期");

            // 检查金额合理性
            if (Math.Abs(Amount) > 1000000)
                result.AddWarning("奖惩金额超过100万，请确认是否正确");

            return result;
        }

        /// <summary>
        /// 克隆奖惩信息对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public OtherCompensationPenalty Clone()
        {
            return new OtherCompensationPenalty
            {
                CompensationPenaltyID = this.CompensationPenaltyID,
                EmployeeID = this.EmployeeID,
                Type = this.Type,
                ItemName = this.ItemName,
                Amount = this.Amount,
                Reason = this.Reason,
                OccurrenceDate = this.OccurrenceDate,
                EffectiveYear = this.EffectiveYear,
                EffectiveMonth = this.EffectiveMonth,
                IsOneTime = this.IsOneTime,
                InstallmentMonths = this.InstallmentMonths,
                MonthlyAmount = this.MonthlyAmount,
                ProcessedMonths = this.ProcessedMonths,
                RemainingAmount = this.RemainingAmount,
                Priority = this.Priority,
                AffectsTax = this.AffectsTax,
                AffectsSocialSecurity = this.AffectsSocialSecurity,
                ApprovalStatus = this.ApprovalStatus,
                Applicant = this.Applicant,
                ApplicationDate = this.ApplicationDate,
                Approver = this.Approver,
                ApprovalDate = this.ApprovalDate,
                ApprovalComments = this.ApprovalComments,
                ExecutionStatus = this.ExecutionStatus,
                Executor = this.Executor,
                ExecutionDate = this.ExecutionDate,
                DocumentPath = this.DocumentPath,
                Remarks = this.Remarks,
                IsActive = this.IsActive,
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
            return $"CompensationPenalty[{EmployeeID}]: {Type}-{ItemName} {Amount:C} - {EffectivePeriodString} - {ApprovalStatus}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is OtherCompensationPenalty other)
            {
                return CompensationPenaltyID == other.CompensationPenaltyID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return CompensationPenaltyID.GetHashCode();
        }
    }
}