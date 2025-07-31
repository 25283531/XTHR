using System;
using System.ComponentModel.DataAnnotations;

using XTHR.Common.Entities;

namespace XTHR.Common.Models
{
    /// <summary>
    /// 系统配置实体类
    /// </summary>
    public class SystemConfig : IEntity<int>
    {
        /// <summary>
        /// 配置ID（主键）
        /// </summary>
        public int ConfigID { get; set; }

        /// <summary>
        /// 实现 IEntity<int> 接口的 Id 属性
        /// </summary>
        public int Id
        {
            get => ConfigID;
            set => ConfigID = value;
        }

        /// <summary>
        /// 配置分组
        /// </summary>
        [Required(ErrorMessage = "配置分组不能为空")]
        [StringLength(50, ErrorMessage = "配置分组长度不能超过50个字符")]
        public string ConfigGroup { get; set; } = string.Empty;

        /// <summary>
        /// 配置键名
        /// </summary>
        [Required(ErrorMessage = "配置键名不能为空")]
        [StringLength(100, ErrorMessage = "配置键名长度不能超过100个字符")]
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        [Required(ErrorMessage = "配置名称不能为空")]
        [StringLength(100, ErrorMessage = "配置名称长度不能超过100个字符")]
        public string ConfigName { get; set; } = string.Empty;

        /// <summary>
        /// 配置描述
        /// </summary>
        [StringLength(500, ErrorMessage = "配置描述长度不能超过500个字符")]
        public string? ConfigDescription { get; set; }

        /// <summary>
        /// 配置值（字符串形式）
        /// </summary>
        [StringLength(2000, ErrorMessage = "配置值长度不能超过2000个字符")]
        public string? ConfigValue { get; set; }

        /// <summary>
        /// 数值型配置值
        /// </summary>
        public decimal? NumericValue { get; set; }

        /// <summary>
        /// 布尔型配置值
        /// </summary>
        public bool? BooleanValue { get; set; }

        /// <summary>
        /// 日期型配置值
        /// </summary>
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// JSON格式的复杂配置值
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
        [StringLength(500, ErrorMessage = "默认值长度不能超过500个字符")]
        public string? DefaultValue { get; set; }

        /// <summary>
        /// 可选值列表（JSON格式）
        /// </summary>
        public string? OptionValues { get; set; }

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
        /// 是否敏感信息（如密码等）
        /// </summary>
        public bool IsSensitive { get; set; } = false;

        /// <summary>
        /// 是否需要重启生效
        /// </summary>
        public bool RequiresRestart { get; set; } = false;

        /// <summary>
        /// 配置级别（System/Application/User）
        /// </summary>
        [StringLength(20, ErrorMessage = "配置级别长度不能超过20个字符")]
        public string ConfigLevel { get; set; } = "Application";

        /// <summary>
        /// 适用环境（Development/Testing/Production/All）
        /// </summary>
        [StringLength(20, ErrorMessage = "适用环境长度不能超过20个字符")]
        public string Environment { get; set; } = "All";

        /// <summary>
        /// 版本号
        /// </summary>
        [StringLength(20, ErrorMessage = "版本号长度不能超过20个字符")]
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 优先级（数字越小优先级越高）
        /// </summary>
        [Range(1, 1000, ErrorMessage = "优先级必须在1-1000之间")]
        public int Priority { get; set; } = 100;

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
        /// 配置分类标签
        /// </summary>
        [StringLength(100, ErrorMessage = "配置分类标签长度不能超过100个字符")]
        public string? Tags { get; set; }

        /// <summary>
        /// 最后修改原因
        /// </summary>
        [StringLength(200, ErrorMessage = "最后修改原因长度不能超过200个字符")]
        public string? LastChangeReason { get; set; }

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
        /// 配置全名（包含分组）
        /// </summary>
        public string FullConfigName => $"{ConfigGroup}.{ConfigKey}";

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        public bool CanEdit => IsEditable && !IsSystemBuiltIn && !IsReadOnly;

        /// <summary>
        /// 是否需要加密显示
        /// </summary>
        public bool ShouldMask => IsSensitive && !string.IsNullOrEmpty(ConfigValue);

        /// <summary>
        /// 获取配置的实际值
        /// </summary>
        /// <returns>配置值</returns>
        public object? GetConfigValue()
        {
            return DataType.ToUpper() switch
            {
                "NUMERIC" => NumericValue,
                "BOOLEAN" => BooleanValue,
                "DATE" => DateValue,
                "JSON" => JsonValue,
                _ => ConfigValue
            };
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="value">配置值</param>
        public void SetConfigValue(object? value)
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
                    ConfigValue = value?.ToString();
                    break;
            }
            
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 获取格式化的配置值
        /// </summary>
        /// <param name="maskSensitive">是否遮蔽敏感信息</param>
        /// <returns>格式化的配置值字符串</returns>
        public string GetFormattedValue(bool maskSensitive = true)
        {
            var value = GetConfigValue();
            if (value == null)
                return string.Empty;

            // 敏感信息遮蔽
            if (maskSensitive && IsSensitive)
                return "******";

            return DataType.ToUpper() switch
            {
                "NUMERIC" when Unit == "%" => $"{value:P2}",
                "NUMERIC" when Unit == "元" => $"{value:C}",
                "NUMERIC" => $"{value}{Unit}",
                "BOOLEAN" => ((bool)value) ? "是" : "否",
                "DATE" => ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"),
                _ => value.ToString() ?? string.Empty
            };
        }

        /// <summary>
        /// 获取配置值（泛型方法）
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>转换后的配置值</returns>
        public T? GetValue<T>()
        {
            var value = GetConfigValue();
            if (value == null)
                return default(T);

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 获取配置值（带默认值）
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值或默认值</returns>
        public T GetValueOrDefault<T>(T defaultValue)
        {
            var value = GetValue<T>();
            return value ?? defaultValue;
        }

        /// <summary>
        /// 验证配置值
        /// </summary>
        /// <param name="value">要验证的值</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateValue(object? value)
        {
            var result = new ValidationResult();

            // 检查必填
            if (IsRequired && value == null)
            {
                result.AddError(ErrorMessage ?? $"{ConfigName}不能为空");
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
                        result.AddError($"{ConfigName}必须是数字");
                        break;
                    }
                    
                    if (MinValue.HasValue && numericValue < MinValue.Value)
                        result.AddError($"{ConfigName}不能小于{MinValue.Value}");
                        
                    if (MaxValue.HasValue && numericValue > MaxValue.Value)
                        result.AddError($"{ConfigName}不能大于{MaxValue.Value}");
                    break;
                    
                case "BOOLEAN":
                    if (!bool.TryParse(value.ToString(), out _))
                        result.AddError($"{ConfigName}必须是布尔值");
                    break;
                    
                case "DATE":
                    if (!DateTime.TryParse(value.ToString(), out _))
                        result.AddError($"{ConfigName}必须是有效日期");
                    break;
            }

            // 可选值验证
            if (!string.IsNullOrWhiteSpace(OptionValues))
            {
                // 这里可以解析JSON格式的可选值列表进行验证
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
        /// 重置为默认值
        /// </summary>
        public void ResetToDefault()
        {
            if (!string.IsNullOrWhiteSpace(DefaultValue))
            {
                SetConfigValue(DefaultValue);
            }
            else
            {
                // 根据数据类型设置默认值
                switch (DataType.ToUpper())
                {
                    case "NUMERIC":
                        NumericValue = 0;
                        break;
                    case "BOOLEAN":
                        BooleanValue = false;
                        break;
                    case "DATE":
                        DateValue = DateTime.Now;
                        break;
                    default:
                        ConfigValue = string.Empty;
                        break;
                }
            }
            
            UpdatedDate = DateTime.Now;
        }

        /// <summary>
        /// 检查配置是否适用于当前环境
        /// </summary>
        /// <param name="currentEnvironment">当前环境</param>
        /// <returns>是否适用</returns>
        public bool IsApplicableToEnvironment(string currentEnvironment)
        {
            if (Environment.Equals("All", StringComparison.OrdinalIgnoreCase))
                return true;
                
            return Environment.Equals(currentEnvironment, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 验证系统配置
        /// </summary>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateSystemConfig()
        {
            var result = new ValidationResult();

            // 检查必填字段
            if (string.IsNullOrWhiteSpace(ConfigGroup))
                result.AddError("配置分组不能为空");

            if (string.IsNullOrWhiteSpace(ConfigKey))
                result.AddError("配置键名不能为空");

            if (string.IsNullOrWhiteSpace(ConfigName))
                result.AddError("配置名称不能为空");

            if (string.IsNullOrWhiteSpace(DataType))
                result.AddError("数据类型不能为空");

            // 检查数据类型有效性
            var validDataTypes = new[] { "String", "Numeric", "Boolean", "Date", "Json" };
            if (!validDataTypes.Contains(DataType, StringComparer.OrdinalIgnoreCase))
                result.AddError("数据类型必须是String、Numeric、Boolean、Date或Json之一");

            // 检查配置级别
            var validLevels = new[] { "System", "Application", "User" };
            if (!validLevels.Contains(ConfigLevel, StringComparer.OrdinalIgnoreCase))
                result.AddError("配置级别必须是System、Application或User之一");

            // 检查环境
            var validEnvironments = new[] { "Development", "Testing", "Production", "All" };
            if (!validEnvironments.Contains(Environment, StringComparer.OrdinalIgnoreCase))
                result.AddError("适用环境必须是Development、Testing、Production或All之一");

            // 检查数值范围
            if (MinValue.HasValue && MaxValue.HasValue && MinValue.Value > MaxValue.Value)
                result.AddError("最小值不能大于最大值");

            // 检查优先级
            if (Priority < 1 || Priority > 1000)
                result.AddError("优先级必须在1-1000之间");

            // 检查显示顺序
            if (DisplayOrder < 1 || DisplayOrder > 9999)
                result.AddError("显示顺序必须在1-9999之间");

            // 检查系统内置配置
            if (IsSystemBuiltIn && IsEditable)
                result.AddWarning("系统内置配置建议设置为不可编辑");

            // 验证配置值
            var valueValidation = ValidateValue(GetConfigValue());
            result.Merge(valueValidation);

            return result;
        }

        /// <summary>
        /// 创建配置的备份
        /// </summary>
        /// <param name="reason">备份原因</param>
        /// <returns>备份的配置对象</returns>
        public SystemConfig CreateBackup(string? reason = null)
        {
            var backup = Clone();
            backup.ConfigID = 0; // 新记录
            backup.ConfigKey = $"{ConfigKey}_backup_{DateTime.Now:yyyyMMddHHmmss}";
            backup.ConfigName = $"{ConfigName}（备份）";
            backup.LastChangeReason = reason ?? "配置备份";
            backup.IsActive = false;
            backup.CreatedDate = DateTime.Now;
            backup.UpdatedDate = DateTime.Now;
            
            return backup;
        }

        /// <summary>
        /// 克隆系统配置对象
        /// </summary>
        /// <returns>克隆的对象</returns>
        public SystemConfig Clone()
        {
            return new SystemConfig
            {
                ConfigID = this.ConfigID,
                ConfigGroup = this.ConfigGroup,
                ConfigKey = this.ConfigKey,
                ConfigName = this.ConfigName,
                ConfigDescription = this.ConfigDescription,
                ConfigValue = this.ConfigValue,
                NumericValue = this.NumericValue,
                BooleanValue = this.BooleanValue,
                DateValue = this.DateValue,
                JsonValue = this.JsonValue,
                DataType = this.DataType,
                Unit = this.Unit,
                MinValue = this.MinValue,
                MaxValue = this.MaxValue,
                DefaultValue = this.DefaultValue,
                OptionValues = this.OptionValues,
                IsRequired = this.IsRequired,
                IsEditable = this.IsEditable,
                IsSystemBuiltIn = this.IsSystemBuiltIn,
                IsSensitive = this.IsSensitive,
                RequiresRestart = this.RequiresRestart,
                ConfigLevel = this.ConfigLevel,
                Environment = this.Environment,
                Version = this.Version,
                Priority = this.Priority,
                DisplayOrder = this.DisplayOrder,
                IsVisible = this.IsVisible,
                IsReadOnly = this.IsReadOnly,
                ValidationRule = this.ValidationRule,
                ErrorMessage = this.ErrorMessage,
                HelpText = this.HelpText,
                Tags = this.Tags,
                LastChangeReason = this.LastChangeReason,
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
            return $"SystemConfig[{FullConfigName}]: {ConfigName} = {GetFormattedValue()}";
        }

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object? obj)
        {
            if (obj is SystemConfig other)
            {
                return ConfigID == other.ConfigID;
            }
            return false;
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return ConfigID.GetHashCode();
        }
    }
}