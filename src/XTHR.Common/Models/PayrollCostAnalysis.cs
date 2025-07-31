using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 工资成本分析实体类
    /// </summary>
    public class PayrollCostAnalysis
    {
        /// <summary>
        /// 分析ID（主键）
        /// </summary>
        public int AnalysisID { get; set; }

        /// <summary>
        /// 分析年份
        /// </summary>
        [Required(ErrorMessage = "分析年份不能为空")]
        [Range(2000, 3000, ErrorMessage = "分析年份必须在2000-3000之间")]
        public int AnalysisYear { get; set; }

        /// <summary>
        /// 分析月份
        /// </summary>
        [Required(ErrorMessage = "分析月份不能为空")]
        [Range(1, 12, ErrorMessage = "分析月份必须在1-12之间")]
        public int AnalysisMonth { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(100, ErrorMessage = "部门名称长度不能超过100个字符")]
        public string? Department { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        [StringLength(100, ErrorMessage = "职位名称长度不能超过100个字符")]
        public string? Position { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [StringLength(50, ErrorMessage = "职级长度不能超过50个字符")]
        public string? JobLevel { get; set; }

        /// <summary>
        /// 员工总数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "员工总数不能为负数")]
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 在职员工数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "在职员工数不能为负数")]
        public int ActiveEmployees { get; set; }

        /// <summary>
        /// 离职员工数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "离职员工数不能为负数")]
        public int InactiveEmployees { get; set; }

        /// <summary>
        /// 新入职员工数
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "新入职员工数不能为负数")]
        public int NewEmployees { get; set; }

        /// <summary>
        /// 基础工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "基础工资总额不能为负数")]
        public decimal TotalBaseSalary { get; set; }

        /// <summary>
        /// 岗位工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "岗位工资总额不能为负数")]
        public decimal TotalPositionSalary { get; set; }

        /// <summary>
        /// 技能工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "技能工资总额不能为负数")]
        public decimal TotalSkillSalary { get; set; }

        /// <summary>
        /// 工龄工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "工龄工资总额不能为负数")]
        public decimal TotalSenioritySalary { get; set; }

        /// <summary>
        /// 绩效工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "绩效工资总额不能为负数")]
        public decimal TotalPerformanceSalary { get; set; }

        /// <summary>
        /// 津贴补助总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "津贴补助总额不能为负数")]
        public decimal TotalAllowances { get; set; }

        /// <summary>
        /// 加班费总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "加班费总额不能为负数")]
        public decimal TotalOvertimePay { get; set; }

        /// <summary>
        /// 竞业补偿总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "竞业补偿总额不能为负数")]
        public decimal TotalNonCompeteCompensation { get; set; }

        /// <summary>
        /// 奖金总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "奖金总额不能为负数")]
        public decimal TotalBonuses { get; set; }

        /// <summary>
        /// 扣款总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "扣款总额不能为负数")]
        public decimal TotalDeductions { get; set; }

        /// <summary>
        /// 应发工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "应发工资总额不能为负数")]
        public decimal TotalGrossPay { get; set; }

        /// <summary>
        /// 个人所得税总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "个人所得税总额不能为负数")]
        public decimal TotalIncomeTax { get; set; }

        /// <summary>
        /// 个人社保缴费总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "个人社保缴费总额不能为负数")]
        public decimal TotalPersonalSocialSecurity { get; set; }

        /// <summary>
        /// 个人公积金缴费总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "个人公积金缴费总额不能为负数")]
        public decimal TotalPersonalHousingFund { get; set; }

        /// <summary>
        /// 实发工资总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "实发工资总额不能为负数")]
        public decimal TotalNetPay { get; set; }

        /// <summary>
        /// 公司社保缴费总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "公司社保缴费总额不能为负数")]
        public decimal TotalCompanySocialSecurity { get; set; }

        /// <summary>
        /// 公司公积金缴费总额
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "公司公积金缴费总额不能为负数")]
        public decimal TotalCompanyHousingFund { get; set; }

        /// <summary>
        /// 公司总成本
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "公司总成本不能为负数")]
        public decimal TotalCompanyCost { get; set; }

        /// <summary>
        /// 平均基础工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "平均基础工资不能为负数")]
        public decimal AverageBaseSalary { get; set; }

        /// <summary>
        /// 平均应发工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "平均应发工资不能为负数")]
        public decimal AverageGrossPay { get; set; }

        /// <summary>
        /// 平均实发工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "平均实发工资不能为负数")]
        public decimal AverageNetPay { get; set; }

        /// <summary>
        /// 平均公司成本
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "平均公司成本不能为负数")]
        public decimal AverageCompanyCost { get; set; }

        /// <summary>
        /// 工资成本率（工资成本/营业收入）
        /// </summary>
        [Range(0, 1, ErrorMessage = "工资成本率必须在0-1之间")]
        public decimal PayrollCostRatio { get; set; }

        /// <summary>
        /// 人均产出（营业收入/员工数）
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "人均产出不能为负数")]
        public decimal RevenuePerEmployee { get; set; }

        /// <summary>
        /// 人工效率（营业收入/工资成本）
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "人工效率不能为负数")]
        public decimal LaborEfficiency { get; set; }

        /// <summary>
        /// 社保缴费率
        /// </summary>
        [Range(0, 1, ErrorMessage = "社保缴费率必须在0-1之间")]
        public decimal SocialSecurityRate { get; set; }

        /// <summary>
        /// 个税负担率
        /// </summary>
        [Range(0, 1, ErrorMessage = "个税负担率必须在0-1之间")]
        public decimal IncomeTaxRate { get; set; }

        /// <summary>
        /// 加班费占比
        /// </summary>
        [Range(0, 1, ErrorMessage = "加班费占比必须在0-1之间")]
        public decimal OvertimePayRatio { get; set; }

        /// <summary>
        /// 绩效工资占比
        /// </summary>
        [Range(0, 1, ErrorMessage = "绩效工资占比必须在0-1之间")]
        public decimal PerformancePayRatio { get; set; }

        /// <summary>
        /// 津贴补助占比
        /// </summary>
        [Range(0, 1, ErrorMessage = "津贴补助占比必须在0-1之间")]
        public decimal AllowanceRatio { get; set; }

        /// <summary>
        /// 奖金占比
        /// </summary>
        [Range(0, 1, ErrorMessage = "奖金占比必须在0-1之间")]
        public decimal BonusRatio { get; set; }

        /// <summary>
        /// 扣款占比
        /// </summary>
        [Range(0, 1, ErrorMessage = "扣款占比必须在0-1之间")]
        public decimal DeductionRatio { get; set; }

        /// <summary>
        /// 员工流动率
        /// </summary>
        [Range(0, 1, ErrorMessage = "员工流动率必须在0-1之间")]
        public decimal TurnoverRate { get; set; }

        /// <summary>
        /// 新员工比例
        /// </summary>
        [Range(0, 1, ErrorMessage = "新员工比例必须在0-1之间")]
        public decimal NewEmployeeRatio { get; set; }

        /// <summary>
        /// 与上月对比：应发工资变化率
        /// </summary>
        public decimal GrossPayChangeRate { get; set; }

        /// <summary>
        /// 与上月对比：公司成本变化率
        /// </summary>
        public decimal CompanyCostChangeRate { get; set; }

        /// <summary>
        /// 与上月对比：员工数变化率
        /// </summary>
        public decimal EmployeeCountChangeRate { get; set; }

        /// <summary>
        /// 与去年同期对比：应发工资变化率
        /// </summary>
        public decimal YearOverYearGrossPayChangeRate { get; set; }

        /// <summary>
        /// 与去年同期对比：公司成本变化率
        /// </summary>
        public decimal YearOverYearCompanyCostChangeRate { get; set; }

        /// <summary>
        /// 与去年同期对比：员工数变化率
        /// </summary>
        public decimal YearOverYearEmployeeCountChangeRate { get; set; }

        /// <summary>
        /// 分析类型（部门/职位/职级/全公司）
        /// </summary>
        [Required(ErrorMessage = "分析类型不能为空")]
        [StringLength(20, ErrorMessage = "分析类型长度不能超过20个字符")]
        public string AnalysisType { get; set; } = "全公司";

        /// <summary>
        /// 分析维度值
        /// </summary>
        [StringLength(100, ErrorMessage = "分析维度值长度不能超过100个字符")]
        public string? AnalysisDimension { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [StringLength(50, ErrorMessage = "数据来源长度不能超过50个字符")]
        public string DataSource { get; set; } = "系统计算";

        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime CalculationTime { get; set; } = DateTime.Now;

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
        /// 分析期间（年月字符串表示）
        /// </summary>
        public string AnalysisPeriodString => $"{AnalysisYear:0000}-{AnalysisMonth:00}";

        /// <summary>
        /// 分析期间开始日期
        /// </summary>
        public DateTime AnalysisPeriodStartDate => new DateTime(AnalysisYear, AnalysisMonth, 1);

        /// <summary>
        /// 分析期间结束日期
        /// </summary>
        public DateTime AnalysisPeriodEndDate => AnalysisPeriodStartDate.AddMonths(1).AddDays(-1);

        /// <summary>
        /// 固定工资总额（基础+岗位+技能+工龄）
        /// </summary>
        public decimal TotalFixedSalary => TotalBaseSalary + TotalPositionSalary + TotalSkillSalary + TotalSenioritySalary;

        /// <summary>
        /// 变动工资总额（绩效+加班+奖金-扣款）
        /// </summary>
        public decimal TotalVariableSalary => TotalPerformanceSalary + TotalOvertimePay + TotalBonuses - TotalDeductions;

        /// <summary>
        /// 个人缴费总额
        /// </summary>
        public decimal TotalPersonalContributions => TotalPersonalSocialSecurity + TotalPersonalHousingFund;

        /// <summary>
        /// 公司缴费总额
        /// </summary>
        public decimal TotalCompanyContributions => TotalCompanySocialSecurity + TotalCompanyHousingFund;

        /// <summary>
        /// 固定工资占比
        /// </summary>
        public decimal FixedSalaryRatio => TotalGrossPay > 0 ? TotalFixedSalary / TotalGrossPay : 0;

        /// <summary>
        /// 变动工资占比
        /// </summary>
        public decimal VariableSalaryRatio => TotalGrossPay > 0 ? TotalVariableSalary / TotalGrossPay : 0;

        /// <summary>
        /// 公司缴费占应发工资比例
        /// </summary>
        public decimal CompanyContributionRatio => TotalGrossPay > 0 ? TotalCompanyContributions / TotalGrossPay : 0;

        /// <summary>
        /// 实发工资占应发工资比例
        /// </summary>
        public decimal NetPayRatio => TotalGrossPay > 0 ? TotalNetPay / TotalGrossPay : 0;

        /// <summary>
        /// 计算平均值
        /// </summary>
        public void CalculateAverages()
        {
            if (ActiveEmployees > 0)
            {
                AverageBaseSalary = TotalBaseSalary / ActiveEmployees;
                AverageGrossPay = TotalGrossPay / ActiveEmployees;
                AverageNetPay = TotalNetPay / ActiveEmployees;
                AverageCompanyCost = TotalCompanyCost / ActiveEmployees;
            }
            else
            {
                AverageBaseSalary = 0;
                AverageGrossPay = 0;
                AverageNetPay = 0;
                AverageCompanyCost = 0;
            }
        }

        /// <summary>
        /// 计算比率
        /// </summary>
        public void CalculateRatios()
        {
            // 社保缴费率
            SocialSecurityRate = TotalGrossPay > 0 ? (TotalPersonalSocialSecurity + TotalCompanySocialSecurity) / TotalGrossPay : 0;

            // 个税负担率
            IncomeTaxRate = TotalGrossPay > 0 ? TotalIncomeTax / TotalGrossPay : 0;

            // 各项工资占比
            OvertimePayRatio = TotalGrossPay > 0 ? TotalOvertimePay / TotalGrossPay : 0;
            PerformancePayRatio = TotalGrossPay > 0 ? TotalPerformanceSalary / TotalGrossPay : 0;
            AllowanceRatio = TotalGrossPay > 0 ? TotalAllowances / TotalGrossPay : 0;
            BonusRatio = TotalGrossPay > 0 ? TotalBonuses / TotalGrossPay : 0;
            DeductionRatio = TotalGrossPay > 0 ? TotalDeductions / TotalGrossPay : 0;

            // 员工比例
            TurnoverRate = TotalEmployees > 0 ? (decimal)InactiveEmployees / TotalEmployees : 0;
            NewEmployeeRatio = TotalEmployees > 0 ? (decimal)NewEmployees / TotalEmployees : 0;
        }

        /// <summary>
        /// 计算效率指标
        /// </summary>
        /// <param name="revenue">营业收入</param>
        public void CalculateEfficiencyMetrics(decimal revenue)
        {
            // 工资成本率
            PayrollCostRatio = revenue > 0 ? TotalCompanyCost / revenue : 0;

            // 人均产出
            RevenuePerEmployee = ActiveEmployees > 0 ? revenue / ActiveEmployees : 0;

            // 人工效率
            LaborEfficiency = TotalCompanyCost > 0 ? revenue / TotalCompanyCost : 0;
        }

        /// <summary>
        /// 计算同比环比变化率
        /// </summary>
        /// <param name="lastMonthData">上月数据</param>
        /// <param name="lastYearData">去年同期数据</param>
        public void CalculateChangeRates(PayrollCostAnalysis? lastMonthData, PayrollCostAnalysis? lastYearData)
        {
            // 环比变化率
            if (lastMonthData != null)
            {
                GrossPayChangeRate = lastMonthData.TotalGrossPay > 0 ? 
                    (TotalGrossPay - lastMonthData.TotalGrossPay) / lastMonthData.TotalGrossPay : 0;
                    
                CompanyCostChangeRate = lastMonthData.TotalCompanyCost > 0 ? 
                    (TotalCompanyCost - lastMonthData.TotalCompanyCost) / lastMonthData.TotalCompanyCost : 0;
                    
                EmployeeCountChangeRate = lastMonthData.ActiveEmployees > 0 ? 
                    (decimal)(ActiveEmployees - lastMonthData.ActiveEmployees) / lastMonthData.ActiveEmployees : 0;
            }

            // 同比变化率
            if (lastYearData != null)
            {
                YearOverYearGrossPayChangeRate = lastYearData.TotalGrossPay > 0 ? 
                    (TotalGrossPay - lastYearData.TotalGrossPay) / lastYearData.TotalGrossPay : 0;
                    
                YearOverYearCompanyCostChangeRate = lastYearData.TotalCompanyCost > 0 ? 
                    (TotalCompanyCost - lastYearData.TotalCompanyCost) / lastYearData.TotalCompanyCost : 0;
                    
                YearOverYearEmployeeCountChangeRate = lastYearData.ActiveEmployees > 0 ? 
                    (decimal)(ActiveEmployees - lastYearData.ActiveEmployees) / lastYearData.ActiveEmployees : 0;
            }
        }

        /// <summary>
        /// 验证分析数据
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateAnalysisData()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (AnalysisYear < 2000 || AnalysisYear > 3000)
                result.AddError("分析年份必须在2000-3000之间");

            if (AnalysisMonth < 1 || AnalysisMonth > 12)
                result.AddError("分析月份必须在1-12之间");

            if (string.IsNullOrWhiteSpace(AnalysisType))
                result.AddError("分析类型不能为空");

            // 检查员工数逻辑
            if (TotalEmployees != ActiveEmployees + InactiveEmployees)
                result.AddError("员工总数应等于在职员工数加离职员工数");

            if (NewEmployees > TotalEmployees)
                result.AddError("新入职员工数不能超过员工总数");

            // 检查工资逻辑
            var calculatedGrossPay = TotalFixedSalary + TotalPerformanceSalary + TotalAllowances + 
                                   TotalOvertimePay + TotalNonCompeteCompensation + TotalBonuses - TotalDeductions;
            if (Math.Abs(TotalGrossPay - calculatedGrossPay) > 0.01m)
                result.AddWarning("应发工资总额与各项工资之和不一致");

            var calculatedNetPay = TotalGrossPay - TotalIncomeTax - TotalPersonalContributions;
            if (Math.Abs(TotalNetPay - calculatedNetPay) > 0.01m)
                result.AddWarning("实发工资总额计算不一致");

            var calculatedCompanyCost = TotalGrossPay + TotalCompanyContributions;
            if (Math.Abs(TotalCompanyCost - calculatedCompanyCost) > 0.01m)
                result.AddWarning("公司总成本计算不一致");

            // 检查平均值逻辑
            if (ActiveEmployees > 0)
            {
                var expectedAverageGrossPay = TotalGrossPay / ActiveEmployees;
                if (Math.Abs(AverageGrossPay - expectedAverageGrossPay) > 0.01m)
                    result.AddWarning("平均应发工资计算不一致");
            }

            // 检查比率合理性
            if (SocialSecurityRate > 0.5m)
                result.AddWarning("社保缴费率超过50%，请确认是否正确");

            if (IncomeTaxRate > 0.45m)
                result.AddWarning("个税负担率超过45%，请确认是否正确");

            if (PayrollCostRatio > 1)
                result.AddWarning("工资成本率超过100%，请确认营业收入是否正确");

            if (TurnoverRate > 0.5m)
                result.AddWarning("员工流动率超过50%，请关注员工稳定性");

            // 检查数据合理性
            if (TotalGrossPay > 0 && ActiveEmployees == 0)
                result.AddError("有工资发放但在职员工数为0");

            if (ActiveEmployees > 0 && TotalGrossPay == 0)
                result.AddWarning("有在职员工但工资总额为0");

            return result;
        }

        /// <summary>
        /// 克隆分析数据对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public PayrollCostAnalysis Clone()
        {
            return new PayrollCostAnalysis
            {
                AnalysisID = this.AnalysisID,
                AnalysisYear = this.AnalysisYear,
                AnalysisMonth = this.AnalysisMonth,
                Department = this.Department,
                Position = this.Position,
                JobLevel = this.JobLevel,
                TotalEmployees = this.TotalEmployees,
                ActiveEmployees = this.ActiveEmployees,
                InactiveEmployees = this.InactiveEmployees,
                NewEmployees = this.NewEmployees,
                TotalBaseSalary = this.TotalBaseSalary,
                TotalPositionSalary = this.TotalPositionSalary,
                TotalSkillSalary = this.TotalSkillSalary,
                TotalSenioritySalary = this.TotalSenioritySalary,
                TotalPerformanceSalary = this.TotalPerformanceSalary,
                TotalAllowances = this.TotalAllowances,
                TotalOvertimePay = this.TotalOvertimePay,
                TotalNonCompeteCompensation = this.TotalNonCompeteCompensation,
                TotalBonuses = this.TotalBonuses,
                TotalDeductions = this.TotalDeductions,
                TotalGrossPay = this.TotalGrossPay,
                TotalIncomeTax = this.TotalIncomeTax,
                TotalPersonalSocialSecurity = this.TotalPersonalSocialSecurity,
                TotalPersonalHousingFund = this.TotalPersonalHousingFund,
                TotalNetPay = this.TotalNetPay,
                TotalCompanySocialSecurity = this.TotalCompanySocialSecurity,
                TotalCompanyHousingFund = this.TotalCompanyHousingFund,
                TotalCompanyCost = this.TotalCompanyCost,
                AverageBaseSalary = this.AverageBaseSalary,
                AverageGrossPay = this.AverageGrossPay,
                AverageNetPay = this.AverageNetPay,
                AverageCompanyCost = this.AverageCompanyCost,
                PayrollCostRatio = this.PayrollCostRatio,
                RevenuePerEmployee = this.RevenuePerEmployee,
                LaborEfficiency = this.LaborEfficiency,
                SocialSecurityRate = this.SocialSecurityRate,
                IncomeTaxRate = this.IncomeTaxRate,
                OvertimePayRatio = this.OvertimePayRatio,
                PerformancePayRatio = this.PerformancePayRatio,
                AllowanceRatio = this.AllowanceRatio,
                BonusRatio = this.BonusRatio,
                DeductionRatio = this.DeductionRatio,
                TurnoverRate = this.TurnoverRate,
                NewEmployeeRatio = this.NewEmployeeRatio,
                GrossPayChangeRate = this.GrossPayChangeRate,
                CompanyCostChangeRate = this.CompanyCostChangeRate,
                EmployeeCountChangeRate = this.EmployeeCountChangeRate,
                YearOverYearGrossPayChangeRate = this.YearOverYearGrossPayChangeRate,
                YearOverYearCompanyCostChangeRate = this.YearOverYearCompanyCostChangeRate,
                YearOverYearEmployeeCountChangeRate = this.YearOverYearEmployeeCountChangeRate,
                AnalysisType = this.AnalysisType,
                AnalysisDimension = this.AnalysisDimension,
                DataSource = this.DataSource,
                CalculationTime = this.CalculationTime,
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
            var dimension = string.IsNullOrEmpty(AnalysisDimension) ? "全公司" : AnalysisDimension;
            return $"PayrollCostAnalysis[{AnalysisPeriodString}]: {AnalysisType}-{dimension} - {ActiveEmployees}人 - 总成本{TotalCompanyCost:C}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is PayrollCostAnalysis other)
            {
                return AnalysisID == other.AnalysisID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return AnalysisID.GetHashCode();
        }
    }
}