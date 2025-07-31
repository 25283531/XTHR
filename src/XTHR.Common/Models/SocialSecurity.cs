using System;
using System.ComponentModel.DataAnnotations;

using XTHR.Common.Entities;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 社保参保信息实体类
    /// </summary>
    public class SocialSecurity : BaseEntity<int>
    {


        /// <summary>
        /// 员工ID（外键）
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 社保缴费基数
        /// </summary>
        [Required(ErrorMessage = "社保缴费基数不能为空")]
        [Range(0, double.MaxValue, ErrorMessage = "社保缴费基数不能为负数")]
        public decimal SocialSecurityBase { get; set; }

        /// <summary>
        /// 公积金缴费基数
        /// </summary>
        [Required(ErrorMessage = "公积金缴费基数不能为空")]
        [Range(0, double.MaxValue, ErrorMessage = "公积金缴费基数不能为负数")]
        public decimal HousingFundBase { get; set; }

        /// <summary>
        /// 养老保险个人缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "养老保险个人缴费比例必须在0-100%之间")]
        public decimal PensionPersonalRate { get; set; } = 8.0m;

        /// <summary>
        /// 养老保险公司缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "养老保险公司缴费比例必须在0-100%之间")]
        public decimal PensionCompanyRate { get; set; } = 16.0m;

        /// <summary>
        /// 医疗保险个人缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "医疗保险个人缴费比例必须在0-100%之间")]
        public decimal MedicalPersonalRate { get; set; } = 2.0m;

        /// <summary>
        /// 医疗保险公司缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "医疗保险公司缴费比例必须在0-100%之间")]
        public decimal MedicalCompanyRate { get; set; } = 9.0m;

        /// <summary>
        /// 失业保险个人缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "失业保险个人缴费比例必须在0-100%之间")]
        public decimal UnemploymentPersonalRate { get; set; } = 0.5m;

        /// <summary>
        /// 失业保险公司缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "失业保险公司缴费比例必须在0-100%之间")]
        public decimal UnemploymentCompanyRate { get; set; } = 0.5m;

        /// <summary>
        /// 工伤保险公司缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "工伤保险公司缴费比例必须在0-100%之间")]
        public decimal WorkInjuryCompanyRate { get; set; } = 0.2m;

        /// <summary>
        /// 生育保险公司缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "生育保险公司缴费比例必须在0-100%之间")]
        public decimal MaternityCompanyRate { get; set; } = 0.8m;

        /// <summary>
        /// 公积金个人缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "公积金个人缴费比例必须在0-100%之间")]
        public decimal HousingFundPersonalRate { get; set; } = 12.0m;

        /// <summary>
        /// 公积金公司缴费比例（%）
        /// </summary>
        [Range(0, 100, ErrorMessage = "公积金公司缴费比例必须在0-100%之间")]
        public decimal HousingFundCompanyRate { get; set; } = 12.0m;

        /// <summary>
        /// 是否参加养老保险
        /// </summary>
        public bool IsPensionInsured { get; set; } = true;

        /// <summary>
        /// 是否参加医疗保险
        /// </summary>
        public bool IsMedicalInsured { get; set; } = true;

        /// <summary>
        /// 是否参加失业保险
        /// </summary>
        public bool IsUnemploymentInsured { get; set; } = true;

        /// <summary>
        /// 是否参加工伤保险
        /// </summary>
        public bool IsWorkInjuryInsured { get; set; } = true;

        /// <summary>
        /// 是否参加生育保险
        /// </summary>
        public bool IsMaternityInsured { get; set; } = true;

        /// <summary>
        /// 是否缴纳公积金
        /// </summary>
        public bool IsHousingFundPaid { get; set; } = true;

        /// <summary>
        /// 社保账户类型
        /// </summary>
        [StringLength(20, ErrorMessage = "社保账户类型长度不能超过20个字符")]
        public string? SocialSecurityAccountType { get; set; }

        /// <summary>
        /// 社保卡号
        /// </summary>
        [StringLength(30, ErrorMessage = "社保卡号长度不能超过30个字符")]
        public string? SocialSecurityCardNumber { get; set; }

        /// <summary>
        /// 公积金账号
        /// </summary>
        [StringLength(30, ErrorMessage = "公积金账号长度不能超过30个字符")]
        public string? HousingFundAccountNumber { get; set; }

        /// <summary>
        /// 参保地区
        /// </summary>
        [StringLength(100, ErrorMessage = "参保地区长度不能超过100个字符")]
        public string? InsuranceRegion { get; set; }

        /// <summary>
        /// 参保开始日期
        /// </summary>
        public DateTime? InsuranceStartDate { get; set; }

        /// <summary>
        /// 参保结束日期（可为空，表示持续参保）
        /// </summary>
        public DateTime? InsuranceEndDate { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [Required(ErrorMessage = "生效日期不能为空")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期（可为空，表示一直有效）
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 是否当前有效
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remarks { get; set; }


        [StringLength(50, ErrorMessage = "更新人长度不能超过50个字符")]
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// 导航属性：员工信息
        /// </summary>
        public Employee? Employee { get; set; }

        /// <summary>
        /// 社保个人缴费总额
        /// </summary>
        public decimal SocialSecurityPersonalTotal
        {
            get
            {
                decimal total = 0;
                
                if (IsPensionInsured)
                    total += SocialSecurityBase * PensionPersonalRate / 100;
                    
                if (IsMedicalInsured)
                    total += SocialSecurityBase * MedicalPersonalRate / 100;
                    
                if (IsUnemploymentInsured)
                    total += SocialSecurityBase * UnemploymentPersonalRate / 100;
                
                return Math.Round(total, 2);
            }
        }

        /// <summary>
        /// 社保公司缴费总额
        /// </summary>
        public decimal SocialSecurityCompanyTotal
        {
            get
            {
                decimal total = 0;
                
                if (IsPensionInsured)
                    total += SocialSecurityBase * PensionCompanyRate / 100;
                    
                if (IsMedicalInsured)
                    total += SocialSecurityBase * MedicalCompanyRate / 100;
                    
                if (IsUnemploymentInsured)
                    total += SocialSecurityBase * UnemploymentCompanyRate / 100;
                    
                if (IsWorkInjuryInsured)
                    total += SocialSecurityBase * WorkInjuryCompanyRate / 100;
                    
                if (IsMaternityInsured)
                    total += SocialSecurityBase * MaternityCompanyRate / 100;
                
                return Math.Round(total, 2);
            }
        }

        /// <summary>
        /// 公积金个人缴费
        /// </summary>
        public decimal HousingFundPersonalAmount
        {
            get
            {
                if (!IsHousingFundPaid)
                    return 0;
                    
                return Math.Round(HousingFundBase * HousingFundPersonalRate / 100, 2);
            }
        }

        /// <summary>
        /// 公积金公司缴费
        /// </summary>
        public decimal HousingFundCompanyAmount
        {
            get
            {
                if (!IsHousingFundPaid)
                    return 0;
                    
                return Math.Round(HousingFundBase * HousingFundCompanyRate / 100, 2);
            }
        }

        /// <summary>
        /// 个人缴费总额（社保+公积金）
        /// </summary>
        public decimal PersonalContributionTotal
        {
            get
            {
                return SocialSecurityPersonalTotal + HousingFundPersonalAmount;
            }
        }

        /// <summary>
        /// 公司缴费总额（社保+公积金）
        /// </summary>
        public decimal CompanyContributionTotal
        {
            get
            {
                return SocialSecurityCompanyTotal + HousingFundCompanyAmount;
            }
        }

        /// <summary>
        /// 检查当前时间是否在有效期内
        /// </summary>
        /// <param name="checkDate">检查日期，默认为当前日期</param>
        /// <returns>是否有效</returns>
        public bool IsValidAt(DateTime? checkDate = null)
        {
            var date = checkDate ?? DateTime.Today;
            
            if (!IsActive)
                return false;
                
            if (date < EffectiveDate.Date)
                return false;
                
            if (ExpiryDate.HasValue && date > ExpiryDate.Value.Date)
                return false;
                
            return true;
        }

        /// <summary>
        /// 检查是否在参保期间内
        /// </summary>
        /// <param name="checkDate">检查日期，默认为当前日期</param>
        /// <returns>是否在参保期间</returns>
        public bool IsInsuredAt(DateTime? checkDate = null)
        {
            var date = checkDate ?? DateTime.Today;
            
            if (!InsuranceStartDate.HasValue)
                return false;
                
            if (date < InsuranceStartDate.Value.Date)
                return false;
                
            if (InsuranceEndDate.HasValue && date > InsuranceEndDate.Value.Date)
                return false;
                
            return true;
        }

        /// <summary>
        /// 计算指定项目的个人缴费
        /// </summary>
        /// <param name="insuranceType">保险类型</param>
        /// <returns>个人缴费金额</returns>
        public decimal CalculatePersonalContribution(string insuranceType)
        {
            return insuranceType?.ToLower() switch
            {
                "pension" or "养老" => IsPensionInsured ? Math.Round(SocialSecurityBase * PensionPersonalRate / 100, 2) : 0,
                "medical" or "医疗" => IsMedicalInsured ? Math.Round(SocialSecurityBase * MedicalPersonalRate / 100, 2) : 0,
                "unemployment" or "失业" => IsUnemploymentInsured ? Math.Round(SocialSecurityBase * UnemploymentPersonalRate / 100, 2) : 0,
                "housingfund" or "公积金" => IsHousingFundPaid ? Math.Round(HousingFundBase * HousingFundPersonalRate / 100, 2) : 0,
                _ => 0
            };
        }

        /// <summary>
        /// 计算指定项目的公司缴费
        /// </summary>
        /// <param name="insuranceType">保险类型</param>
        /// <returns>公司缴费金额</returns>
        public decimal CalculateCompanyContribution(string insuranceType)
        {
            return insuranceType?.ToLower() switch
            {
                "pension" or "养老" => IsPensionInsured ? Math.Round(SocialSecurityBase * PensionCompanyRate / 100, 2) : 0,
                "medical" or "医疗" => IsMedicalInsured ? Math.Round(SocialSecurityBase * MedicalCompanyRate / 100, 2) : 0,
                "unemployment" or "失业" => IsUnemploymentInsured ? Math.Round(SocialSecurityBase * UnemploymentCompanyRate / 100, 2) : 0,
                "workinjury" or "工伤" => IsWorkInjuryInsured ? Math.Round(SocialSecurityBase * WorkInjuryCompanyRate / 100, 2) : 0,
                "maternity" or "生育" => IsMaternityInsured ? Math.Round(SocialSecurityBase * MaternityCompanyRate / 100, 2) : 0,
                "housingfund" or "公积金" => IsHousingFundPaid ? Math.Round(HousingFundBase * HousingFundCompanyRate / 100, 2) : 0,
                _ => 0
            };
        }

        /// <summary>
        /// 验证社保信息
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateSocialSecurity()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (EmployeeID <= 0)
                result.AddError("员工ID必须大于0");

            if (SocialSecurityBase < 0)
                result.AddError("社保缴费基数不能为负数");

            if (HousingFundBase < 0)
                result.AddError("公积金缴费基数不能为负数");

            if (EffectiveDate == default)
                result.AddError("生效日期不能为空");

            // 检查日期逻辑
            if (ExpiryDate.HasValue && ExpiryDate.Value <= EffectiveDate)
                result.AddError("失效日期必须晚于生效日期");

            if (InsuranceStartDate.HasValue && InsuranceEndDate.HasValue && 
                InsuranceEndDate.Value <= InsuranceStartDate.Value)
                result.AddError("参保结束日期必须晚于参保开始日期");

            // 检查缴费比例合理性
            if (PensionPersonalRate + PensionCompanyRate > 50)
                result.AddWarning("养老保险总缴费比例超过50%，请确认是否正确");

            if (MedicalPersonalRate + MedicalCompanyRate > 20)
                result.AddWarning("医疗保险总缴费比例超过20%，请确认是否正确");

            if (HousingFundPersonalRate + HousingFundCompanyRate > 24)
                result.AddWarning("公积金总缴费比例超过24%，请确认是否正确");

            // 检查缴费基数合理性
            if (SocialSecurityBase > 0 && SocialSecurityBase < 1000)
                result.AddWarning("社保缴费基数过低，请确认是否正确");

            if (HousingFundBase > 0 && HousingFundBase < 1000)
                result.AddWarning("公积金缴费基数过低，请确认是否正确");

            if (SocialSecurityBase > 100000)
                result.AddWarning("社保缴费基数过高，请确认是否正确");

            if (HousingFundBase > 100000)
                result.AddWarning("公积金缴费基数过高，请确认是否正确");

            // 检查参保状态
            if (!IsPensionInsured && !IsMedicalInsured && !IsUnemploymentInsured && 
                !IsWorkInjuryInsured && !IsMaternityInsured && !IsHousingFundPaid)
                result.AddWarning("未参加任何社保项目");

            return result;
        }

        /// <summary>
        /// 克隆社保信息对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public SocialSecurity Clone()
        {
            return new SocialSecurity
            {
                SocialSecurityID = this.SocialSecurityID,
                EmployeeID = this.EmployeeID,
                SocialSecurityBase = this.SocialSecurityBase,
                HousingFundBase = this.HousingFundBase,
                PensionPersonalRate = this.PensionPersonalRate,
                PensionCompanyRate = this.PensionCompanyRate,
                MedicalPersonalRate = this.MedicalPersonalRate,
                MedicalCompanyRate = this.MedicalCompanyRate,
                UnemploymentPersonalRate = this.UnemploymentPersonalRate,
                UnemploymentCompanyRate = this.UnemploymentCompanyRate,
                WorkInjuryCompanyRate = this.WorkInjuryCompanyRate,
                MaternityCompanyRate = this.MaternityCompanyRate,
                HousingFundPersonalRate = this.HousingFundPersonalRate,
                HousingFundCompanyRate = this.HousingFundCompanyRate,
                IsPensionInsured = this.IsPensionInsured,
                IsMedicalInsured = this.IsMedicalInsured,
                IsUnemploymentInsured = this.IsUnemploymentInsured,
                IsWorkInjuryInsured = this.IsWorkInjuryInsured,
                IsMaternityInsured = this.IsMaternityInsured,
                IsHousingFundPaid = this.IsHousingFundPaid,
                SocialSecurityAccountType = this.SocialSecurityAccountType,
                SocialSecurityCardNumber = this.SocialSecurityCardNumber,
                HousingFundAccountNumber = this.HousingFundAccountNumber,
                InsuranceRegion = this.InsuranceRegion,
                InsuranceStartDate = this.InsuranceStartDate,
                InsuranceEndDate = this.InsuranceEndDate,
                EffectiveDate = this.EffectiveDate,
                ExpiryDate = this.ExpiryDate,
                IsActive = this.IsActive,
                Remarks = this.Remarks,
                CreatedDate = this.CreatedDate,
                UpdatedDate = this.UpdatedDate,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy
            };
        }

        /// <summary>
        /// 创建新版本的社保信息（用于调整）
        /// </summary>
        /// <param name="newEffectiveDate">新的生效日期</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>新版本的社保信息</returns>
        public SocialSecurity CreateNewVersion(DateTime newEffectiveDate, string? updatedBy = null)
        {
            var newVersion = this.Clone();
            newVersion.SocialSecurityID = 0; // 新记录，ID重置
            newVersion.EffectiveDate = newEffectiveDate;
            newVersion.ExpiryDate = null; // 新版本默认无失效日期
            newVersion.CreatedDate = DateTime.Now;
            newVersion.UpdatedDate = DateTime.Now;
            newVersion.CreatedBy = updatedBy;
            newVersion.UpdatedBy = updatedBy;
            
            // 当前版本设置失效日期
            this.ExpiryDate = newEffectiveDate.AddDays(-1);
            this.UpdatedDate = DateTime.Now;
            this.UpdatedBy = updatedBy;
            
            return newVersion;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"SocialSecurity[{EmployeeID}]: 社保基数={SocialSecurityBase:C}, 公积金基数={HousingFundBase:C}, 生效日期={EffectiveDate:yyyy-MM-dd}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is SocialSecurity other)
            {
                return SocialSecurityID == other.SocialSecurityID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return SocialSecurityID.GetHashCode();
        }
    }
}