# Excel导入器 (Excel Importer)

## 概述

XTHR.Common.ExcelImporter 是一个功能强大、易于使用的Excel数据导入组件，专为.NET应用程序设计。它提供了完整的Excel文件处理能力，包括数据导入、验证、错误处理、模板生成等功能。

## 主要特性

- ✅ **类型安全**: 基于泛型设计，支持强类型数据导入
- ✅ **灵活配置**: 丰富的配置选项，满足各种导入需求
- ✅ **数据验证**: 内置数据验证和自定义验证规则支持
- ✅ **错误处理**: 完善的错误处理机制和错误报告生成
- ✅ **模板生成**: 自动生成Excel导入模板
- ✅ **批量处理**: 支持大文件和批量数据处理
- ✅ **扩展性强**: 丰富的扩展方法和自定义选项
- ✅ **性能优化**: 基于EPPlus，高效处理Excel文件

## 快速开始

### 1. 安装依赖

确保项目已安装以下NuGet包：

```xml
<PackageReference Include="EPPlus" Version="6.2.10" />
<PackageReference Include="System.ComponentModel.Annotations" Version="5.0.0" />
```

### 2. 定义数据模型

```csharp
using XTHR.Common.Services;
using System.ComponentModel.DataAnnotations;

public class EmployeeImportDto
{
    [ExcelColumn("员工编号", Order = 1)]
    [Required(ErrorMessage = "员工编号不能为空")]
    public string EmployeeNumber { get; set; }
    
    [ExcelColumn("姓名", Order = 2)]
    [Required(ErrorMessage = "姓名不能为空")]
    [StringLength(50, ErrorMessage = "姓名长度不能超过50个字符")]
    public string Name { get; set; }
    
    [ExcelColumn("身份证号", Order = 3)]
    [RegularExpression(@"^\d{17}[\dXx]$", ErrorMessage = "身份证号格式不正确")]
    public string IdCard { get; set; }
    
    [ExcelColumn("手机号", Order = 4)]
    [Phone(ErrorMessage = "手机号格式不正确")]
    public string Phone { get; set; }
    
    [ExcelColumn("邮箱", Order = 5)]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string Email { get; set; }
}
```

### 3. 基础导入

```csharp
using XTHR.Common.Services;
using XTHR.Common.Extensions;

// 创建导入器
var importer = new ExcelImporter<EmployeeImportDto>();

// 配置导入选项
var options = ExcelImporterExtensions.CreateImportOptions()
    .WithWorksheet("员工信息")
    .WithHeaderRow(1)
    .WithDataStartRow(2)
    .SkipEmptyRows()
    .WithErrorHandling(ExcelErrorHandlingStrategy.ContinueOnError)
    .Build();

// 执行导入
var result = await importer.ImportFromFileAsync("employees.xlsx", options);

// 处理结果
if (result.IsSuccess)
{
    Console.WriteLine($"导入成功: {result.SuccessData.Count} 条记录");
    foreach (var employee in result.SuccessData)
    {
        Console.WriteLine($"员工: {employee.Name} ({employee.EmployeeNumber})");
    }
}
else
{
    Console.WriteLine($"导入失败: {result.ErrorMessage}");
}
```

### 4. 快速导入（使用扩展方法）

```csharp
using XTHR.Common.Extensions;

// 一行代码完成导入
var result = await ExcelImporterExtensions.QuickImportAsync<EmployeeImportDto>("employees.xlsx");
```

## 详细使用指南

### 导入选项配置

#### 使用构建器模式

```csharp
var options = ExcelImporterExtensions.CreateImportOptions()
    .WithWorksheet("数据表")                    // 指定工作表名称
    .WithHeaderRow(1)                          // 标题行索引
    .WithDataStartRow(2)                       // 数据开始行索引
    .WithMaxRows(1000)                         // 最大处理行数
    .SkipEmptyRows()                           // 跳过空行
    .WithStrictMode(false)                     // 非严格模式
    .WithErrorHandling(ExcelErrorHandlingStrategy.ContinueOnError) // 错误处理策略
    .WithRequiredColumns("员工编号", "姓名")      // 必需列
    .WithColumnMapping("工号", "EmployeeNumber") // 列名映射
    .WithDataValidator(CustomValidator)         // 自定义验证器
    .Build();
```

#### 直接创建选项对象

```csharp
var options = new ExcelImportOptions
{
    WorksheetName = "员工信息",
    HeaderRowIndex = 1,
    DataStartRowIndex = 2,
    MaxRows = 500,
    SkipEmptyRows = true,
    StrictMode = false,
    ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError,
    RequiredColumns = new[] { "员工编号", "姓名" },
    ColumnMappings = new Dictionary<string, string>
    {
        { "工号", "EmployeeNumber" },
        { "真实姓名", "Name" }
    }
};
```

### 自定义数据验证

```csharp
var options = ExcelImporterExtensions.CreateImportOptions()
    .WithDataValidator((data, rowIndex) =>
    {
        var result = new ExcelRowValidationResult { IsValid = true };
        
        if (data is EmployeeImportDto employee)
        {
            var errors = new List<string>();
            var warnings = new List<string>();
            
            // 业务规则验证
            if (IsEmployeeNumberExists(employee.EmployeeNumber))
            {
                errors.Add($"员工编号 {employee.EmployeeNumber} 已存在");
            }
            
            if (!IsDepartmentExists(employee.DepartmentName))
            {
                warnings.Add($"部门 {employee.DepartmentName} 不存在，将创建新部门");
            }
            
            if (errors.Any())
            {
                result.IsValid = false;
                result.ErrorMessage = string.Join("; ", errors);
            }
            
            if (warnings.Any())
            {
                result.WarningMessage = string.Join("; ", warnings);
            }
        }
        
        return result;
    })
    .Build();
```

### 错误处理策略

```csharp
// 遇到第一个错误时停止
options.ErrorHandlingStrategy = ExcelErrorHandlingStrategy.StopOnFirstError;

// 继续处理，收集所有错误
options.ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError;

// 跳过错误行，只处理正确的数据
options.ErrorHandlingStrategy = ExcelErrorHandlingStrategy.SkipErrorRows;
```

### 处理导入结果

```csharp
var result = await importer.ImportFromFileAsync(filePath, options);

// 检查导入状态
if (result.IsSuccess)
{
    // 获取成功导入的数据
    var successData = result.SuccessData;
    Console.WriteLine($"成功导入 {successData.Count} 条记录");
    
    // 处理成功的数据
    foreach (var item in successData)
    {
        // 保存到数据库或进行其他处理
    }
}

// 处理错误
if (result.Errors.Any())
{
    Console.WriteLine($"发现 {result.Errors.Count} 个错误:");
    foreach (var error in result.Errors)
    {
        Console.WriteLine($"第{error.RowIndex}行: {error.ErrorMessage}");
    }
    
    // 导出错误报告
    await result.ExportErrorReportAsync("error_report.xlsx");
}

// 处理警告
if (result.Warnings.Any())
{
    Console.WriteLine($"发现 {result.Warnings.Count} 个警告:");
    foreach (var warning in result.Warnings)
    {
        Console.WriteLine($"第{warning.RowIndex}行: {warning.WarningMessage}");
    }
}

// 获取数据质量评分
var qualityScore = result.GetDataQualityScore();
Console.WriteLine($"数据质量评分: {qualityScore}分");
```

### 生成Excel模板

```csharp
// 使用通用导入器生成模板
var importer = new ExcelImporter<EmployeeImportDto>();
var templateOptions = new ExcelTemplateOptions
{
    WorksheetName = "员工信息导入模板",
    IncludeDataValidation = true,    // 包含数据验证
    IncludeSampleData = true,        // 包含示例数据
    SampleDataRows = 3,              // 示例数据行数
    IncludeInstructions = true       // 包含使用说明
};

// 生成模板文件
var success = await importer.GenerateTemplateAsync("template.xlsx", templateOptions);

// 获取模板流（用于Web下载）
using var templateStream = await importer.GetTemplateStreamAsync(templateOptions);
```

### 员工专用导入器

```csharp
// 使用专门的员工导入器
var employeeImporter = new EmployeeExcelImporter(logger);

// 员工导入选项
var employeeOptions = new EmployeeImportOptions
{
    GenerateEmployeeNumber = true,   // 自动生成员工编号
    SetDefaultHireDate = true,       // 设置默认入职日期
    ValidateIdCard = true,           // 验证身份证号
    ValidatePhone = true,            // 验证手机号
    ValidateEmail = true             // 验证邮箱
};

// 验证文件
var validationResult = await employeeImporter.ValidateEmployeeFileAsync(filePath, employeeOptions);
if (!validationResult.IsValid)
{
    Console.WriteLine("文件验证失败");
    return;
}

// 导入员工数据
var importResult = await employeeImporter.ImportEmployeesAsync(filePath, employeeOptions);

// 生成员工模板
var templateOptions = new EmployeeTemplateOptions
{
    IncludeSampleData = true,
    SampleDataRows = 5
};
await employeeImporter.GenerateEmployeeTemplateAsync("employee_template.xlsx", templateOptions);
```

### 批量处理大文件

```csharp
// 流式处理大文件
using var fileStream = new FileStream("large_file.xlsx", FileMode.Open, FileAccess.Read);

var options = ExcelImporterExtensions.CreateImportOptions()
    .WithMaxRows(1000)  // 限制每次处理的行数
    .Build();

var result = await importer.ImportFromStreamAsync(fileStream, options);

// 分批处理数据
const int batchSize = 100;
var batches = result.SuccessData
    .Select((item, index) => new { item, index })
    .GroupBy(x => x.index / batchSize)
    .Select(g => g.Select(x => x.item).ToList());

foreach (var batch in batches)
{
    // 处理每批数据
    await ProcessBatchAsync(batch);
}
```

## API 参考

### 核心接口

#### IExcelImporter<T>

```csharp
public interface IExcelImporter<T> where T : class, new()
{
    Task<ExcelImportResult<T>> ImportFromFileAsync(string filePath, ExcelImportOptions options = null);
    Task<ExcelImportResult<T>> ImportFromStreamAsync(Stream stream, ExcelImportOptions options = null);
    Task<ExcelValidationResult> ValidateFileAsync(string filePath, ExcelImportOptions options = null);
    Task<bool> GenerateTemplateAsync(string filePath, ExcelTemplateOptions options = null);
    Task<Stream> GetTemplateStreamAsync(ExcelTemplateOptions options = null);
}
```

### 主要类

#### ExcelImportOptions

| 属性 | 类型 | 说明 |
|------|------|------|
| WorksheetName | string | 工作表名称 |
| WorksheetIndex | int | 工作表索引 |
| HeaderRowIndex | int | 标题行索引 |
| DataStartRowIndex | int | 数据开始行索引 |
| MaxRows | int? | 最大处理行数 |
| SkipEmptyRows | bool | 是否跳过空行 |
| StrictMode | bool | 是否启用严格模式 |
| ErrorHandlingStrategy | ExcelErrorHandlingStrategy | 错误处理策略 |
| RequiredColumns | string[] | 必需列 |
| ColumnMappings | Dictionary<string, string> | 列名映射 |
| DataValidator | Func<T, int, ExcelRowValidationResult> | 自定义验证器 |

#### ExcelImportResult<T>

| 属性 | 类型 | 说明 |
|------|------|------|
| IsSuccess | bool | 是否成功 |
| SuccessData | List<T> | 成功导入的数据 |
| Errors | List<ExcelImportError<T>> | 错误列表 |
| Warnings | List<ExcelImportWarning> | 警告列表 |
| Summary | ExcelImportSummary | 导入摘要 |
| ErrorMessage | string | 错误消息 |

#### ExcelTemplateOptions

| 属性 | 类型 | 说明 |
|------|------|------|
| WorksheetName | string | 工作表名称 |
| IncludeDataValidation | bool | 是否包含数据验证 |
| IncludeSampleData | bool | 是否包含示例数据 |
| SampleDataRows | int | 示例数据行数 |
| IncludeInstructions | bool | 是否包含使用说明 |
| ColumnConfigs | List<ExcelColumnConfig> | 列配置 |

### 扩展方法

#### ExcelImporterExtensions

```csharp
// 快速导入
public static async Task<ExcelImportResult<T>> QuickImportAsync<T>(string filePath) where T : class, new()

// 快速验证
public static async Task<ExcelValidationResult> QuickValidateAsync(string filePath)

// 快速生成模板
public static async Task<bool> QuickGenerateTemplateAsync<T>(string filePath) where T : class, new()

// 获取成功数据
public static List<T> GetSuccessData<T>(this ExcelImportResult<T> result)

// 获取数据质量评分
public static double GetDataQualityScore<T>(this ExcelImportResult<T> result)

// 导出错误报告
public static async Task<bool> ExportErrorReportAsync<T>(this ExcelImportResult<T> result, string filePath)

// 检查是否有致命错误
public static bool HasFatalErrors<T>(this ExcelImportResult<T> result)

// 获取错误摘要
public static string GetErrorSummary<T>(this ExcelImportResult<T> result)

// 获取警告摘要
public static string GetWarningSummary<T>(this ExcelImportResult<T> result)
```

### 特性和注解

#### ExcelColumnAttribute

```csharp
[ExcelColumn("列名", Order = 1, Required = true, DefaultValue = "默认值")]
public string PropertyName { get; set; }
```

| 参数 | 类型 | 说明 |
|------|------|------|
| ColumnName | string | Excel列名 |
| Order | int | 列顺序 |
| Required | bool | 是否必需 |
| DefaultValue | object | 默认值 |
| Format | string | 格式化字符串 |
| Description | string | 列描述 |

## 最佳实践

### 1. 数据模型设计

```csharp
public class EmployeeImportDto
{
    [ExcelColumn("员工编号", Order = 1, Required = true)]
    [StringLength(20, ErrorMessage = "员工编号长度不能超过20个字符")]
    public string EmployeeNumber { get; set; }
    
    [ExcelColumn("姓名", Order = 2, Required = true)]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "姓名长度必须在2-50个字符之间")]
    public string Name { get; set; }
    
    [ExcelColumn("出生日期", Order = 3, Format = "yyyy-MM-dd")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
    
    [ExcelColumn("薪资", Order = 4, Format = "#,##0.00")]
    [Range(0, 999999.99, ErrorMessage = "薪资必须在0-999999.99之间")]
    public decimal? Salary { get; set; }
}
```

### 2. 错误处理

```csharp
try
{
    var result = await importer.ImportFromFileAsync(filePath, options);
    
    if (result.IsSuccess)
    {
        // 处理成功的数据
        await ProcessSuccessDataAsync(result.SuccessData);
        
        // 记录导入日志
        logger.LogInformation("导入成功: {Count} 条记录", result.SuccessData.Count);
    }
    
    // 处理错误和警告
    if (result.Errors.Any())
    {
        await HandleImportErrorsAsync(result.Errors);
        await result.ExportErrorReportAsync("error_report.xlsx");
    }
    
    if (result.Warnings.Any())
    {
        await HandleImportWarningsAsync(result.Warnings);
    }
}
catch (Exception ex)
{
    logger.LogError(ex, "Excel导入过程中发生异常");
    throw;
}
```

### 3. 性能优化

```csharp
// 对于大文件，使用流式处理
using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

var options = new ExcelImportOptions
{
    MaxRows = 1000,  // 限制每次处理的行数
    SkipEmptyRows = true,
    ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError
};

var result = await importer.ImportFromStreamAsync(fileStream, options);

// 分批处理数据
const int batchSize = 100;
for (int i = 0; i < result.SuccessData.Count; i += batchSize)
{
    var batch = result.SuccessData.Skip(i).Take(batchSize).ToList();
    await ProcessBatchAsync(batch);
}
```

### 4. 数据验证

```csharp
// 组合使用内置验证和自定义验证
var options = ExcelImporterExtensions.CreateImportOptions()
    .WithDataValidator((data, rowIndex) =>
    {
        var result = new ExcelRowValidationResult { IsValid = true };
        
        if (data is EmployeeImportDto employee)
        {
            // 业务规则验证
            var businessErrors = ValidateBusinessRules(employee);
            if (businessErrors.Any())
            {
                result.IsValid = false;
                result.ErrorMessage = string.Join("; ", businessErrors);
            }
            
            // 数据完整性验证
            var integrityWarnings = ValidateDataIntegrity(employee);
            if (integrityWarnings.Any())
            {
                result.WarningMessage = string.Join("; ", integrityWarnings);
            }
        }
        
        return result;
    })
    .Build();
```

## 常见问题

### Q: 如何处理中文列名？

A: 使用 `ExcelColumnAttribute` 指定中文列名：

```csharp
[ExcelColumn("员工编号")]
public string EmployeeNumber { get; set; }
```

### Q: 如何处理日期格式？

A: 使用 `Format` 属性指定日期格式：

```csharp
[ExcelColumn("入职日期", Format = "yyyy-MM-dd")]
public DateTime? HireDate { get; set; }
```

### Q: 如何跳过某些列？

A: 不为属性添加 `ExcelColumnAttribute` 即可跳过该列。

### Q: 如何处理大文件？

A: 使用流式处理和分批处理：

```csharp
var options = new ExcelImportOptions { MaxRows = 1000 };
var result = await importer.ImportFromStreamAsync(stream, options);
```

### Q: 如何自定义错误消息？

A: 使用数据注解的 `ErrorMessage` 属性：

```csharp
[Required(ErrorMessage = "员工编号不能为空")]
[StringLength(20, ErrorMessage = "员工编号长度不能超过20个字符")]
public string EmployeeNumber { get; set; }
```

## 更新日志

### v1.0.0
- 初始版本发布
- 支持基础Excel导入功能
- 支持数据验证和错误处理
- 支持模板生成
- 提供员工专用导入器
- 包含完整的单元测试

## 许可证

本项目采用 MIT 许可证。详情请参阅 LICENSE 文件。

## 贡献

欢迎提交 Issue 和 Pull Request 来改进这个项目。

## 支持

如果您在使用过程中遇到问题，请通过以下方式获取支持：

1. 查看本文档的常见问题部分
2. 查看项目的 Issues 页面
3. 提交新的 Issue 描述您的问题
4. 联系开发团队

---

*最后更新: 2024年*