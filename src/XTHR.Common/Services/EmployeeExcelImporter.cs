using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTHR.Common.Interfaces;
using XTHR.Common.Models;
using XTHR.Common.Extensions;
using Serilog;
using DataAnnotationsValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace XTHR.Common.Services
{
    /// <summary>
    /// 员工Excel导入器
    /// </summary>
    public class EmployeeExcelImporter
    {
        private readonly IExcelImporter<EmployeeImportDto> _excelImporter;
        private readonly ILogger _logger;
        
        public EmployeeExcelImporter(ILogger? logger = null)
        {
            _excelImporter = new ExcelImporter<EmployeeImportDto>(logger);
            _logger = logger ?? Log.Logger;
        }
        
        /// <summary>
        /// 导入员工数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        public async Task<ExcelImportResult<EmployeeImportDto>> ImportEmployeesAsync(string filePath, EmployeeImportOptions options = null)
        {
            try
            {
                var importOptions = CreateImportOptions(options);
                var result = await _excelImporter.ImportFromFileAsync(filePath, importOptions);
                
                // 后处理：转换为Employee对象
                if (result.IsSuccess && result.SuccessData.Any())
                {
                    await PostProcessImportedDataAsync(result.SuccessData, options);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "导入员工数据时发生错误: {FilePath}", filePath);
                return new ExcelImportResult<EmployeeImportDto>
                {
                    IsSuccess = false,
                    ErrorMessage = $"导入失败: {ex.Message}"
                };
            }
        }
        
        /// <summary>
        /// 导入员工数据流
        /// </summary>
        /// <param name="stream">Excel数据流</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        public async Task<ExcelImportResult<EmployeeImportDto>> ImportEmployeesAsync(Stream stream, EmployeeImportOptions options = null)
        {
            try
            {
                var importOptions = CreateImportOptions(options);
                var result = await _excelImporter.ImportFromStreamAsync(stream, importOptions);
                
                // 后处理：转换为Employee对象
                if (result.IsSuccess && result.SuccessData.Any())
                {
                    await PostProcessImportedDataAsync(result.SuccessData, options);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "导入员工数据流时发生错误");
                return new ExcelImportResult<EmployeeImportDto>
                {
                    IsSuccess = false,
                    ErrorMessage = $"导入失败: {ex.Message}"
                };
            }
        }
        
        /// <summary>
        /// 验证员工Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>验证结果</returns>
        public async Task<ExcelValidationResult> ValidateEmployeeFileAsync(string filePath, EmployeeImportOptions options = null)
        {
            var importOptions = CreateImportOptions(options);
            return await _excelImporter.ValidateFileAsync(filePath, importOptions);
        }
        
        /// <summary>
        /// 生成员工导入模板
        /// </summary>
        /// <param name="filePath">保存路径</param>
        /// <param name="options">模板选项</param>
        /// <returns>是否成功</returns>
        public async Task<bool> GenerateEmployeeTemplateAsync(string filePath, EmployeeTemplateOptions options = null)
        {
            var templateOptions = CreateTemplateOptions(options);
            return await _excelImporter.GenerateTemplateAsync(filePath, templateOptions);
        }
        
        /// <summary>
        /// 获取员工导入模板流
        /// </summary>
        /// <param name="options">模板选项</param>
        /// <returns>模板流</returns>
        public async Task<Stream> GetEmployeeTemplateStreamAsync(EmployeeTemplateOptions options = null)
        {
            var templateOptions = CreateTemplateOptions(options);
            return await _excelImporter.GetTemplateStreamAsync(templateOptions);
        }
        
        #region 私有方法
        
        /// <summary>
        /// 创建导入选项
        /// </summary>
        private ExcelImportOptions<EmployeeImportDto> CreateImportOptions(EmployeeImportOptions options)
        {
            options ??= new EmployeeImportOptions();
            
            var importOptions = ExcelImporterExtensions.CreateImportOptions<EmployeeImportDto>()
                .WithWorksheet(options.WorksheetName ?? "员工信息")
                .WithHeaderRow(options.HeaderRowIndex)
                .WithDataStartRow(options.DataStartRowIndex)
                .WithMaxRows(options.MaxRows)
                .SkipEmptyRows(options.SkipEmptyRows)
                .WithStrictMode(options.StrictMode)
                .WithErrorHandling(options.ErrorHandlingStrategy)
                .WithRequiredColumns("员工编号", "姓名", "身份证号")
                .WithDataValidator(ValidateEmployeeData)
                .Build();
            
            // 添加自定义列映射
            if (options.ColumnMappings?.Any() == true)
            {
                foreach (var mapping in options.ColumnMappings)
                {
                    importOptions.ColumnMappings[mapping.Key] = mapping.Value;
                }
            }
            
            return importOptions;
        }
        
        /// <summary>
        /// 创建模板选项
        /// </summary>
        private ExcelTemplateOptions CreateTemplateOptions(EmployeeTemplateOptions options)
        {
            options ??= new EmployeeTemplateOptions();
            
            var templateOptions = ExcelImporterExtensions.CreateTemplateOptions()
                .WithWorksheetName(options.WorksheetName ?? "员工信息")
                .IncludeDataValidation(options.IncludeDataValidation)
                .IncludeSampleData(options.IncludeSampleData, options.SampleDataRows)
                .IncludeInstructions(options.IncludeInstructions)
                .Build();
            
            // 添加员工特定的列配置
            if (!options.CustomColumnConfigs?.Any() == true)
            {
                AddDefaultEmployeeColumns(templateOptions);
            }
            else if (options.CustomColumnConfigs != null)
            {
                templateOptions.ColumnConfigs.AddRange(options.CustomColumnConfigs);
            }
            
            return templateOptions;
        }
        
        /// <summary>
        /// 添加默认员工列配置
        /// </summary>
        private void AddDefaultEmployeeColumns(ExcelTemplateOptions templateOptions)
        {
            var columns = new List<ExcelColumnConfig>
            {
                new ExcelColumnConfig
                {
                    ColumnName = "EmployeeNumber",
                    DisplayName = "员工编号",
                    IsRequired = true,
                    Width = 15,
                    SampleValue = "EMP001",
                    ValidationDescription = "唯一的员工编号"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "Name",
                    DisplayName = "姓名",
                    IsRequired = true,
                    Width = 12,
                    SampleValue = "张三",
                    ValidationDescription = "员工真实姓名"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "IdCard",
                    DisplayName = "身份证号",
                    IsRequired = true,
                    Width = 20,
                    SampleValue = "110101199001011234",
                    ValidationDescription = "18位身份证号码"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "Gender",
                    DisplayName = "性别",
                    IsRequired = false,
                    Width = 8,
                    SampleValue = "男",
                    DropdownOptions = new List<string> { "男", "女" },
                    ValidationDescription = "性别：男/女"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "BirthDate",
                    DisplayName = "出生日期",
                    IsRequired = false,
                    Width = 12,
                    SampleValue = "1990-01-01",
                    Format = "yyyy-mm-dd",
                    ValidationDescription = "出生日期，格式：yyyy-MM-dd"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "Phone",
                    DisplayName = "手机号码",
                    IsRequired = false,
                    Width = 15,
                    SampleValue = "13800138000",
                    ValidationDescription = "11位手机号码"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "Email",
                    DisplayName = "邮箱",
                    IsRequired = false,
                    Width = 25,
                    SampleValue = "zhangsan@company.com",
                    ValidationDescription = "有效的邮箱地址"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "DepartmentName",
                    DisplayName = "部门",
                    IsRequired = false,
                    Width = 15,
                    SampleValue = "技术部",
                    ValidationDescription = "所属部门名称"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "PositionName",
                    DisplayName = "职位",
                    IsRequired = false,
                    Width = 15,
                    SampleValue = "软件工程师",
                    ValidationDescription = "职位名称"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "HireDate",
                    DisplayName = "入职日期",
                    IsRequired = false,
                    Width = 12,
                    SampleValue = "2023-01-01",
                    Format = "yyyy-mm-dd",
                    ValidationDescription = "入职日期，格式：yyyy-MM-dd"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "Status",
                    DisplayName = "状态",
                    IsRequired = false,
                    Width = 10,
                    SampleValue = "在职",
                    DropdownOptions = new List<string> { "在职", "离职", "试用期", "停薪留职" },
                    ValidationDescription = "员工状态"
                },
                new ExcelColumnConfig
                {
                    ColumnName = "Address",
                    DisplayName = "地址",
                    IsRequired = false,
                    Width = 30,
                    SampleValue = "北京市朝阳区xxx街道xxx号",
                    ValidationDescription = "详细地址"
                }
            };
            
            templateOptions.ColumnConfigs.AddRange(columns);
        }
        
        /// <summary>
        /// 验证员工数据
        /// </summary>
        private ExcelRowValidationResult ValidateEmployeeData(object data, int rowIndex)
        {
            var result = new ExcelRowValidationResult { IsValid = true };
            
            if (data is not EmployeeImportDto employee)
            {
                result.IsValid = false;
                result.ErrorMessage = "数据类型错误";
                return result;
            }
            
            var errors = new List<string>();
            var warnings = new List<string>();

            // 1. 使用 DataAnnotations 进行基础验证
            var context = new ValidationContext(employee, serviceProvider: null, items: null);
            var validationResults = new List<DataAnnotationsValidationResult>();
            if (!Validator.TryValidateObject(employee, context, (ICollection<DataAnnotationsValidationResult>)validationResults, true))
            {
                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage).Where(msg => msg != null)!);
            }
            
            // 2. 执行额外的业务逻辑验证
            // 验证年龄一致性
            if (employee.BirthDate.HasValue && employee.HireDate.HasValue)
            {
                var ageAtHire = employee.HireDate.Value.Year - employee.BirthDate.Value.Year;
                if (employee.HireDate.Value.DayOfYear < employee.BirthDate.Value.DayOfYear)
                {
                    ageAtHire--;
                }
                
                if (ageAtHire < 16)
                {
                    errors.Add("入职时年龄不能小于16岁");
                }
                else if (ageAtHire > 70)
                {
                    warnings.Add("入职时年龄较大，请确认");
                }
            }
            
            if (errors.Any())
            {
                result.IsValid = false;
                result.ErrorMessage = string.Join("; ", errors.Distinct());
            }
            
            if (warnings.Any())
            {
                result.WarningMessage = string.Join("; ", warnings.Distinct());
            }
            
            return result;
        }
        
        /// <summary>
        /// 后处理导入的数据
        /// </summary>
        private async Task PostProcessImportedDataAsync(List<EmployeeImportDto> importedData, EmployeeImportOptions options)
        {
            foreach (var employee in importedData)
            {
                // 数据清理和标准化
                employee.Name = employee.Name?.Trim();
                employee.EmployeeNumber = employee.EmployeeNumber?.Trim().ToUpper();
                employee.Email = employee.Email?.Trim().ToLower();
                employee.Phone = employee.Phone?.Trim();
                employee.IdCard = employee.IdCard?.Trim().ToUpper();
                
                // 设置默认值
                if (string.IsNullOrWhiteSpace(employee.Status))
                {
                    employee.Status = "试用期";
                }
                
                if (!employee.HireDate.HasValue && options?.SetDefaultHireDate == true)
                {
                    employee.HireDate = DateTime.Today;
                }
                
                // 生成默认员工编号（如果为空）
                if (string.IsNullOrWhiteSpace(employee.EmployeeNumber) && options?.GenerateEmployeeNumber == true)
                {
                    employee.EmployeeNumber = await GenerateEmployeeNumberAsync();
                }
            }
        }
        
        /// <summary>
        /// 生成员工编号
        /// </summary>
        private async Task<string> GenerateEmployeeNumberAsync()
        {
            // 这里应该调用实际的员工编号生成服务
            // 暂时使用简单的时间戳生成
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return await Task.FromResult($"EMP{timestamp}");
        }
        
        /// <summary>
        /// 验证身份证号
        /// </summary>
        private bool IsValidIdCard(string idCard)
        {
            if (string.IsNullOrWhiteSpace(idCard) || idCard.Length != 18)
            {
                return false;
            }
            
            // 简单的身份证号验证（实际应用中应该使用更严格的验证）
            return idCard.All(c => char.IsDigit(c) || c == 'X' || c == 'x') &&
                   idCard.Take(17).All(char.IsDigit);
        }
        
        /// <summary>
        /// 验证手机号
        /// </summary>
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }
            
            // 简单的手机号验证
            return phone.Length == 11 && phone.All(char.IsDigit) && phone.StartsWith("1");
        }
        
        /// <summary>
        /// 验证邮箱
        /// </summary>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        
        #endregion
    }
    
    /// <summary>
    /// 员工导入DTO
    /// </summary>
    public class EmployeeImportDto
    {
        [ExcelColumn("员工编号", DisplayName = "员工编号", IsRequired = true)]
        [Required(ErrorMessage = "员工编号不能为空")]
        [StringLength(20, ErrorMessage = "员工编号长度不能超过20个字符")]
        public string? EmployeeNumber { get; set; }
        
        [ExcelColumn("姓名", DisplayName = "姓名", IsRequired = true)]
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(50, ErrorMessage = "姓名长度不能超过50个字符")]
        public string? Name { get; set; }
        
        [ExcelColumn("身份证号", DisplayName = "身份证号", IsRequired = true)]
        [StringLength(18, ErrorMessage = "身份证号长度必须为18位")]
        public string? IdCard { get; set; }
        
        [ExcelColumn("性别", DisplayName = "性别")]
        [StringLength(2, ErrorMessage = "性别长度不能超过2个字符")]
        public string? Gender { get; set; }
        
        [ExcelColumn("出生日期", DisplayName = "出生日期")]
        public DateTime? BirthDate { get; set; }
        
        [ExcelColumn("手机号码", DisplayName = "手机号码")]
        [StringLength(11, ErrorMessage = "手机号码长度不能超过11位")]
        public string? Phone { get; set; }
        
        [ExcelColumn("邮箱", DisplayName = "邮箱")]
        [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string? Email { get; set; }
        
        [ExcelColumn("部门", DisplayName = "部门")]
        [StringLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
        public string? DepartmentName { get; set; }
        
        [ExcelColumn("职位", DisplayName = "职位")]
        [StringLength(50, ErrorMessage = "职位名称长度不能超过50个字符")]
        public string? PositionName { get; set; }
        
        [ExcelColumn("入职日期", DisplayName = "入职日期")]
        public DateTime? HireDate { get; set; }
        
        [ExcelColumn("状态", DisplayName = "状态")]
        [StringLength(20, ErrorMessage = "状态长度不能超过20个字符")]
        public string? Status { get; set; }
        
        [ExcelColumn("地址", DisplayName = "地址")]
        [StringLength(200, ErrorMessage = "地址长度不能超过200个字符")]
        public string? Address { get; set; }
        
        /// <summary>
        /// 转换为Employee对象
        /// </summary>
        public Employee ToEmployee()
        {
            return new Employee
            {
                EmployeeCode = this.EmployeeNumber ?? string.Empty,
                Name = this.Name ?? string.Empty,
                Department = this.DepartmentName ?? string.Empty,
                Position = this.PositionName ?? string.Empty,
                JobGrade = null,
                HireDate = this.HireDate ?? DateTime.Now,
                IDNumber = this.IdCard,
                ContactInfo = this.Phone,
                IsActive = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
        }
    }
    
    /// <summary>
    /// 员工导入选项
    /// </summary>
    public class EmployeeImportOptions
    {
        /// <summary>
        /// 工作表名称
        /// </summary>
        public string WorksheetName { get; set; } = "员工信息";
        
        /// <summary>
        /// 标题行索引
        /// </summary>
        public int HeaderRowIndex { get; set; } = 1;
        
        /// <summary>
        /// 数据开始行索引
        /// </summary>
        public int DataStartRowIndex { get; set; } = 2;
        
        /// <summary>
        /// 最大行数
        /// </summary>
        public int MaxRows { get; set; } = 0;
        
        /// <summary>
        /// 跳过空行
        /// </summary>
        public bool SkipEmptyRows { get; set; } = true;
        
        /// <summary>
        /// 严格模式
        /// </summary>
        public bool StrictMode { get; set; } = false;
        
        /// <summary>
        /// 错误处理策略
        /// </summary>
        public ExcelErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ExcelErrorHandlingStrategy.ContinueOnError;
        
        /// <summary>
        /// 列映射
        /// </summary>
        public Dictionary<string, string> ColumnMappings { get; set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// 生成员工编号
        /// </summary>
        public bool GenerateEmployeeNumber { get; set; } = false;
        
        /// <summary>
        /// 设置默认入职日期
        /// </summary>
        public bool SetDefaultHireDate { get; set; } = false;
    }
    
    /// <summary>
    /// 员工模板选项
    /// </summary>
    public class EmployeeTemplateOptions
    {
        /// <summary>
        /// 工作表名称
        /// </summary>
        public string WorksheetName { get; set; } = "员工信息";
        
        /// <summary>
        /// 包含数据验证
        /// </summary>
        public bool IncludeDataValidation { get; set; } = true;
        
        /// <summary>
        /// 包含示例数据
        /// </summary>
        public bool IncludeSampleData { get; set; } = true;
        
        /// <summary>
        /// 示例数据行数
        /// </summary>
        public int SampleDataRows { get; set; } = 3;
        
        /// <summary>
        /// 包含说明
        /// </summary>
        public bool IncludeInstructions { get; set; } = true;
        
        /// <summary>
        /// 自定义列配置
        /// </summary>
        public List<ExcelColumnConfig> CustomColumnConfigs { get; set; } = new List<ExcelColumnConfig>();
    }
}