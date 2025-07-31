using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using OfficeOpenXml;
using XTHR.Common.Extensions;
using XTHR.Common.Interfaces;
using XTHR.Common.Models;
using XTHR.Common.Services;
using Serilog;

namespace XTHR.Tests
{
    /// <summary>
    /// Excel导入器单元测试
    /// </summary>
    
    public class ExcelImporterTests : IDisposable
    {
        private ILogger _logger;
        private string _testDataDirectory;
        
        public ExcelImporterTests()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
                
            _testDataDirectory = Path.Combine(Path.GetTempPath(), "ExcelImporterTests");
            Directory.CreateDirectory(_testDataDirectory);
            
            // 设置EPPlus许可证
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        
        public void Dispose()
        {
            if (Directory.Exists(_testDataDirectory))
            {
                Directory.Delete(_testDataDirectory, true);
            }
        }
        
        #region 基础导入测试
        
        [Fact]
        public async Task ImportFromFile_ValidData_ShouldSucceed()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("valid_employees.xlsx", 5);
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            
            // Act
            var result = await importer.ImportFromFileAsync(testFile, options);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(5, result.SuccessData.Count);
            Assert.Empty(result.Errors);
            Assert.All(result.SuccessData, e => Assert.False(string.IsNullOrEmpty(e.Name)));
        }
        
        [Fact]
        public async Task ImportFromFile_InvalidFile_ShouldFail()
        {
            // Arrange
            var invalidFile = Path.Combine(_testDataDirectory, "invalid.txt");
            await File.WriteAllTextAsync(invalidFile, "This is not an Excel file");
            
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            
            // Act
            var result = await importer.ImportFromFileAsync(invalidFile, options);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ErrorMessage);
        }
        
        [Fact]
        public async Task ImportFromFile_NonExistentFile_ShouldFail()
        {
            // Arrange
            var nonExistentFile = Path.Combine(_testDataDirectory, "nonexistent.xlsx");
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            
            // Act
            var result = await importer.ImportFromFileAsync(nonExistentFile, options);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.ErrorMessage);
        }
        
        [Fact]
        public async Task ImportFromStream_ValidData_ShouldSucceed()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("stream_test.xlsx", 3);
            using var fileStream = new FileStream(testFile, FileMode.Open, FileAccess.Read);
            
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            
            // Act
            var result = await importer.ImportFromStreamAsync(fileStream, options);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.SuccessData.Count);
        }
        
        #endregion
        
        #region 数据验证测试
        
        [Fact]
        public async Task ImportFromFile_WithValidationErrors_ShouldContinueOnError()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileWithErrorsAsync("validation_errors.xlsx");
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            options.ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError;
            
            // Act
            var result = await importer.ImportFromFileAsync(testFile, options);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.SuccessData.Count > 0);
            Assert.NotEmpty(result.Errors);
        }
        
        [Fact]
        public async Task ImportFromFile_WithValidationErrors_ShouldStopOnFirstError()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileWithErrorsAsync("stop_on_error.xlsx");
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            options.ErrorHandlingStrategy = ExcelErrorHandlingStrategy.StopOnFirstError;
            
            // Act
            var result = await importer.ImportFromFileAsync(testFile, options);
            
            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
        }
        
        [TestMethod]
        public async Task ImportFromFile_WithCustomValidator_ShouldApplyCustomRules()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("custom_validation.xlsx", 3);
            var importer = new ExcelImporter<EmployeeImportDto>();
            
            var options = ExcelImporterExtensions.CreateImportOptions<EmployeeImportDto>()
                .WithWorksheet("员工信息")
                .WithDataValidator((data, rowIndex) =>
                {
                    var result = new ExcelRowValidationResult { IsValid = true };
                    if (data is EmployeeImportDto employee)
                    {
                        if (string.IsNullOrEmpty(employee.Name) || employee.Name.Length < 2)
                        {
                            result.IsValid = false;
                            result.ErrorMessage = "姓名长度不能少于2个字符";
                        }
                    }
                    return result;
                })
                .Build();
            
            // Act
            var result = await importer.ImportFromFileAsync(testFile, options);
            
            // Assert
            Assert.True(result.IsSuccess);
            // 验证自定义验证规则是否生效
        }
        
        #endregion
        
        #region 模板生成测试
        
        [TestMethod]
        public async Task GenerateTemplate_WithDefaultOptions_ShouldCreateValidTemplate()
        {
            // Arrange
            var importer = new ExcelImporter<EmployeeImportDto>();
            var templatePath = Path.Combine(_testDataDirectory, "template.xlsx");
            var options = CreateDefaultTemplateOptions();
            
            // Act
            var success = await importer.GenerateTemplateAsync(templatePath, options);
            
            // Assert
            Assert.True(success);
            Assert.True(File.Exists(templatePath));
            
            // 验证模板内容
            using var package = new ExcelPackage(new FileInfo(templatePath));
            var worksheet = package.Workbook.Worksheets[0];
            Assert.NotNull(worksheet);
            Assert.Contains("员工编号", worksheet.Cells[1, 1].Value?.ToString());
        }
        
        [TestMethod]
        public async Task GetTemplateStream_ShouldReturnValidStream()
        {
            // Arrange
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultTemplateOptions();
            
            // Act
            using var stream = await importer.GetTemplateStreamAsync(options);
            
            // Assert
            Assert.NotNull(stream);
            Assert.True(stream.Length > 0);
            Assert.True(stream.CanRead);
        }
        
        #endregion
        
        #region 员工导入器测试
        
        [TestMethod]
        public async Task EmployeeImporter_ImportEmployees_ShouldSucceed()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("employee_import.xlsx", 5);
            var employeeImporter = new EmployeeExcelImporter(_logger);
            var options = new EmployeeImportOptions
            {
                GenerateEmployeeNumber = true,
                SetDefaultHireDate = true
            };
            
            // Act
            var result = await employeeImporter.ImportEmployeesAsync(testFile, options);
            
            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(5, result.SuccessData.Count);
            Assert.All(result.SuccessData, e => Assert.False(string.IsNullOrEmpty(e.EmployeeNumber)));
            Assert.All(result.SuccessData, e => Assert.True(e.HireDate.HasValue));
        }
        
        [TestMethod]
        public async Task EmployeeImporter_ValidateFile_ShouldReturnValidationResult()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("validation_test.xlsx", 3);
            var employeeImporter = new EmployeeExcelImporter(_logger);
            var options = new EmployeeImportOptions();
            
            // Act
            var result = await employeeImporter.ValidateEmployeeFileAsync(testFile, options);
            
            // Assert
            Assert.True(result.IsValid);
            Assert.NotNull(result.FileInfo);
            Assert.True(result.FileInfo.DataRows > 0);
        }
        
        [TestMethod]
        public async Task EmployeeImporter_GenerateTemplate_ShouldCreateEmployeeTemplate()
        {
            // Arrange
            var employeeImporter = new EmployeeExcelImporter(_logger);
            var templatePath = Path.Combine(_testDataDirectory, "employee_template.xlsx");
            var options = new EmployeeTemplateOptions
            {
                IncludeSampleData = true,
                SampleDataRows = 2
            };
            
            // Act
            var success = await employeeImporter.GenerateEmployeeTemplateAsync(templatePath, options);
            
            // Assert
            Assert.True(success);
            Assert.True(File.Exists(templatePath));
            
            // 验证模板包含员工特定的列
            using var package = new ExcelPackage(new FileInfo(templatePath));
            var worksheet = package.Workbook.Worksheets[0];
            var headerRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column];
            var headers = headerRow.Select(c => c.Value?.ToString()).ToList();
            
            Assert.Contains("员工编号", headers);
            Assert.Contains("姓名", headers);
            Assert.Contains("身份证号", headers);
        }
        
        #endregion
        
        #region 扩展方法测试
        
        [TestMethod]
        public async Task ExcelImporterExtensions_QuickImport_ShouldWork()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("quick_import.xlsx", 3);
            
            // Act
            var result = await ExcelImporterExtensions.QuickImportAsync<EmployeeImportDto>(testFile);
            
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.Equal(3, result.SuccessData.Count);
        }
        
        [TestMethod]
        public async Task ExcelImporterExtensions_QuickValidate_ShouldWork()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("quick_validate.xlsx", 2);
            
            // Act
            var result = await ExcelImporterExtensions.QuickValidateAsync(testFile);
            
            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNotNull(result.FileInfo);
        }
        
        [TestMethod]
        public async Task ExcelImporterExtensions_QuickTemplate_ShouldWork()
        {
            // Arrange
            var templatePath = Path.Combine(_testDataDirectory, "quick_template.xlsx");
            
            // Act
            var success = await ExcelImporterExtensions.QuickGenerateTemplateAsync<EmployeeImportDto>(templatePath);
            
            // Assert
            Assert.True(success);
            Assert.True(File.Exists(templatePath));
        }
        
        [TestMethod]
        public void ExcelImportResult_GetDataQualityScore_ShouldCalculateCorrectly()
        {
            // Arrange
            var result = new ExcelImportResult<EmployeeImportDto>
            {
                IsSuccess = true,
                SuccessData = new List<EmployeeImportDto> { new(), new(), new() },
                Errors = new List<ExcelImportError<EmployeeImportDto>> { new() },
                Warnings = new List<ExcelImportWarning> { new(), new() }
            };
            
            // Act
            var score = result.GetDataQualityScore();
            
            // Assert
            Assert.InRange(score, 0, 100);
            Assert.True(score < 100);
        }
        
        [TestMethod]
        public void ExcelImportResult_GetErrorSummary_ShouldReturnSummary()
        {
            // Arrange
            var result = new ExcelImportResult<EmployeeImportDto>
            {
                Errors = new List<ExcelImportError<EmployeeImportDto>>
                {
                    new() { ErrorType = ExcelErrorType.ValidationError },
                    new() { ErrorType = ExcelErrorType.ValidationError },
                    new() { ErrorType = ExcelErrorType.DataTypeError }
                }
            };
            
            // Act
            var summary = result.GetErrorSummary();
            
            // Assert
            Assert.NotNull(summary);
            Assert.Contains("ValidationError", summary);
            Assert.Contains("DataTypeError", summary);
        }
        
        #endregion
        
        #region 错误处理测试
        
        [TestMethod]
        public async Task ImportFromFile_WithMaxRowsLimit_ShouldRespectLimit()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileAsync("max_rows_test.xlsx", 10);
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            options.MaxRows = 5;
            
            // Act
            var result = await importer.ImportFromFileAsync(testFile, options);
            
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.True(result.SuccessData.Count <= 5);
        }
        
        [TestMethod]
        public async Task ImportFromFile_WithEmptyRows_ShouldSkipWhenConfigured()
        {
            // Arrange
            var testFile = await CreateTestEmployeeFileWithEmptyRowsAsync("empty_rows_test.xlsx");
            var importer = new ExcelImporter<EmployeeImportDto>();
            var options = CreateDefaultImportOptions();
            options.SkipEmptyRows = true;
            
            // Act
            var result = await importer.ImportFromFileAsync(testFile, options);
            
            // Assert
            Assert.IsTrue(result.IsSuccess);
            // 验证空行被跳过
        }
        
        [TestMethod]
        public async Task ErrorReporter_ExportErrorReport_ShouldCreateReport()
        {
            // Arrange
            var result = new ExcelImportResult<EmployeeImportDto>
            {
                IsSuccess = false,
                SuccessData = new List<EmployeeImportDto> { new() { Name = "张三" } },
                Errors = new List<ExcelImportError<EmployeeImportDto>>
                {
                    new()
                    {
                        RowIndex = 2,
                        ErrorType = ExcelErrorType.ValidationError,
                        ErrorMessage = "测试错误",
                        Data = new EmployeeImportDto { Name = "李四" }
                    }
                },
                Warnings = new List<ExcelImportWarning>
                {
                    new() { RowIndex = 3, WarningType = ExcelWarningType.DataQualityWarning, WarningMessage = "测试警告" }
                }
            };
            
            var reportPath = Path.Combine(_testDataDirectory, "error_report.xlsx");
            var errorReporter = new ExcelErrorReporter<EmployeeImportDto>();
            
            // Act
            var success = await errorReporter.ExportErrorReportAsync(result, reportPath);
            
            // Assert
            Assert.IsTrue(success);
            Assert.IsTrue(File.Exists(reportPath));
            
            // 验证报告内容
            using var package = new ExcelPackage(new FileInfo(reportPath));
            Assert.True(package.Workbook.Worksheets.Count > 0);
        }
        
        #endregion
        
        #region 辅助方法
        
        /// <summary>
        /// 创建默认导入选项
        /// </summary>
        private ExcelImportOptions CreateDefaultImportOptions()
        {
            return new ExcelImportOptions
            {
                WorksheetName = "员工信息",
                HeaderRowIndex = 1,
                DataStartRowIndex = 2,
                SkipEmptyRows = true,
                StrictMode = false,
                ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError
            };
        }
        
        /// <summary>
        /// 创建默认模板选项
        /// </summary>
        private ExcelTemplateOptions CreateDefaultTemplateOptions()
        {
            return new ExcelTemplateOptions
            {
                WorksheetName = "员工信息",
                IncludeDataValidation = true,
                IncludeSampleData = false
            };
        }
        
        /// <summary>
        /// 创建测试员工Excel文件
        /// </summary>
        private async Task<string> CreateTestEmployeeFileAsync(string fileName, int rowCount)
        {
            var filePath = Path.Combine(_testDataDirectory, fileName);
            
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("员工信息");
            
            // 添加标题行
            worksheet.Cells[1, 1].Value = "员工编号";
            worksheet.Cells[1, 2].Value = "姓名";
            worksheet.Cells[1, 3].Value = "身份证号";
            worksheet.Cells[1, 4].Value = "手机号";
            worksheet.Cells[1, 5].Value = "邮箱";
            worksheet.Cells[1, 6].Value = "部门";
            worksheet.Cells[1, 7].Value = "职位";
            worksheet.Cells[1, 8].Value = "入职日期";
            
            // 添加数据行
            for (int i = 0; i < rowCount; i++)
            {
                var row = i + 2;
                worksheet.Cells[row, 1].Value = $"EMP{i + 1:000}";
                worksheet.Cells[row, 2].Value = $"员工{i + 1}";
                worksheet.Cells[row, 3].Value = $"11010119900101{i + 1:000}X";
                worksheet.Cells[row, 4].Value = $"1380000{i + 1:0000}";
                worksheet.Cells[row, 5].Value = $"employee{i + 1}@company.com";
                worksheet.Cells[row, 6].Value = "技术部";
                worksheet.Cells[row, 7].Value = "软件工程师";
                worksheet.Cells[row, 8].Value = DateTime.Now.AddDays(-i * 30).ToString("yyyy-MM-dd");
            }
            
            await package.SaveAsAsync(new FileInfo(filePath));
            return filePath;
        }
        
        /// <summary>
        /// 创建包含错误的测试员工Excel文件
        /// </summary>
        private async Task<string> CreateTestEmployeeFileWithErrorsAsync(string fileName)
        {
            var filePath = Path.Combine(_testDataDirectory, fileName);
            
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("员工信息");
            
            // 添加标题行
            worksheet.Cells[1, 1].Value = "员工编号";
            worksheet.Cells[1, 2].Value = "姓名";
            worksheet.Cells[1, 3].Value = "身份证号";
            worksheet.Cells[1, 4].Value = "手机号";
            worksheet.Cells[1, 5].Value = "邮箱";
            
            // 添加正确的数据行
            worksheet.Cells[2, 1].Value = "EMP001";
            worksheet.Cells[2, 2].Value = "张三";
            worksheet.Cells[2, 3].Value = "110101199001011234";
            worksheet.Cells[2, 4].Value = "13800001234";
            worksheet.Cells[2, 5].Value = "zhangsan@company.com";
            
            // 添加错误的数据行
            worksheet.Cells[3, 1].Value = ""; // 空员工编号
            worksheet.Cells[3, 2].Value = "李四";
            worksheet.Cells[3, 3].Value = "invalid_id"; // 无效身份证号
            worksheet.Cells[3, 4].Value = "invalid_phone"; // 无效手机号
            worksheet.Cells[3, 5].Value = "invalid_email"; // 无效邮箱
            
            // 添加另一个正确的数据行
            worksheet.Cells[4, 1].Value = "EMP003";
            worksheet.Cells[4, 2].Value = "王五";
            worksheet.Cells[4, 3].Value = "110101199001015678";
            worksheet.Cells[4, 4].Value = "13800005678";
            worksheet.Cells[4, 5].Value = "wangwu@company.com";
            
            await package.SaveAsAsync(new FileInfo(filePath));
            return filePath;
        }
        
        /// <summary>
        /// 创建包含空行的测试员工Excel文件
        /// </summary>
        private async Task<string> CreateTestEmployeeFileWithEmptyRowsAsync(string fileName)
        {
            var filePath = Path.Combine(_testDataDirectory, fileName);
            
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("员工信息");
            
            // 添加标题行
            worksheet.Cells[1, 1].Value = "员工编号";
            worksheet.Cells[1, 2].Value = "姓名";
            
            // 添加数据行
            worksheet.Cells[2, 1].Value = "EMP001";
            worksheet.Cells[2, 2].Value = "张三";
            
            // 空行（第3行）
            
            worksheet.Cells[4, 1].Value = "EMP002";
            worksheet.Cells[4, 2].Value = "李四";
            
            // 另一个空行（第5行）
            
            worksheet.Cells[6, 1].Value = "EMP003";
            worksheet.Cells[6, 2].Value = "王五";
            
            await package.SaveAsAsync(new FileInfo(filePath));
            return filePath;
        }
        
        #endregion
    }
}