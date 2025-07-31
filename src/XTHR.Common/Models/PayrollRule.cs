using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 工资规则配置实体类
    /// </summary>
    public class PayrollRule
    {
        /// <summary>
        /// 规则ID（主键）
        /// </summary>
        public int RuleID { get; set; }

        /// <summary>
        /// 规则类别
        /// </summary>
        [Required(ErrorMessage = "规则类别不能为空")]
        [StringLength(50, ErrorMessage = "规则类别长度不能超过50个字符")]
        public string RuleCategory { get; set; } = string.Empty;

        /// <summary>
        /// 规则名称
        /// </summary>
        [Required(ErrorMessage = "规则名称不能为空")]
        [StringLength(100, ErrorMessage = "规则名称长度不能超过100个字符")]
        public string RuleName { get; set; } = string.Empty;

        /// <summary>
        /// 规则代码（唯一标识）
        /// </summary>
        [Required(ErrorMessage = "规则代码不能为空")]
        [StringLength(50, ErrorMessage = "规则代码长度不能超过50个字符")]
        public string RuleCode { get; set; } = string.Empty;

        /// <summary>
        /// 规则描述
        /// </summary>
        [StringLength(500, ErrorMessage = "规则描述长度不能超过500个字符")]
        public string? RuleDescription { get; set; }

        /// <summary>
        /// 规则值（字符串形式）
        /// </summary>
        [StringLength(1000, ErrorMessage = "规则值长度不能超过1000个字符")]
        public string? RuleValue { get; set; }

        /// <summary>
        /// 数值型规则值
        /// </summary>
        public decimal? NumericValue { get; set; }

        /// <summary>
        /// 布尔型规则值
        /// </summary>
        public bool? BooleanValue { get; set; }

        /// <summary>
        /// 日期型规则值
        /// </summary>
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// JSON格式的复杂规则值
        /// </summary>
        public string? JsonValue { get; set; }

        /// <summary>
        /// 数据类型（String/Numeric/Boolean/Date/Json）
        /// </summary>
        [Required(ErrorMessage = "数据类型不能为空")]
        [StringLength(20, ErrorMessage = "数据类型长度不能超过20个字符")]
        public string DataType { get; set; } = "String";

        /// <summary>
        /// 单位（如：元、%、天、小时等）
        /// </summary>
        [StringLength(20, ErrorMessage = "单位长度不能超过20个字符")]
        public string? Unit { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public decimal? MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public decimal? MaxValue { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(200, ErrorMessage = "默认值长度不能超过200个字符")]
        public string? DefaultValue { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IsEditable { get; set; } = true;

        /// <summary>
        /// 是否系统内置
        /// </summary>
        public bool IsSystemBuiltIn { get; set; } = false;

        /// <summary>
        /// 适用范围（全公司/部门/职位/个人）
        /// </summary>
        [StringLength(20, ErrorMessage = "适用范围长度不能超过20个字符")]
        public string ApplicableScope { get; set; } = "全公司";

        /// <summary>
        /// 适用条件（JSON格式）
        /// </summary>
        public string? ApplicableConditions { get; set; }

        /// <summary>
        /// 优先级（数字越小优先级越高）
        /// </summary>
        [Range(1, 1000, ErrorMessage = "优先级必须在1-1000之间")]
        public int Priority { get; set; } = 100;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 是否当前有效
        /// </summary>
        public bool IsCurrentlyEffective { get; set; } = true;

        /// <summary>
        /// 版本号
        /// </summary>
        [StringLength(20, ErrorMessage = "版本号长度不能超过20个字符")]
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 父规则ID（用于规则继承）
        /// </summary>
        public int? ParentRuleID { get; set; }

        /// <summary>
        /// 规则层级
        /// </summary>
        [Range(1, 10, ErrorMessage = "规则层级必须在1-10之间")]
        public int RuleLevel { get; set; } = 1;

        /// <summary>
        /// 计算公式
        /// </summary>
        [StringLength(1000, ErrorMessage = "计算公式长度不能超过1000个字符")]
        public string? CalculationFormula { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        [StringLength(500, ErrorMessage = "验证规则长度不能超过500个字符")]
        public string? ValidationRule { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [StringLength(200, ErrorMessage = "错误消息长度不能超过200个字符")]
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 帮助文本
        /// </summary>
        [StringLength(500, ErrorMessage = "帮助文本长度不能超过500个字符")]
        public string? HelpText { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Range(1, 9999, ErrorMessage = "显示顺序必须在1-9999之间")]
        public int DisplayOrder { get; set; } = 100;

        /// <summary>
        /// 是否在界面显示
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// 审批状态
        /// </summary>
        [StringLength(20, ErrorMessage = "审批状态长度不能超过20个字符")]
        public string ApprovalStatus { get; set; } = "已生效";

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
        /// 导航属性：父规则
        /// </summary>
        public PayrollRule? ParentRule { get; set; }

        /// <summary>
        /// 导航属性：子规则列表
        /// </summary>
        public List<PayrollRule>? ChildRules { get; set; }

        /// <summary>
        /// 规则全名（包含类别）
        /// </summary>
        public string FullRuleName => $"{RuleCategory}.{RuleName}";

        /// <summary>
        /// 是否已审批通过
        /// </summary>
        public bool IsApproved => ApprovalStatus == "已审批" || ApprovalStatus == "已生效";

        /// <summary>
        /// 是否已拒绝
        /// </summary>
        public bool IsRejected => ApprovalStatus == "已拒绝" || ApprovalStatus == "拒绝";

        /// <summary>
        /// 是否在有效期内
        /// </summary>
        public bool IsInEffectivePeriod
        {
            get
            {
                var now = DateTime.Now.Date;
                var effectiveCheck = !EffectiveDate.HasValue || EffectiveDate.Value.Date <= now;
                var expiryCheck = !ExpiryDate.HasValue || ExpiryDate.Value.Date >= now;
                return effectiveCheck && expiryCheck;
            }
        }

        /// <summary>
        /// 是否可以使用
        /// </summary>
        public bool CanUse => IsActive && IsApproved && IsInEffectivePeriod && IsCurrentlyEffective;

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        public bool CanEdit => IsEditable && !IsSystemBuiltIn && !IsReadOnly;

        /// <summary>
        /// 获取规则的实际值
        /// </summary>
        /// <returns>规则值</returns>
        public object? GetRuleValue()
        {
            return DataType.ToUpper() switch
            {
                "NUMERIC" => NumericValue,
                "BOOLEAN" => BooleanValue,
                "DATE" => DateValue,
                "JSON" => JsonValue,
                _ => RuleValue
            };
        }

        /// <summary>
        /// 设置规则值
        /// </summary>
        /// <param name="value">规则值</param>
        public void SetRuleValue(object? value)
        {
            switch (DataType.ToUpper())
            {
                case "NUMERIC":
                    if (value is decimal decimalValue)
                        NumericValue = decimalValue;
                    else if (decimal.TryParse(value?.ToString(), out var parsedDecimal))
                        NumericValue = parsedDecimal;
                    break;
                    
                case "BOOLEAN":
                    if (value is bool boolValue)
                        BooleanValue = boolValue;
                    else if (bool.TryParse(value?.ToString(), out var parsedBool))
                        BooleanValue = parsedBool;
                    break;
                    
                case "DATE":
                    if (value is DateTime dateValue)
                        DateValue = dateValue;
                    else if (DateTime.TryParse(value?.ToString(), out var parsedDate))
                        DateValue = parsedDate;
                    break;
                    
                case "JSON":
                    JsonValue = value?.ToString();
                    break;
                    
                default:
                    RuleValue = value?.ToString();
                    break;
            }
            
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 获取格式化的规则值
        /// </summary>
        /// <returns>格式化的规则值字符串</returns>
        public string GetFormattedValue()
        {
            var value = GetRuleValue();
            if (value == null)
                return string.Empty;

            return DataType.ToUpper() switch
            {
                "NUMERIC" when Unit == "%" => $"{value:P2}",
                "NUMERIC" when Unit == "元" => $"{value:C}",
                "NUMERIC" => $"{value}{Unit}",
                "BOOLEAN" => ((bool)value) ? "是" : "否",
                "DATE" => ((DateTime)value).ToString("yyyy-MM-dd"),
                _ => value.ToString() ?? string.Empty
            };
        }

        /// <summary>
        /// 验证规则值
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateValue(object? value)
        {
            var result = new ValidationResult();

            // 检查必填
            if (IsRequired && value == null)
            {
                result.AddError(ErrorMessage ?? $"{RuleName}不能为空");
                return result;
            }

            if (value == null)
                return result;

            // 根据数据类型验证
            switch (DataType.ToUpper())
            {
                case "NUMERIC":
                    if (!decimal.TryParse(value.ToString(), out var numericValue))
                    {
                        result.AddError($"{RuleName}必须是数字");
                        break;
                    }
                    
                    if (MinValue.HasValue && numericValue < MinValue.Value)
                        result.AddError($"{RuleName}不能小于{MinValue.Value}");
                        
                    if (MaxValue.HasValue && numericValue > MaxValue.Value)
                        result.AddError($"{RuleName}不能大于{MaxValue.Value}");
                    break;
                    
                case "BOOLEAN":
                    if (!bool.TryParse(value.ToString(), out _))
                        result.AddError($"{RuleName}必须是布尔值");
                    break;
                    
                case "DATE":
                    if (!DateTime.TryParse(value.ToString(), out _))
                        result.AddError($"{RuleName}必须是有效日期");
                    break;
            }

            // 自定义验证规则
            if (!string.IsNullOrWhiteSpace(ValidationRule))
            {
                // 这里可以实现自定义验证逻辑
                // 例如正则表达式验证、范围验证等
            }

            return result;
        }

        /// <summary>
        /// 检查规则是否适用于指定条件
        /// </summary>
        /// <param name="conditions">条件字典</param>
        /// <returns>是否适用</returns>
        public bool IsApplicable(Dictionary<string, object>? conditions = null)
        {
            if (!CanUse)
                return false;

            // 如果没有适用条件，则默认适用
            if (string.IsNullOrWhiteSpace(ApplicableConditions))
                return true;

            // 这里可以实现复杂的条件匹配逻辑
            // 例如解析JSON格式的适用条件，与传入的条件进行匹配
            
            return true; // 简化实现，实际应根据ApplicableConditions进行判断
        }

        /// <summary>
        /// 计算规则值（如果有计算公式）
        /// </summary>
        /// <param name="variables">计算变量</param>
        /// <returns>计算结果</returns>
        public object? CalculateValue(Dictionary<string, object>? variables = null)
        {
            if (string.IsNullOrWhiteSpace(CalculationFormula))
                return GetRuleValue();

            // 这里可以实现公式计算逻辑
            // 例如使用表达式引擎计算CalculationFormula
            
            return GetRuleValue(); // 简化实现
        }

        /// <summary>
        /// 验证工资规则
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidatePayrollRule()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (string.IsNullOrWhiteSpace(RuleCategory))
                result.AddError("规则类别不能为空");

            if (string.IsNullOrWhiteSpace(RuleName))
                result.AddError("规则名称不能为空");

            if (string.IsNullOrWhiteSpace(RuleCode))
                result.AddError("规则代码不能为空");

            if (string.IsNullOrWhiteSpace(DataType))
                result.AddError("数据类型不能为空");

            // 检查数据类型有效性
            var validDataTypes = new[] { "String", "Numeric", "Boolean", "Date", "Json" };
            if (!validDataTypes.Contains(DataType, StringComparer.OrdinalIgnoreCase))
                result.AddError("数据类型必须是String、Numeric、Boolean、Date或Json之一");

            // 检查数值范围
            if (MinValue.HasValue && MaxValue.HasValue && MinValue.Value > MaxValue.Value)
                result.AddError("最小值不能大于最大值");

            // 检查日期逻辑
            if (EffectiveDate.HasValue && ExpiryDate.HasValue && EffectiveDate.Value > ExpiryDate.Value)
                result.AddError("生效日期不能晚于失效日期");

            // 检查优先级
            if (Priority < 1 || Priority > 1000)
                result.AddError("优先级必须在1-1000之间");

            // 检查层级
            if (RuleLevel < 1 || RuleLevel > 10)
                result.AddError("规则层级必须在1-10之间");

            // 检查父子关系
            if (ParentRuleID.HasValue && ParentRuleID.Value == RuleID)
                result.AddError("规则不能以自己作为父规则");

            // 检查系统内置规则
            if (IsSystemBuiltIn && IsEditable)
                result.AddWarning("系统内置规则建议设置为不可编辑");

            // 检查审批逻辑
            if (IsApproved && !ApprovalDate.HasValue)
                result.AddError("已审批的规则必须有审批日期");

            if (ApprovalDate.HasValue && ApplicationDate.HasValue && ApprovalDate.Value < ApplicationDate.Value)
                result.AddError("审批日期不能早于申请日期");

            // 验证规则值
            var valueValidation = ValidateValue(GetRuleValue());
            result.Merge(valueValidation);

            return result;
        }

        /// <summary>
        /// 创建规则的新版本
        /// </summary>
        /// <param name="newValue">新的规则值</param>
        /// <param name="updatedBy">更新人</param>
        /// <returns>新版本的规则</returns>
        public PayrollRule CreateNewVersion(object? newValue, string? updatedBy = null)
        {
            var newRule = Clone();
            newRule.RuleID = 0; // 新记录
            newRule.SetRuleValue(newValue);
            newRule.Version = IncrementVersion(Version);
            newRule.EffectiveDate = DateTime.Now.Date;
            newRule.ApprovalStatus = "待审批";
            newRule.Applicant = updatedBy;
            newRule.ApplicationDate = DateTime.Now;
            newRule.CreatedDate = DateTime.Now;
            newRule.UpdatedDate = DateTime.Now;
            newRule.CreatedBy = updatedBy;
            newRule.UpdatedBy = updatedBy;
            
            return newRule;
        }

        /// <summary>
        /// 版本号递增
        /// </summary>
        /// <param name="currentVersion">当前版本号</param>
        /// <returns>新版本号</returns>
        private string IncrementVersion(string currentVersion)
        {
            if (string.IsNullOrWhiteSpace(currentVersion))
                return "1.0";

            var parts = currentVersion.Split('.');
            if (parts.Length >= 2 && int.TryParse(parts[1], out var minor))
            {
                return $"{parts[0]}.{minor + 1}";
            }
            
            return $"{currentVersion}.1";
        }

        /// <summary>
        /// 克隆工资规则对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public PayrollRule Clone()
        {
            return new PayrollRule
            {
                RuleID = this.RuleID,
                RuleCategory = this.RuleCategory,
                RuleName = this.RuleName,
                RuleCode = this.RuleCode,
                RuleDescription = this.RuleDescription,
                RuleValue = this.RuleValue,
                NumericValue = this.NumericValue,
                BooleanValue = this.BooleanValue,
                DateValue = this.DateValue,
                JsonValue = this.JsonValue,
                DataType = this.DataType,
                Unit = this.Unit,
                MinValue = this.MinValue,
                MaxValue = this.MaxValue,
                DefaultValue = this.DefaultValue,
                IsRequired = this.IsRequired,
                IsEditable = this.IsEditable,
                IsSystemBuiltIn = this.IsSystemBuiltIn,
                ApplicableScope = this.ApplicableScope,
                ApplicableConditions = this.ApplicableConditions,
                Priority = this.Priority,
                EffectiveDate = this.EffectiveDate,
                ExpiryDate = this.ExpiryDate,
                IsCurrentlyEffective = this.IsCurrentlyEffective,
                Version = this.Version,
                ParentRuleID = this.ParentRuleID,
                RuleLevel = this.RuleLevel,
                CalculationFormula = this.CalculationFormula,
                ValidationRule = this.ValidationRule,
                ErrorMessage = this.ErrorMessage,
                HelpText = this.HelpText,
                DisplayOrder = this.DisplayOrder,
                IsVisible = this.IsVisible,
                IsReadOnly = this.IsReadOnly,
                ApprovalStatus = this.ApprovalStatus,
                Applicant = this.Applicant,
                ApplicationDate = this.ApplicationDate,
                Approver = this.Approver,
                ApprovalDate = this.ApprovalDate,
                ApprovalComments = this.ApprovalComments,
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
            return $"PayrollRule[{RuleCode}]: {FullRuleName} = {GetFormattedValue()} - {ApprovalStatus}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is PayrollRule other)
            {
                return RuleID == other.RuleID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return RuleID.GetHashCode();
        }
    }
}