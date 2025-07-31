using System;
using System.ComponentModel.DataAnnotations;

using XTHR.Common.Entities;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 工资基础信息实体类
    /// </summary>
    public class SalaryBase : BaseEntity<int>
    {


        /// <summary>
        /// 员工ID（外键）
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// 基础工资
        /// </summary>
        [Required(ErrorMessage = "基础工资不能为空")]
        [Range(0, double.MaxValue, ErrorMessage = "基础工资不能为负数")]
        public decimal BaseSalary { get; set; }

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
        /// 竞业补偿基数
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "竞业补偿基数不能为负数")]
        public decimal NonCompeteBase { get; set; } = 0;

        /// <summary>
        /// 是否享受竞业补偿
        /// </summary>
        public bool IsNonCompeteEligible { get; set; } = false;

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



        /// <summary>
        /// 导航属性：员工信息
        /// </summary>
        public Employee? Employee { get; set; }

        /// <summary>
        /// 计算固定工资总额（不含绩效）
        /// </summary>
        public decimal TotalFixedSalary
        {
            get
            {
                return BaseSalary + PositionSalary + SkillSalary + SeniorityPay + 
                       Allowance + TransportAllowance + MealAllowance + 
                       CommunicationAllowance + HousingAllowance + OtherFixedAllowance;
            }
        }

        /// <summary>
        /// 计算基本工资（基础工资+岗位工资+技能工资+工龄工资）
        /// </summary>
        public decimal BasicSalary
        {
            get
            {
                return BaseSalary + PositionSalary + SkillSalary + SeniorityPay;
            }
        }

        /// <summary>
        /// 计算补助总额
        /// </summary>
        public decimal TotalAllowance
        {
            get
            {
                return Allowance + TransportAllowance + MealAllowance + 
                       CommunicationAllowance + HousingAllowance + OtherFixedAllowance;
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
        /// 验证工资基础信息
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateSalaryBase()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (EmployeeID <= 0)
                result.AddError("员工ID必须大于0");

            if (BaseSalary < 0)
                result.AddError("基础工资不能为负数");

            if (EffectiveDate == default)
                result.AddError("生效日期不能为空");

            // 检查日期逻辑
            if (ExpiryDate.HasValue && ExpiryDate.Value <= EffectiveDate)
                result.AddError("失效日期必须晚于生效日期");

            // 检查工资合理性
            if (BaseSalary == 0 && PositionSalary == 0 && SkillSalary == 0)
                result.AddWarning("基础工资、岗位工资、技能工资至少应设置一项");

            if (TotalFixedSalary > 1000000)
                result.AddWarning("固定工资总额超过100万，请确认是否正确");

            // 检查竞业补偿设置
            if (IsNonCompeteEligible && NonCompeteBase <= 0)
                result.AddError("享受竞业补偿时，竞业补偿基数必须大于0");

            if (!IsNonCompeteEligible && NonCompeteBase > 0)
                result.AddWarning("未享受竞业补偿但设置了竞业补偿基数");

            return result;
        }

        /// <summary>
        /// 克隆工资基础信息对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public SalaryBase Clone()
        {
            return new SalaryBase
            {
                Id = this.Id,
                EmployeeID = this.EmployeeID,
                BaseSalary = this.BaseSalary,
                PositionSalary = this.PositionSalary,
                SkillSalary = this.SkillSalary,
                SeniorityPay = this.SeniorityPay,
                Allowance = this.Allowance,
                TransportAllowance = this.TransportAllowance,
                MealAllowance = this.MealAllowance,
                CommunicationAllowance = this.CommunicationAllowance,
                HousingAllowance = this.HousingAllowance,
                OtherFixedAllowance = this.OtherFixedAllowance,
                NonCompeteBase = this.NonCompeteBase,
                IsNonCompeteEligible = this.IsNonCompeteEligible,
                EffectiveDate = this.EffectiveDate,
                ExpiryDate = this.ExpiryDate,
                IsActive = this.IsActive,
                Remarks = this.Remarks,
                CreatedAt = this.CreatedAt,
                UpdatedAt = this.UpdatedAt,
                CreatedBy = this.CreatedBy,
                UpdatedBy = this.UpdatedBy
            };
        }

        /// <summary>
        /// 创建新版本的工资基础信息（用于工资调整）
        /// </summary>
        /// <param name="newEffectiveDate">新的生效日期</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>新版本的工资基础信息</returns>
        public SalaryBase CreateNewVersion(DateTime newEffectiveDate, string? updatedBy = null)
        {
            var newVersion = this.Clone();
            newVersion.Id = 0; // 新记录，ID重置
            newVersion.EffectiveDate = newEffectiveDate;
            newVersion.ExpiryDate = null; // 新版本默认无失效日期
            newVersion.CreatedAt = DateTime.Now;
            newVersion.UpdatedAt = DateTime.Now;
            newVersion.CreatedBy = updatedBy;
            newVersion.UpdatedBy = updatedBy;
            
            // 当前版本设置失效日期
            this.ExpiryDate = newEffectiveDate.AddDays(-1);
            this.UpdatedAt = DateTime.Now;
            this.UpdatedBy = updatedBy;
            
            return newVersion;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"SalaryBase[{EmployeeID}]: 固定工资={TotalFixedSalary:C}, 生效日期={EffectiveDate:yyyy-MM-dd}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is SalaryBase other)
            {
                return Id == other.Id;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}