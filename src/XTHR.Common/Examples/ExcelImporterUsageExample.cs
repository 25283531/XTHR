using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTHR.Common.Extensions;
using XTHR.Common.Interfaces;
using XTHR.Common.Models;
using XTHR.Common.Services;
using Serilog;

namespace XTHR.Common.Examples
{
    /// <summary>
    /// Excel导入器使用示例
    /// </summary>
    public class ExcelImporterUsageExample
    {
        private readonly ILogger _logger;
        
        public ExcelImporterUsageExample(ILogger? logger = null)
        {
            _logger = logger ?? Log.Logger;
        }
        
        /// <summary>
        /// 基础Excel导入示例
        /// </summary>
        public async Task BasicImportExampleAsync()
        {
            try
            {
                _logger?.Information("开始基础Excel导入示例");
                
                // 1. 创建导入器
                var importer = new ExcelImporter<EmployeeImportDto>();
                
                // 2. 配置导入选项
                var options = ExcelImporterExtensions.CreateImportOptions<EmployeeImportDto>()
                    .WithWorksheet("员工信息")
                    .WithHeaderRow(1)
                    .WithDataStartRow(2)
                    .WithMaxRows(1000)
                    .SkipEmptyRows()
                    .WithStrictMode(false)
                    .WithErrorHandling(ExcelErrorHandlingStrategy.ContinueOnError)
                    .WithRequiredColumns("员工编号", "姓名", "身份证号")
                    .WithColumnMapping("工号", "EmployeeNumber")
                    .WithColumnMapping("真实姓名", "Name")
                    .Build();
                
                // 3. 执行导入
                var filePath = @"d:\temp\employees.xlsx";
                var result = await importer.ImportFromFileAsync(filePath, options);
                
                // 4. 处理结果
                if (result.IsSuccess)
                {
                    _logger?.Information("导入成功: 共{Count}条记录", result.SuccessData.Count);
                    
                    foreach (var employee in result.SuccessData)
                    {
                        _logger?.Information("导入员工: {Name} ({EmployeeNumber})", employee.Name, employee.EmployeeNumber);
                    }
                }
                else
                {
                    _logger?.Error("导入失败: {ErrorMessage}", result.ErrorMessage);
                }
                
                // 5. 处理错误和警告
                if (result.Errors.Any())
                {
                    _logger?.Warning("导入过程中发现{Count}个错误", result.Errors.Count);
                    foreach (var error in result.Errors.Take(5)) // 只显示前5个错误
                    {
                        _logger?.Warning("第{Row}行错误: {Message}", error.RowIndex, error.ErrorMessage);
                    }
                }
                
                if (result.Warnings.Any())
                {
                    _logger?.Information("导入过程中发现{Count}个警告", result.Warnings.Count);
                }
                
                // 6. 导出错误报告
                if (result.Errors.Any())
                {
                    var errorReportPath = @"d:\temp\import_errors.xlsx";
                    var exported = await result.ExportErrorReportAsync(errorReportPath);
                    if (exported)
                    {
                        _logger?.Information("错误报告已导出到: {Path}", errorReportPath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "基础Excel导入示例执行失败");
            }
        }
        
        /// <summary>
        /// 员工Excel导入示例
        /// </summary>
        public async Task EmployeeImportExampleAsync()
        {
            try
            {
                _logger?.Information("开始员工Excel导入示例");
                
                // 1. 创建员工导入器
                var employeeImporter = new EmployeeExcelImporter(_logger);
                
                // 2. 配置员工导入选项
                var options = new EmployeeImportOptions
                {
                    WorksheetName = "员工信息",
                    HeaderRowIndex = 1,
                    DataStartRowIndex = 2,
                    MaxRows = 500,
                    SkipEmptyRows = true,
                    StrictMode = false,
                    ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError,
                    GenerateEmployeeNumber = true,
                    SetDefaultHireDate = true,
                    ColumnMappings = new Dictionary<string, string>
                    {
                        { "工号", "EmployeeNumber" },
                        { "真实姓名", "Name" },
                        { "证件号码", "IdCard" },
                        { "联系电话", "Phone" },
                        { "电子邮箱", "Email" }
                    }
                };
                
                // 3. 验证文件
                var filePath = @"d:\temp\employees.xlsx";
                var validationResult = await employeeImporter.ValidateEmployeeFileAsync(filePath, options);
                
                if (!validationResult.IsValid)
                {
                    _logger?.Error("文件验证失败: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return;
                }
                
                if (validationResult.FileInfo != null)
                {
                    _logger?.Information("文件验证通过: 工作表数量={Count}, 数据行数={Rows}", 
                        validationResult.FileInfo.WorksheetCount, validationResult.FileInfo.DataRows);
                }
                
                // 4. 执行导入
                var importResult = await employeeImporter.ImportEmployeesAsync(filePath, options);
                
                // 5. 处理导入结果
                if (importResult.IsSuccess)
                {
                    _logger?.Information("员工导入成功: 共{Count}条记录", importResult.SuccessData.Count);
                    
                    // 转换为Employee对象并保存到数据库
                    var employees = importResult.SuccessData.Select(dto => dto.ToEmployee()).ToList();
                    
                    // 这里应该调用实际的数据库保存逻辑
                    // await _employeeRepository.BatchInsertAsync(employees);
                    
                    _logger?.Information("员工数据已保存到数据库");
                }
                else
                {
                    _logger?.Error("员工导入失败: {ErrorMessage}", importResult.ErrorMessage);
                }
                
                // 6. 生成导入报告
                await GenerateImportReportAsync(importResult);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "员工Excel导入示例执行失败");
            }
        }
        
        /// <summary>
        /// 生成Excel模板示例
        /// </summary>
        public async Task GenerateTemplateExampleAsync()
        {
            try
            {
                _logger?.Information("开始生成Excel模板示例");
                
                // 1. 创建员工导入器
                var employeeImporter = new EmployeeExcelImporter(_logger);
                
                // 2. 配置模板选项
                var templateOptions = new EmployeeTemplateOptions
                {
                    WorksheetName = "员工信息导入模板",
                    IncludeDataValidation = true,
                    IncludeSampleData = true,
                    SampleDataRows = 3,
                    IncludeInstructions = true
                };
                
                // 3. 生成模板文件
                var templatePath = @"d:\temp\employee_import_template.xlsx";
                var success = await employeeImporter.GenerateEmployeeTemplateAsync(templatePath, templateOptions);
                
                if (success)
                {
                    _logger?.Information("员工导入模板已生成: {Path}", templatePath);
                }
                else
                {
                    _logger?.Error("生成员工导入模板失败");
                }
                
                // 4. 获取模板流（用于Web下载）
                using var templateStream = await employeeImporter.GetEmployeeTemplateStreamAsync(templateOptions);
                
                // 这里可以将流返回给前端下载
                // return File(templateStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "employee_template.xlsx");
                
                _logger?.Information("模板流已生成，大小: {Size} bytes", templateStream.Length);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "生成Excel模板示例执行失败");
            }
        }
        
        /// <summary>
        /// 批量导入示例
        /// </summary>
        public async Task BatchImportExampleAsync()
        {
            try
            {
                _logger?.Information("开始批量导入示例");
                
                var employeeImporter = new EmployeeExcelImporter(_logger);
                var filePaths = new[]
                {
                    @"d:\temp\employees_dept1.xlsx",
                    @"d:\temp\employees_dept2.xlsx",
                    @"d:\temp\employees_dept3.xlsx"
                };
                
                var allResults = new List<ExcelImportResult<EmployeeImportDto>>();
                var totalSuccessCount = 0;
                var totalErrorCount = 0;
                
                foreach (var filePath in filePaths)
                {
                    if (!File.Exists(filePath))
                    {
                        _logger?.Warning("文件不存在，跳过: {FilePath}", filePath);
                        continue;
                    }
                    
                    _logger?.Information("正在导入文件: {FilePath}", filePath);
                    
                    var options = new EmployeeImportOptions
                    {
                        ErrorHandlingStrategy = ExcelErrorHandlingStrategy.ContinueOnError,
                        GenerateEmployeeNumber = true
                    };
                    
                    var result = await employeeImporter.ImportEmployeesAsync(filePath, options);
                    allResults.Add(result);
                    
                    totalSuccessCount += result.SuccessData.Count;
                    totalErrorCount += result.Errors.Count;
                    
                    _logger?.Information("文件导入完成: 成功{Success}条, 错误{Error}条", 
                        result.SuccessData.Count, result.Errors.Count);
                }
                
                _logger?.Information("批量导入完成: 总成功{Success}条, 总错误{Error}条", 
                    totalSuccessCount, totalErrorCount);
                
                // 生成汇总报告
                await GenerateBatchImportSummaryAsync(allResults);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "批量导入示例执行失败");
            }
        }
        
        /// <summary>
        /// 自定义验证示例
        /// </summary>
        public async Task CustomValidationExampleAsync()
        {
            try
            {
                _logger?.Information("开始自定义验证示例");
                
                // 创建带自定义验证的导入选项
                var options = ExcelImporterExtensions.CreateImportOptions<EmployeeImportDto>()
                    .WithWorksheet("员工信息")
                    .WithDataValidator((data, rowIndex) =>
                    {
                        var result = new ExcelRowValidationResult { IsValid = true };
                        
                        if (data is EmployeeImportDto employee)
                        {
                            var errors = new List<string>();
                            var warnings = new List<string>();
                            
                            // 自定义业务规则验证
                            if (!string.IsNullOrEmpty(employee.EmployeeNumber))
                            {
                                // 检查员工编号是否已存在（这里应该查询数据库）
                                if (IsEmployeeNumberExists(employee.EmployeeNumber))
                                {
                                    errors.Add($"员工编号 {employee.EmployeeNumber} 已存在");
                                }
                                
                                // 检查员工编号格式
                                if (!employee.EmployeeNumber.StartsWith("EMP"))
                                {
                                    warnings.Add("建议员工编号以EMP开头");
                                }
                            }
                            
                            // 检查部门是否存在
                            if (!string.IsNullOrEmpty(employee.DepartmentName))
                            {
                                if (!IsDepartmentExists(employee.DepartmentName))
                                {
                                    errors.Add($"部门 {employee.DepartmentName} 不存在");
                                }
                            }
                            
                            // 检查职位是否存在
                            if (!string.IsNullOrEmpty(employee.PositionName))
                            {
                                if (!IsPositionExists(employee.PositionName))
                                {
                                    warnings.Add($"职位 {employee.PositionName} 可能不存在，将创建新职位");
                                }
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
                
                var importer = new ExcelImporter<EmployeeImportDto>();
                var filePath = @"d:\temp\employees.xlsx";
                var result = await importer.ImportFromFileAsync(filePath, options);
                
                _logger?.Information("自定义验证导入完成: 成功{Success}条, 错误{Error}条, 警告{Warning}条", 
                    result.SuccessData.Count, result.Errors.Count, result.Warnings.Count);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "自定义验证示例执行失败");
            }
        }
        
        /// <summary>
        /// 流式导入示例（适用于大文件）
        /// </summary>
        public async Task StreamImportExampleAsync()
        {
            try
            {
                _logger?.Information("开始流式导入示例");
                
                var filePath = @"d:\temp\large_employees.xlsx";
                
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                
                var options = ExcelImporterExtensions.CreateImportOptions<EmployeeImportDto>()
                    .WithWorksheet("员工信息")
                    .WithMaxRows(1000) // 限制每次处理的行数
                    .WithErrorHandling(ExcelErrorHandlingStrategy.ContinueOnError)
                    .Build();
                
                var importer = new ExcelImporter<EmployeeImportDto>();
                var result = await importer.ImportFromStreamAsync(fileStream, options);
                
                _logger?.Information("流式导入完成: 成功{Success}条, 错误{Error}条", 
                    result.SuccessData.Count, result.Errors.Count);
                
                // 分批处理数据（避免内存溢出）
                const int batchSize = 100;
                var batches = result.SuccessData
                    .Select((item, index) => new { item, index })
                    .GroupBy(x => x.index / batchSize)
                    .Select(g => g.Select(x => x.item).ToList());
                
                foreach (var batch in batches)
                {
                    // 处理每批数据
                    await ProcessEmployeeBatchAsync(batch);
                    _logger?.Information("已处理批次: {Count}条记录", batch.Count);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "流式导入示例执行失败");
            }
        }
        
        #region 辅助方法
        
        /// <summary>
        /// 生成导入报告
        /// </summary>
        private async Task GenerateImportReportAsync(ExcelImportResult<EmployeeImportDto> result)
        {
            try
            {
                var reportPath = $@"d:\temp\import_report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                
                var errorReporter = new ExcelErrorReporter<EmployeeImportDto>();
                var success = await errorReporter.ExportErrorReportAsync(result, reportPath);
                
                if (success)
                {
                    _logger?.Information("导入报告已生成: {Path}", reportPath);
                    
                    // 输出摘要信息
                    var qualityScore = result.GetDataQualityScore();
                    var errorSummary = result.GetErrorSummary();
                    var warningSummary = result.GetWarningSummary();
                    
                    _logger?.Information("数据质量评分: {Score}分", qualityScore);
                    _logger?.Information("错误摘要: {Summary}", errorSummary);
                    _logger?.Information("警告摘要: {Summary}", warningSummary);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "生成导入报告失败");
            }
        }
        
        /// <summary>
        /// 生成批量导入汇总
        /// </summary>
        private async Task GenerateBatchImportSummaryAsync(List<ExcelImportResult<EmployeeImportDto>> results)
        {
            try
            {
                var summaryPath = $@"d:\temp\batch_import_summary_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                
                var summary = new List<string>
                {
                    "批量导入汇总报告",
                    $"生成时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                    "",
                    "总体统计:",
                    $"文件数量: {results.Count}",
                    $"总成功记录: {results.Sum(r => r.SuccessData.Count)}",
                    $"总错误记录: {results.Sum(r => r.Errors.Count)}",
                    $"总警告记录: {results.Sum(r => r.Warnings.Count)}",
                    $"平均质量评分: {results.Average(r => r.GetDataQualityScore()):F2}分",
                    ""
                };
                
                summary.Add("各文件详情:");
                for (int i = 0; i < results.Count; i++)
                {
                    var result = results[i];
                    summary.Add($"文件{i + 1}: 成功{result.SuccessData.Count}条, 错误{result.Errors.Count}条, 质量评分{result.GetDataQualityScore():F2}分");
                }
                
                await File.WriteAllLinesAsync(summaryPath, summary);
                _logger?.Information("批量导入汇总已生成: {Path}", summaryPath);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "生成批量导入汇总失败");
            }
        }
        
        /// <summary>
        /// 处理员工批次数据
        /// </summary>
        private async Task ProcessEmployeeBatchAsync(List<EmployeeImportDto> batch)
        {
            // 这里应该实现实际的批次处理逻辑
            // 例如：保存到数据库、发送通知等
            await Task.Delay(100); // 模拟处理时间
        }
        
        /// <summary>
        /// 检查员工编号是否存在（模拟）
        /// </summary>
        private bool IsEmployeeNumberExists(string employeeNumber)
        {
            // 这里应该查询实际的数据库
            var existingNumbers = new[] { "EMP001", "EMP002", "EMP003" };
            return existingNumbers.Contains(employeeNumber);
        }
        
        /// <summary>
        /// 检查部门是否存在（模拟）
        /// </summary>
        private bool IsDepartmentExists(string departmentName)
        {
            // 这里应该查询实际的数据库
            var existingDepartments = new[] { "技术部", "人事部", "财务部", "市场部" };
            return existingDepartments.Contains(departmentName);
        }
        
        /// <summary>
        /// 检查职位是否存在（模拟）
        /// </summary>
        private bool IsPositionExists(string positionName)
        {
            // 这里应该查询实际的数据库
            var existingPositions = new[] { "软件工程师", "产品经理", "UI设计师", "测试工程师" };
            return existingPositions.Contains(positionName);
        }
        
        #endregion
    }
}