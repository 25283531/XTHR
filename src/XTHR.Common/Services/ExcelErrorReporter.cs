using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using XTHR.Common.Interfaces;
using Serilog;

namespace XTHR.Common.Services
{
    /// <summary>
    /// Excel错误报告器
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ExcelErrorReporter<T> where T : class
    {
        private readonly ILogger? _logger;
        
        public ExcelErrorReporter(ILogger? logger = null)
        {
            _logger = logger ?? Log.Logger;
        }
        
        /// <summary>
        /// 导出错误报告到Excel文件
        /// </summary>
        /// <param name="importResult">导入结果</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ExportErrorReportAsync(ExcelImportResult<T> importResult, string filePath)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                
                using var package = new ExcelPackage();
                
                // 创建摘要工作表
                CreateSummaryWorksheet(package, importResult);
                
                // 创建错误详情工作表
                if (importResult.Errors?.Any() == true)
                {
                    CreateErrorDetailsWorksheet(package, importResult.Errors);
                }
                
                // 创建警告详情工作表
                if (importResult.Warnings?.Any() == true)
                {
                    CreateWarningDetailsWorksheet(package, importResult.Warnings);
                }
                
                // 创建数据质量分析工作表
                CreateDataQualityAnalysisWorksheet(package, importResult);
                
                // 保存文件
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                await package.SaveAsAsync(new FileInfo(filePath));
                
                _logger?.Information("错误报告已导出到: {FilePath}", filePath);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "导出错误报告时发生错误: {FilePath}", filePath);
                return false;
            }
        }
        
        /// <summary>
        /// 创建摘要工作表
        /// </summary>
        private void CreateSummaryWorksheet(ExcelPackage package, ExcelImportResult<T> importResult)
        {
            var worksheet = package.Workbook.Worksheets.Add("导入摘要");
            
            var row = 1;
            
            // 标题
            worksheet.Cells[row, 1].Value = "Excel导入结果摘要";
            worksheet.Cells[row, 1].Style.Font.Size = 16;
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            worksheet.Cells[row, 1, row, 4].Merge = true;
            row += 2;
            
            // 基本信息
            AddSummaryRow(worksheet, ref row, "导入状态", importResult.IsSuccess ? "成功" : "失败");
            AddSummaryRow(worksheet, ref row, "处理时间", importResult.Summary?.ProcessingTime.ToString(@"hh\:mm\:ss\.fff") ?? "未知");
            AddSummaryRow(worksheet, ref row, "总行数", importResult.Summary?.TotalRows.ToString() ?? "0");
            AddSummaryRow(worksheet, ref row, "成功行数", importResult.Summary?.SuccessRows.ToString() ?? "0");
            AddSummaryRow(worksheet, ref row, "错误行数", importResult.Summary?.ErrorRows.ToString() ?? "0");
            AddSummaryRow(worksheet, ref row, "警告行数", importResult.Summary?.WarningRows.ToString() ?? "0");
            AddSummaryRow(worksheet, ref row, "跳过行数", importResult.Summary?.SkippedRows.ToString() ?? "0");
            
            if (!string.IsNullOrEmpty(importResult.ErrorMessage))
            {
                AddSummaryRow(worksheet, ref row, "错误信息", importResult.ErrorMessage);
            }
            
            row++;
            
            // 错误统计
            if (importResult.Errors?.Any() == true)
            {
                worksheet.Cells[row, 1].Value = "错误类型统计";
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                row++;
                
                var errorStats = importResult.Errors
                    .GroupBy(e => e.ErrorType)
                    .Select(g => new { Type = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);
                
                foreach (var stat in errorStats)
                {
                    AddSummaryRow(worksheet, ref row, GetErrorTypeDisplayName(stat.Type), stat.Count.ToString());
                }
                
                row++;
            }
            
            // 警告统计
            if (importResult.Warnings?.Any() == true)
            {
                worksheet.Cells[row, 1].Value = "警告类型统计";
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                row++;
                
                var warningStats = importResult.Warnings
                    .GroupBy(w => w.WarningType)
                    .Select(g => new { Type = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count);
                
                foreach (var stat in warningStats)
                {
                    AddSummaryRow(worksheet, ref row, GetWarningTypeDisplayName(stat.Type), stat.Count.ToString());
                }
            }
            
            // 设置列宽
            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 30;
            
            // 设置边框
            var range = worksheet.Cells[1, 1, row - 1, 2];
            range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }
        
        /// <summary>
        /// 创建错误详情工作表
        /// </summary>
        private void CreateErrorDetailsWorksheet(ExcelPackage package, List<ExcelImportError<T>> errors)
        {
            var worksheet = package.Workbook.Worksheets.Add("错误详情");

            // 创建标题行
            var headers = new[] { "行号", "错误类型", "字段名", "错误信息", "原始值" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            // 填充错误数据
            int rowIndex = 2;
            foreach (var error in errors)
            {
                worksheet.Cells[rowIndex, 1].Value = error.RowIndex;
                worksheet.Cells[rowIndex, 2].Value = GetErrorTypeDisplayName(error.ErrorType);
                worksheet.Cells[rowIndex, 3].Value = error.ColumnName ?? "";
                worksheet.Cells[rowIndex, 4].Value = error.ErrorMessage;
                worksheet.Cells[rowIndex, 5].Value = error.OriginalValue?.ToString() ?? "";

                var rowRange = worksheet.Cells[rowIndex, 1, rowIndex, headers.Length];
                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(GetRowColorForErrorType(error.ErrorType));
                rowRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                rowIndex++;
            }

            // 设置列宽
            worksheet.Column(1).Width = 8;  // 行号
            worksheet.Column(2).Width = 15; // 错误类型
            worksheet.Column(3).Width = 15; // 字段名
            worksheet.Column(4).Width = 40; // 错误信息
            worksheet.Column(5).Width = 20; // 原始值

            // 自动筛选
            worksheet.Cells[1, 1, errors.Count + 1, headers.Length].AutoFilter = true;
        }

        private System.Drawing.Color GetRowColorForErrorType(ExcelErrorType errorType)
        {
            return errorType switch
            {
                ExcelErrorType.ValidationFailed => System.Drawing.Color.MistyRose,
                ExcelErrorType.DataTypeError => System.Drawing.Color.LightCoral,
                ExcelErrorType.MissingRequiredColumn => System.Drawing.Color.LightPink,
                ExcelErrorType.SystemError => System.Drawing.Color.LightYellow,
                _ => System.Drawing.Color.White,
            };
        }
        
        /// <summary>
        /// 创建警告详情工作表
        /// </summary>
        private void CreateWarningDetailsWorksheet(ExcelPackage package, List<ExcelImportWarning> warnings)
        {
            var worksheet = package.Workbook.Worksheets.Add("警告详情");
            
            // 创建标题行
            var headers = new[] { "行号", "警告类型", "警告信息", "发生时间" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
            
            // 填充警告数据
            for (int i = 0; i < warnings.Count; i++)
            {
                var warning = warnings[i];
                var row = i + 2;
                
                worksheet.Cells[row, 1].Value = warning.RowIndex;
                worksheet.Cells[row, 2].Value = GetWarningTypeDisplayName(warning.WarningType);
                worksheet.Cells[row, 3].Value = warning.WarningMessage;
                worksheet.Cells[row, 4].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                // 设置警告行颜色
                var rowRange = worksheet.Cells[row, 1, row, headers.Length];
                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);
                rowRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
            
            // 设置列宽
            worksheet.Column(1).Width = 8;  // 行号
            worksheet.Column(2).Width = 15; // 警告类型
            worksheet.Column(3).Width = 40; // 警告信息
            worksheet.Column(4).Width = 20; // 发生时间
            
            // 自动筛选
            worksheet.Cells[1, 1, warnings.Count + 1, headers.Length].AutoFilter = true;
        }
        
        /// <summary>
        /// 创建数据质量分析工作表
        /// </summary>
        private void CreateDataQualityAnalysisWorksheet(ExcelPackage package, ExcelImportResult<T> importResult)
        {
            var worksheet = package.Workbook.Worksheets.Add("数据质量分析");
            
            var row = 1;
            
            // 标题
            worksheet.Cells[row, 1].Value = "数据质量分析报告";
            worksheet.Cells[row, 1].Style.Font.Size = 16;
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            worksheet.Cells[row, 1, row, 4].Merge = true;
            row += 2;
            
            // 质量评分
            var qualityScore = CalculateQualityScore(importResult);
            AddAnalysisRow(worksheet, ref row, "数据质量评分", $"{qualityScore:F2}分", GetQualityScoreColor(qualityScore));
            
            // 完整性分析
            var completeness = CalculateCompleteness(importResult);
            AddAnalysisRow(worksheet, ref row, "数据完整性", $"{completeness:F2}%", GetCompletenessColor(completeness));
            
            // 准确性分析
            var accuracy = CalculateAccuracy(importResult);
            AddAnalysisRow(worksheet, ref row, "数据准确性", $"{accuracy:F2}%", GetAccuracyColor(accuracy));
            
            row++;
            
            // 问题分布
            worksheet.Cells[row, 1].Value = "问题分布分析";
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            row++;
            
            if (importResult.Errors?.Any() == true)
            {
                var errorsByRow = importResult.Errors
                    .GroupBy(e => e.RowIndex)
                    .Select(g => new { RowIndex = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(10);
                
                worksheet.Cells[row, 1].Value = "错误最多的行(前10)";
                worksheet.Cells[row, 1].Style.Font.Bold = true;
                row++;
                
                foreach (var errorRow in errorsByRow)
                {
                    AddAnalysisRow(worksheet, ref row, $"第{errorRow.RowIndex}行", $"{errorRow.Count}个错误", System.Drawing.Color.LightCoral);
                }
                
                row++;
            }
            
            // 改进建议
            worksheet.Cells[row, 1].Value = "改进建议";
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            row++;
            
            var suggestions = GenerateImprovementSuggestions(importResult);
            foreach (var suggestion in suggestions)
            {
                worksheet.Cells[row, 1].Value = $"• {suggestion}";
                row++;
            }
            
            // 设置列宽
            worksheet.Column(1).Width = 25;
            worksheet.Column(2).Width = 20;
        }
        
        #region 辅助方法
        
        /// <summary>
        /// 添加摘要行
        /// </summary>
        private void AddSummaryRow(ExcelWorksheet worksheet, ref int row, string label, string value)
        {
            worksheet.Cells[row, 1].Value = label;
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            worksheet.Cells[row, 2].Value = value;
            row++;
        }
        
        /// <summary>
        /// 添加分析行
        /// </summary>
        private void AddAnalysisRow(ExcelWorksheet worksheet, ref int row, string label, string value, System.Drawing.Color? backgroundColor = null)
        {
            worksheet.Cells[row, 1].Value = label;
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            worksheet.Cells[row, 2].Value = value;
            
            if (backgroundColor.HasValue)
            {
                worksheet.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(backgroundColor.Value);
            }
            
            row++;
        }
        
        /// <summary>
        /// 获取错误类型显示名称
        /// </summary>
        private string GetErrorTypeDisplayName(ExcelErrorType errorType)
        {
            return errorType switch
            {
                ExcelErrorType.FileFormat => "文件格式错误",
                ExcelErrorType.WorksheetNotFound => "工作表未找到",
                ExcelErrorType.MissingRequiredColumn => "缺少必需列",
                ExcelErrorType.DataTypeError => "数据类型错误",
                ExcelErrorType.ValidationFailed => "数据验证失败",
                ExcelErrorType.SystemError => "系统错误",
                _ => errorType.ToString()
            };
        }
        
        /// <summary>
        /// 获取警告类型显示名称
        /// </summary>
        private string GetWarningTypeDisplayName(ExcelWarningType warningType)
        {
            return warningType switch
            {
                ExcelWarningType.EmptyRowSkipped => "跳过空行",
                ExcelWarningType.DataConverted => "数据已转换",
                ExcelWarningType.DataTruncated => "数据被截断",
                ExcelWarningType.ExtraColumnIgnored => "额外列被忽略",
                ExcelWarningType.FormatInconsistent => "格式不一致",
                _ => warningType.ToString()
            };
        }
        
        /// <summary>
        /// 计算质量评分
        /// </summary>
        private double CalculateQualityScore(ExcelImportResult<T> importResult)
        {
            if (importResult?.Summary == null || importResult.Summary.TotalRows == 0)
            {
                return 0;
            }
            
            var totalRows = importResult.Summary.TotalRows;
            var successRows = importResult.Summary.SuccessRows;
            var errorRows = importResult.Summary.ErrorRows;
            var warningRows = importResult.Summary.WarningRows;
            
            var baseScore = (double)successRows / totalRows * 100;
            var errorPenalty = (double)errorRows / totalRows * 30;
            var warningPenalty = (double)warningRows / totalRows * 10;
            
            return Math.Max(0, baseScore - errorPenalty - warningPenalty);
        }
        
        /// <summary>
        /// 计算完整性
        /// </summary>
        private double CalculateCompleteness(ExcelImportResult<T> importResult)
        {
            if (importResult?.Summary == null || importResult.Summary.TotalRows == 0)
            {
                return 0;
            }
            
            var totalRows = importResult.Summary.TotalRows;
            var skippedRows = importResult.Summary.SkippedRows;
            
            return (double)(totalRows - skippedRows) / totalRows * 100;
        }
        
        /// <summary>
        /// 计算准确性
        /// </summary>
        private double CalculateAccuracy(ExcelImportResult<T> importResult)
        {
            if (importResult?.Summary == null || importResult.Summary.TotalRows == 0)
            {
                return 0;
            }
            
            var totalRows = importResult.Summary.TotalRows;
            var errorRows = importResult.Summary.ErrorRows;
            
            return (double)(totalRows - errorRows) / totalRows * 100;
        }
        
        /// <summary>
        /// 获取质量评分颜色
        /// </summary>
        private System.Drawing.Color GetQualityScoreColor(double score)
        {
            if (score >= 90) return System.Drawing.Color.LightGreen;
            if (score >= 70) return System.Drawing.Color.LightYellow;
            return System.Drawing.Color.LightCoral;
        }
        
        /// <summary>
        /// 获取完整性颜色
        /// </summary>
        private System.Drawing.Color GetCompletenessColor(double completeness)
        {
            if (completeness >= 95) return System.Drawing.Color.LightGreen;
            if (completeness >= 80) return System.Drawing.Color.LightYellow;
            return System.Drawing.Color.LightCoral;
        }
        
        /// <summary>
        /// 获取准确性颜色
        /// </summary>
        private System.Drawing.Color GetAccuracyColor(double accuracy)
        {
            if (accuracy >= 95) return System.Drawing.Color.LightGreen;
            if (accuracy >= 85) return System.Drawing.Color.LightYellow;
            return System.Drawing.Color.LightCoral;
        }
        
        /// <summary>
        /// 生成改进建议
        /// </summary>
        private List<string> GenerateImprovementSuggestions(ExcelImportResult<T> importResult)
        {
            var suggestions = new List<string>();
            
            if (importResult?.Errors == null)
            {
                return suggestions;
            }
            
            // 基于错误类型生成建议
            var errorTypes = importResult.Errors.Select(e => e.ErrorType).Distinct();
            
            foreach (var errorType in errorTypes)
            {
                switch (errorType)
                {
                    case ExcelErrorType.DataTypeError:
                        suggestions.Add("检查数据格式，确保数值、日期等字段格式正确");
                        break;
                    case ExcelErrorType.ValidationFailed:
                        suggestions.Add("验证数据完整性，确保必填字段不为空");
                        break;
                    case ExcelErrorType.MissingRequiredColumn:
                        suggestions.Add("确保Excel文件包含所有必需的列");
                        break;
                    case ExcelErrorType.FileFormat:
                        suggestions.Add("使用正确的Excel文件格式(.xlsx)");
                        break;
                }
            }
            
            // 基于数据质量生成建议
            var qualityScore = CalculateQualityScore(importResult);
            if (qualityScore < 70)
            {
                suggestions.Add("数据质量较低，建议在导入前进行数据清洗");
            }
            
            if (importResult.Summary?.SkippedRows > 0)
            {
                suggestions.Add("存在空行，建议清理Excel文件中的空行");
            }
            
            if (suggestions.Count == 0)
            {
                suggestions.Add("数据质量良好，无特殊改进建议");
            }
            
            return suggestions;
        }
        
        #endregion
    }
}