using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTHR.Common.Interfaces;
using XTHR.Common.Services;
using Serilog;

namespace XTHR.Common.Extensions
{
    /// <summary>
    /// Excel导入器扩展方法
    /// </summary>
    public static class ExcelImporterExtensions
    {
        /// <summary>
        /// 快速导入Excel文件
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        public static async Task<ExcelImportResult<T>> ImportExcelAsync<T>(this string filePath, ExcelImportOptions<T> options = null) 
            where T : class, new()
        {
            var importer = new ExcelImporter<T>();
            return await importer.ImportFromFileAsync(filePath, options);
        }
        
        /// <summary>
        /// 快速导入Excel流
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="stream">Excel流</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        public static async Task<ExcelImportResult<T>> ImportExcelAsync<T>(this Stream stream, ExcelImportOptions<T> options = null) 
            where T : class, new()
        {
            var importer = new ExcelImporter<T>();
            return await importer.ImportFromStreamAsync(stream, options);
        }
        
        /// <summary>
        /// 生成Excel导入模板
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="filePath">保存路径</param>
        /// <param name="options">模板选项</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> GenerateExcelTemplateAsync<T>(this string filePath, ExcelTemplateOptions options = null) 
            where T : class, new()
        {
            var importer = new ExcelImporter<T>();
            return await importer.GenerateTemplateAsync(filePath, options);
        }
        
        /// <summary>
        /// 验证Excel文件
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="filePath">文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>验证结果</returns>
        public static async Task<ExcelValidationResult> ValidateExcelAsync<T>(this string filePath, ExcelImportOptions<T> options = null) 
            where T : class, new()
        {
            var importer = new ExcelImporter<T>();
            return await importer.ValidateFileAsync(filePath, options);
        }
        
        /// <summary>
        /// 获取成功导入的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">导入结果</param>
        /// <returns>成功数据列表</returns>
        public static List<T> GetSuccessData<T>(this ExcelImportResult<T> result) where T : class
        {
            return result?.SuccessData ?? new List<T>();
        }
        
        /// <summary>
        /// 获取错误信息摘要
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">导入结果</param>
        /// <returns>错误摘要</returns>
        public static string GetErrorSummary<T>(this ExcelImportResult<T> result) where T : class
        {
            if (result?.Errors == null || !result.Errors.Any())
            {
                return "无错误";
            }
            
            var errorGroups = result.Errors.GroupBy(e => e.ErrorType)
                .Select(g => $"{g.Key}: {g.Count()}个")
                .ToList();
            
            return string.Join(", ", errorGroups);
        }
        
        /// <summary>
        /// 获取警告信息摘要
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">导入结果</param>
        /// <returns>警告摘要</returns>
        public static string GetWarningSummary<T>(this ExcelImportResult<T> result) where T : class
        {
            if (result?.Warnings == null || !result.Warnings.Any())
            {
                return "无警告";
            }
            
            var warningGroups = result.Warnings.GroupBy(w => w.WarningType)
                .Select(g => $"{g.Key}: {g.Count()}个")
                .ToList();
            
            return string.Join(", ", warningGroups);
        }
        
        /// <summary>
        /// 导出错误报告到Excel
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">导入结果</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> ExportErrorReportAsync<T>(this ExcelImportResult<T> result, string filePath) where T : class
        {
            try
            {
                if (result?.Errors == null || !result.Errors.Any())
                {
                    return false;
                }
                
                var errorReporter = new ExcelErrorReporter<T>();
                return await errorReporter.ExportErrorReportAsync(result, filePath);
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 检查是否有致命错误
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">导入结果</param>
        /// <returns>是否有致命错误</returns>
        public static bool HasCriticalErrors<T>(this ExcelImportResult<T> result) where T : class
        {
            if (result?.Errors == null)
            {
                return false;
            }
            
            var criticalErrorTypes = new[]
            {
                ExcelErrorType.FileFormat,
                ExcelErrorType.WorksheetNotFound,
                ExcelErrorType.MissingRequiredColumn,
                ExcelErrorType.SystemError
            };
            
            return result.Errors.Any(e => criticalErrorTypes.Contains(e.ErrorType));
        }
        
        /// <summary>
        /// 获取数据质量评分
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="result">导入结果</param>
        /// <returns>质量评分(0-100)</returns>
        public static double GetDataQualityScore<T>(this ExcelImportResult<T> result) where T : class
        {
            if (result?.Summary == null)
            {
                return 0;
            }
            
            var totalRows = result.Summary.TotalRows;
            if (totalRows == 0)
            {
                return 100;
            }
            
            var successRows = result.Summary.SuccessRows;
            var errorRows = result.Summary.ErrorRows;
            var warningRows = result.Summary.WarningRows;
            
            // 基础分数：成功率
            var baseScore = (double)successRows / totalRows * 100;
            
            // 扣分：错误和警告
            var errorPenalty = (double)errorRows / totalRows * 30; // 错误扣分更多
            var warningPenalty = (double)warningRows / totalRows * 10; // 警告扣分较少
            
            var finalScore = Math.Max(0, baseScore - errorPenalty - warningPenalty);
            return Math.Round(finalScore, 2);
        }
        
        /// <summary>
        /// 创建导入选项构建器
        /// </summary>
        /// <returns>选项构建器</returns>
        public static ExcelImportOptionsBuilder<T> CreateImportOptions<T>() where T : class
        {
            return new ExcelImportOptionsBuilder<T>();
        }
        
        /// <summary>
        /// 创建模板选项构建器
        /// </summary>
        /// <returns>模板选项构建器</returns>
        public static ExcelTemplateOptionsBuilder CreateTemplateOptions()
        {
            return new ExcelTemplateOptionsBuilder();
        }
    }
    
    /// <summary>
    /// Excel导入选项构建器
    /// </summary>
    public class ExcelImportOptionsBuilder<T> where T : class
    {
        private readonly ExcelImportOptions<T> _options;
        
        public ExcelImportOptionsBuilder()
        {
            _options = new ExcelImportOptions<T>();
        }
        
        /// <summary>
        /// 设置工作表名称
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithWorksheet(string worksheetName)
        {
            _options.WorksheetName = worksheetName;
            return this;
        }
        
        /// <summary>
        /// 设置工作表索引
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithWorksheet(int worksheetIndex)
        {
            _options.WorksheetIndex = worksheetIndex;
            return this;
        }
        
        /// <summary>
        /// 设置标题行索引
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithHeaderRow(int headerRowIndex)
        {
            _options.HeaderRowIndex = headerRowIndex;
            return this;
        }
        
        /// <summary>
        /// 设置数据开始行索引
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithDataStartRow(int dataStartRowIndex)
        {
            _options.DataStartRowIndex = dataStartRowIndex;
            return this;
        }
        
        /// <summary>
        /// 设置最大行数
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithMaxRows(int maxRows)
        {
            _options.MaxRows = maxRows;
            return this;
        }
        
        /// <summary>
        /// 跳过空行
        /// </summary>
        public ExcelImportOptionsBuilder<T> SkipEmptyRows(bool skip = true)
        {
            _options.SkipEmptyRows = skip;
            return this;
        }
        
        /// <summary>
        /// 启用严格模式
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithStrictMode(bool strict = true)
        {
            _options.StrictMode = strict;
            return this;
        }
        
        /// <summary>
        /// 设置错误处理策略
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithErrorHandling(ExcelErrorHandlingStrategy strategy)
        {
            _options.ErrorHandlingStrategy = strategy;
            return this;
        }
        
        /// <summary>
        /// 添加列映射
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithColumnMapping(string excelColumn, string propertyName)
        {
            _options.ColumnMappings[excelColumn] = propertyName;
            return this;
        }
        
        /// <summary>
        /// 添加必需列
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithRequiredColumn(string columnName)
        {
            _options.RequiredColumns.Add(columnName);
            return this;
        }
        
        /// <summary>
        /// 添加必需列
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithRequiredColumns(params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                _options.RequiredColumns.Add(columnName);
            }
            return this;
        }
        
        /// <summary>
        /// 设置数据验证器
        /// </summary>
        public ExcelImportOptionsBuilder<T> WithDataValidator(Func<object, int, ExcelRowValidationResult> validator)
        {
            _options.DataValidator = validator;
            return this;
        }
        
        /// <summary>
        /// 构建选项
        /// </summary>
        public ExcelImportOptions<T> Build()
        {
            return _options;
        }
    }
    
    /// <summary>
    /// Excel模板选项构建器
    /// </summary>
    public class ExcelTemplateOptionsBuilder
    {
        private readonly ExcelTemplateOptions _options;
        
        public ExcelTemplateOptionsBuilder()
        {
            _options = new ExcelTemplateOptions();
        }
        
        /// <summary>
        /// 设置工作表名称
        /// </summary>
        public ExcelTemplateOptionsBuilder WithWorksheetName(string worksheetName)
        {
            _options.WorksheetName = worksheetName;
            return this;
        }
        
        /// <summary>
        /// 包含数据验证
        /// </summary>
        public ExcelTemplateOptionsBuilder IncludeDataValidation(bool include = true)
        {
            _options.IncludeDataValidation = include;
            return this;
        }
        
        /// <summary>
        /// 包含示例数据
        /// </summary>
        public ExcelTemplateOptionsBuilder IncludeSampleData(bool include = true, int sampleRows = 3)
        {
            _options.IncludeSampleData = include;
            _options.SampleDataRows = sampleRows;
            return this;
        }
        
        /// <summary>
        /// 包含说明
        /// </summary>
        public ExcelTemplateOptionsBuilder IncludeInstructions(bool include = true)
        {
            _options.IncludeInstructions = include;
            return this;
        }
        
        /// <summary>
        /// 添加列配置
        /// </summary>
        public ExcelTemplateOptionsBuilder WithColumn(ExcelColumnConfig columnConfig)
        {
            _options.ColumnConfigs.Add(columnConfig);
            return this;
        }
        
        /// <summary>
        /// 添加列配置
        /// </summary>
        public ExcelTemplateOptionsBuilder WithColumn(string columnName, string displayName, bool isRequired = false, double width = 15)
        {
            _options.ColumnConfigs.Add(new ExcelColumnConfig
            {
                ColumnName = columnName,
                DisplayName = displayName,
                IsRequired = isRequired,
                Width = width
            });
            return this;
        }
        
        /// <summary>
        /// 构建选项
        /// </summary>
        public ExcelTemplateOptions Build()
        {
            return _options;
        }
    }
}