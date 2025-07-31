using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 工资核算结果实体类
    /// </summary>
    public class PayrollResult
    {
        /// <summary>
        /// 工资核算ID（主键）
        /// </summary>
        public int PayrollID { get; set; }

        /// <summary>
        /// 员工ID（外键）
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 工资年份
        /// </summary>
        [Required(ErrorMessage = "工资年份不能为空")]
        [Range(2000, 3000, ErrorMessage = "工资年份必须在2000-3000之间")]
        public int Year { get; set; }

        /// <summary>
        /// 工资月份
        /// </summary>
        [Required(ErrorMessage = "工资月份不能为空")]
        [Range(1, 12, ErrorMessage = "工资月份必须在1-12之间")]
        public int Month { get; set; }

        /// <summary>
        /// 基础工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "基础工资不能为负数")]
        public decimal BaseSalary { get; set; } = 0;

        /// <summary>
        /// 岗位工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "岗位工资不能为负数")]
        public decimal PositionSalary { get; set; } = 0;

        /// <summary>
        /// 技能工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "技能工资不能为负数")]
        public decimal SkillSalary { get; set; } = 0;

        /// <summary>
        /// 工龄工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "工龄工资不能为负数")]
        public decimal SeniorityPay { get; set; } = 0;

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; } = 0;

        /// <summary>
        /// 津贴补助
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "津贴补助不能为负数")]
        public decimal Allowance { get; set; } = 0;

        /// <summary>
        /// 交通补助
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "交通补助不能为负数")]
        public decimal TransportAllowance { get; set; } = 0;

        /// <summary>
        /// 餐费补助
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "餐费补助不能为负数")]
        public decimal MealAllowance { get; set; } = 0;

        /// <summary>
        /// 通讯补助
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "通讯补助不能为负数")]
        public decimal CommunicationAllowance { get; set; } = 0;

        /// <summary>
        /// 住房补助
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "住房补助不能为负数")]
        public decimal HousingAllowance { get; set; } = 0;

        /// <summary>
        /// 其他固定补助
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "其他固定补助不能为负数")]
        public decimal OtherFixedAllowance { get; set; } = 0;

        /// <summary>
        /// 平时加班费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "平时加班费不能为负数")]
        public decimal OvertimePay { get; set; } = 0;

        /// <summary>
        /// 周六加班费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "周六加班费不能为负数")]
        public decimal SaturdayOvertimePay { get; set; } = 0;

        /// <summary>
        /// 周日加班费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "周日加班费不能为负数")]
        public decimal SundayOvertimePay { get; set; } = 0;

        /// <summary>
        /// 法定节假日加班费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "法定节假日加班费不能为负数")]
        public decimal HolidayOvertimePay { get; set; } = 0;

        /// <summary>
        /// 竞业补偿
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "竞业补偿不能为负数")]
        public decimal NonCompeteCompensation { get; set; } = 0;

        /// <summary>
        /// 全勤奖
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "全勤奖不能为负数")]
        public decimal PerfectAttendanceBonus { get; set; } = 0;

        /// <summary>
        /// 其他奖励
        /// </summary>
        public decimal OtherBonus { get; set; } = 0;

        /// <summary>
        /// 其他扣款
        /// </summary>
        public decimal OtherDeduction { get; set; } = 0;

        /// <summary>
        /// 迟到扣款
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "迟到扣款不能为负数")]
        public decimal LateDeduction { get; set; } = 0;

        /// <summary>
        /// 早退扣款
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "早退扣款不能为负数")]
        public decimal EarlyLeaveDeduction { get; set; } = 0;

        /// <summary>
        /// 缺勤扣款
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "缺勤扣款不能为负数")]
        public decimal AbsentDeduction { get; set; } = 0;

        /// <summary>
        /// 事假扣款
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "事假扣款不能为负数")]
        public decimal PersonalLeaveDeduction { get; set; } = 0;

        /// <summary>
        /// 病假扣款
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "病假扣款不能为负数")]
        public decimal SickLeaveDeduction { get; set; } = 0;

        /// <summary>
        /// 应发工资总额
        /// </summary>
        public decimal GrossSalary { get; set; } = 0;

        /// <summary>
        /// 个人所得税
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "个人所得税不能为负数")]
        public decimal IncomeTax { get; set; } = 0;

        /// <summary>
        /// 社保个人缴费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "社保个人缴费不能为负数")]
        public decimal SocialSecurityPersonal { get; set; } = 0;

        /// <summary>
        /// 公积金个人缴费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "公积金个人缴费不能为负数")]
        public decimal HousingFundPersonal { get; set; } = 0;

        /// <summary>
        /// 社保公司缴费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "社保公司缴费不能为负数")]
        public decimal SocialSecurityCompany { get; set; } = 0;

        /// <summary>
        /// 公积金公司缴费
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "公积金公司缴费不能为负数")]
        public decimal HousingFundCompany { get; set; } = 0;

        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetSalary { get; set; } = 0;

        /// <summary>
        /// 公司总成本
        /// </summary>
        public decimal TotalCost { get; set; } = 0;

        /// <summary>
        /// 应出勤天数
        /// </summary>
        [Range(0, 31, ErrorMessage = "应出勤天数必须在0-31之间")]
        public int ExpectedWorkDays { get; set; } = 0;

        /// <summary>
        /// 实际出勤天数
        /// </summary>
        [Range(0, 31, ErrorMessage = "实际出勤天数必须在0-31之间")]
        public int ActualWorkDays { get; set; } = 0;

        /// <summary>
        /// 请假天数
        /// </summary>
        [Range(0, 31, ErrorMessage = "请假天数必须在0-31之间")]
        public decimal LeaveDays { get; set; } = 0;

        /// <summary>
        /// 迟到次数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "迟到次数不能为负数")]
        public int LateCount { get; set; } = 0;

        /// <summary>
        /// 早退次数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "早退次数不能为负数")]
        public int EarlyLeaveCount { get; set; } = 0;

        /// <summary>
        /// 总加班小时数
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "总加班小时数不能为负数")]
        public decimal TotalOvertimeHours { get; set; } = 0;

        /// <summary>
        /// 是否全勤
        /// </summary>
        public bool IsPerfectAttendance { get; set; } = false;

        /// <summary>
        /// 核算状态
        /// </summary>
        [Required(ErrorMessage = "核算状态不能为空")]
        [StringLength(20, ErrorMessage = "核算状态长度不能超过20个字符")]
        public string CalculationStatus { get; set; } = "草稿";

        /// <summary>
        /// 审核状态
        /// </summary>
        [StringLength(20, ErrorMessage = "审核状态长度不能超过20个字符")]
        public string ApprovalStatus { get; set; } = "待审核";

        /// <summary>
        /// 发放状态
        /// </summary>
        [StringLength(20, ErrorMessage = "发放状态长度不能超过20个字符")]
        public string PaymentStatus { get; set; } = "未发放";

        /// <summary>
        /// 核算人
        /// </summary>
        [StringLength(50, ErrorMessage = "核算人长度不能超过50个字符")]
        public string? Calculator { get; set; }

        /// <summary>
        /// 核算日期
        /// </summary>
        public DateTime? CalculationDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [StringLength(50, ErrorMessage = "审核人长度不能超过50个字符")]
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 发放人
        /// </summary>
        [StringLength(50, ErrorMessage = "发放人长度不能超过50个字符")]
        public string? Payer { get; set; }

        /// <summary>
        /// 发放日期
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1000, ErrorMessage = "备注长度不能超过1000个字符")]
        public string? Remarks { get; set; }

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
        /// 工资期间（年月字符串表示）
        /// </summary>
        public string PeriodString => $"{Year:0000}-{Month:00}";

        /// <summary>
        /// 工资期间开始日期
        /// </summary>
        public DateTime PeriodStartDate => new DateTime(Year, Month, 1);

        /// <summary>
        /// 工资期间结束日期
        /// </summary>
        public DateTime PeriodEndDate => PeriodStartDate.AddMonths(1).AddDays(-1);

        /// <summary>
        /// 基本工资总额（基础+岗位+技能+工龄）
        /// </summary>
        public decimal BasicSalaryTotal
        {
            get
            {
                return BaseSalary + PositionSalary + SkillSalary + SeniorityPay;
            }
        }

        /// <summary>
        /// 补助总额
        /// </summary>
        public decimal AllowanceTotal
        {
            get
            {
                return Allowance + TransportAllowance + MealAllowance + 
                       CommunicationAllowance + HousingAllowance + OtherFixedAllowance;
            }
        }

        /// <summary>
        /// 加班费总额
        /// </summary>
        public decimal OvertimePayTotal
        {
            get
            {
                return OvertimePay + SaturdayOvertimePay + SundayOvertimePay + HolidayOvertimePay;
            }
        }

        /// <summary>
        /// 奖励总额
        /// </summary>
        public decimal BonusTotal
        {
            get
            {
                return PerfectAttendanceBonus + OtherBonus;
            }
        }

        /// <summary>
        /// 扣款总额
        /// </summary>
        public decimal DeductionTotal
        {
            get
            {
                return LateDeduction + EarlyLeaveDeduction + AbsentDeduction + 
                       PersonalLeaveDeduction + SickLeaveDeduction + OtherDeduction;
            }
        }

        /// <summary>
        /// 个人缴费总额（社保+公积金）
        /// </summary>
        public decimal PersonalContributionTotal
        {
            get
            {
                return SocialSecurityPersonal + HousingFundPersonal;
            }
        }

        /// <summary>
        /// 公司缴费总额（社保+公积金）
        /// </summary>
        public decimal CompanyContributionTotal
        {
            get
            {
                return SocialSecurityCompany + HousingFundCompany;
            }
        }

        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate
        {
            get
            {
                if (ExpectedWorkDays == 0)
                    return 0;
                return Math.Round((decimal)ActualWorkDays / ExpectedWorkDays * 100, 2);
            }
        }

        /// <summary>
        /// 是否已完成核算
        /// </summary>
        public bool IsCalculated => CalculationStatus == "已完成" || CalculationStatus == "已审核";

        /// <summary>
        /// 是否已审核
        /// </summary>
        public bool IsApproved => ApprovalStatus == "已审核";

        /// <summary>
        /// 是否已发放
        /// </summary>
        public bool IsPaid => PaymentStatus == "已发放";

        /// <summary>
        /// 计算应发工资总额
        /// </summary>
        public void CalculateGrossSalary()
        {
            GrossSalary = BasicSalaryTotal + PerformanceSalary + AllowanceTotal + 
                         OvertimePayTotal + NonCompeteCompensation + BonusTotal - DeductionTotal;
        }

        /// <summary>
        /// 计算实发工资
        /// </summary>
        public void CalculateNetSalary()
        {
            NetSalary = GrossSalary - IncomeTax - PersonalContributionTotal;
        }

        /// <summary>
        /// 计算公司总成本
        /// </summary>
        public void CalculateTotalCost()
        {
            TotalCost = GrossSalary + CompanyContributionTotal;
        }

        /// <summary>
        /// 执行完整的工资计算
        /// </summary>
        public void CalculateAll()
        {
            CalculateGrossSalary();
            CalculateNetSalary();
            CalculateTotalCost();
        }

        /// <summary>
        /// 验证工资核算结果
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidatePayroll()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (EmployeeID <= 0)
                result.AddError("员工ID必须大于0");

            if (Year < 2000 || Year > 3000)
                result.AddError("工资年份必须在2000-3000之间");

            if (Month < 1 || Month > 12)
                result.AddError("工资月份必须在1-12之间");

            // 检查工资期间是否合理
            var periodDate = new DateTime(Year, Month, 1);
            if (periodDate > DateTime.Today)
                result.AddError("工资期间不能晚于当前月份");

            // 检查数值逻辑
            if (ActualWorkDays > ExpectedWorkDays)
                result.AddError("实际出勤天数不能大于应出勤天数");

            if (LeaveDays > ExpectedWorkDays)
                result.AddError("请假天数不能大于应出勤天数");

            // 检查计算结果的一致性
            var calculatedGross = BasicSalaryTotal + PerformanceSalary + AllowanceTotal + 
                                 OvertimePayTotal + NonCompeteCompensation + BonusTotal - DeductionTotal;
            if (Math.Abs(GrossSalary - calculatedGross) > 0.01m)
                result.AddError($"应发工资总额计算不正确，应为{calculatedGross:F2}，实际为{GrossSalary:F2}");

            var calculatedNet = GrossSalary - IncomeTax - PersonalContributionTotal;
            if (Math.Abs(NetSalary - calculatedNet) > 0.01m)
                result.AddError($"实发工资计算不正确，应为{calculatedNet:F2}，实际为{NetSalary:F2}");

            var calculatedCost = GrossSalary + CompanyContributionTotal;
            if (Math.Abs(TotalCost - calculatedCost) > 0.01m)
                result.AddError($"公司总成本计算不正确，应为{calculatedCost:F2}，实际为{TotalCost:F2}");

            // 检查状态逻辑
            if (IsCalculated && !CalculationDate.HasValue)
                result.AddError("已完成核算的记录必须有核算日期");

            if (IsApproved && !ApprovalDate.HasValue)
                result.AddError("已审核的记录必须有审核日期");

            if (IsPaid && !PaymentDate.HasValue)
                result.AddError("已发放的记录必须有发放日期");

            // 检查合理性
            if (GrossSalary > 1000000)
                result.AddWarning("应发工资超过100万，请确认是否正确");

            if (NetSalary < 0)
                result.AddWarning("实发工资为负数，请检查扣款设置");

            if (AttendanceRate < 50)
                result.AddWarning($"出勤率过低({AttendanceRate:F1}%)，请确认考勤数据");

            return result;
        }

        /// <summary>
        /// 克隆工资核算结果对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public PayrollResult Clone()
        {
            return new PayrollResult
            {
                PayrollID = this.PayrollID,
                EmployeeID = this.EmployeeID,
                Year = this.Year,
                Month = this.Month,
                BaseSalary = this.BaseSalary,
                PositionSalary = this.PositionSalary,
                SkillSalary = this.SkillSalary,
                SeniorityPay = this.SeniorityPay,
                PerformanceSalary = this.PerformanceSalary,
                Allowance = this.Allowance,
                TransportAllowance = this.TransportAllowance,
                MealAllowance = this.MealAllowance,
                CommunicationAllowance = this.CommunicationAllowance,
                HousingAllowance = this.HousingAllowance,
                OtherFixedAllowance = this.OtherFixedAllowance,
                OvertimePay = this.OvertimePay,
                SaturdayOvertimePay = this.SaturdayOvertimePay,
                SundayOvertimePay = this.SundayOvertimePay,
                HolidayOvertimePay = this.HolidayOvertimePay,
                NonCompeteCompensation = this.NonCompeteCompensation,
                PerfectAttendanceBonus = this.PerfectAttendanceBonus,
                OtherBonus = this.OtherBonus,
                OtherDeduction = this.OtherDeduction,
                LateDeduction = this.LateDeduction,
                EarlyLeaveDeduction = this.EarlyLeaveDeduction,
                AbsentDeduction = this.AbsentDeduction,
                PersonalLeaveDeduction = this.PersonalLeaveDeduction,
                SickLeaveDeduction = this.SickLeaveDeduction,
                GrossSalary = this.GrossSalary,
                IncomeTax = this.IncomeTax,
                SocialSecurityPersonal = this.SocialSecurityPersonal,
                HousingFundPersonal = this.HousingFundPersonal,
                SocialSecurityCompany = this.SocialSecurityCompany,
                HousingFundCompany = this.HousingFundCompany,
                NetSalary = this.NetSalary,
                TotalCost = this.TotalCost,
                ExpectedWorkDays = this.ExpectedWorkDays,
                ActualWorkDays = this.ActualWorkDays,
                LeaveDays = this.LeaveDays,
                LateCount = this.LateCount,
                EarlyLeaveCount = this.EarlyLeaveCount,
                TotalOvertimeHours = this.TotalOvertimeHours,
                IsPerfectAttendance = this.IsPerfectAttendance,
                CalculationStatus = this.CalculationStatus,
                ApprovalStatus = this.ApprovalStatus,
                PaymentStatus = this.PaymentStatus,
                Calculator = this.Calculator,
                CalculationDate = this.CalculationDate,
                Approver = this.Approver,
                ApprovalDate = this.ApprovalDate,
                Payer = this.Payer,
                PaymentDate = this.PaymentDate,
                Remarks = this.Remarks,
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
            return $"Payroll[{EmployeeID}]: {PeriodString} - 应发:{GrossSalary:C} 实发:{NetSalary:C} - {CalculationStatus}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is PayrollResult other)
            {
                return PayrollID == other.PayrollID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return PayrollID.GetHashCode();
        }
    }
}