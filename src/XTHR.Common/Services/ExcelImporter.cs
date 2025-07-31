using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using XTHR.Common.Interfaces;
using Serilog;

namespace XTHR.Common.Services
{
    /// <summary>
    /// Excel导入器基础实现
    /// </summary>
    /// <typeparam name="T">导入的数据类型</typeparam>
    public class ExcelImporter<T> : IExcelImporter<T> where T : class, new()
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, PropertyInfo> _propertyMappings;
        private readonly Dictionary<PropertyInfo, ExcelColumnAttribute> _columnAttributes;
        
        public ExcelImporter(ILogger? logger = null)
        {
            _logger = logger ?? Log.Logger;
            _propertyMappings = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            _columnAttributes = new Dictionary<PropertyInfo, ExcelColumnAttribute>();
            
            InitializePropertyMappings();
        }
        
        /// <summary>
        /// 从Excel文件导入数据
        /// </summary>
        public async Task<ExcelImportResult<T>> ImportFromFileAsync(string filePath, ExcelImportOptions<T> options = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new ExcelImportResult<T>
                    {
                        IsSuccess = false,
                        ErrorMessage = $"文件不存在: {filePath}"
                    };
                }
                
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return await ImportFromStreamAsync(stream, options);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "从文件导入Excel数据时发生错误: {FilePath}", filePath);
                return new ExcelImportResult<T>
                {
                    IsSuccess = false,
                    ErrorMessage = $"导入失败: {ex.Message}"
                };
            }
        }
        
        /// <summary>
        /// 从Excel流导入数据
        /// </summary>
        public async Task<ExcelImportResult<T>> ImportFromStreamAsync(Stream stream, ExcelImportOptions<T> options = null)
        {
            var startTime = DateTime.Now;
            var result = new ExcelImportResult<T>();
            options ??= new ExcelImportOptions<T>();
            
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                
                using var package = new ExcelPackage(stream);
                var worksheet = GetWorksheet(package, options ?? new ExcelImportOptions<T>());
                
                if (worksheet == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "无法找到指定的工作表";
                    return result;
                }
                
                // 验证列映射
                var columnMappings = ValidateAndMapColumns(worksheet, options);
                if (columnMappings == null || !columnMappings.Any())
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "无法映射Excel列到数据属性";
                    return result;
                }
                
                // 导入数据
                await Task.Run(() => ImportDataFromWorksheet(worksheet, columnMappings, options, result));
                
                // 设置摘要信息
                result.Summary.ProcessingTime = DateTime.Now - startTime;
                result.Summary.TotalRows = result.SuccessData.Count + result.Errors.Count;
                result.Summary.SuccessRows = result.SuccessData.Count;
                result.Summary.ErrorRows = result.Errors.Count;
                result.Summary.WarningRows = result.Warnings.Count;
                
                result.IsSuccess = result.SuccessData.Any() || !result.Errors.Any();
                
                _logger?.Information("Excel导入完成: 总行数={TotalRows}, 成功={SuccessRows}, 错误={ErrorRows}", 
                    result.Summary.TotalRows, result.Summary.SuccessRows, result.Summary.ErrorRows);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "导入Excel数据时发生错误");
                result.IsSuccess = false;
                result.ErrorMessage = $"导入失败: {ex.Message}";
                return result;
            }
        }
        
        /// <summary>
        /// 验证Excel文件格式
        /// </summary>
        public async Task<ExcelValidationResult> ValidateFileAsync(string filePath, ExcelImportOptions<T> options = null)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new ExcelValidationResult
                    {
                        IsValid = false,
                        Errors = { new ExcelValidationError { ErrorType = ExcelErrorType.FileFormat, ErrorMessage = "文件不存在" } }
                    };
                }
                
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var result = await ValidateStreamAsync(stream, options);
                if (result.FileInfo != null)
                {
                    result.FileInfo.FileName = Path.GetFileName(filePath);
                    result.FileInfo.FileSize = new FileInfo(filePath).Length;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "验证Excel文件时发生错误: {FilePath}", filePath);
                return new ExcelValidationResult
                {
                    IsValid = false,
                    Errors = new List<ExcelValidationError> { new ExcelValidationError { ErrorType = ExcelErrorType.SystemError, ErrorMessage = ex.Message } }
                };
            }
        }
        
        /// <summary>
        /// 验证Excel流格式
        /// </summary>
        public async Task<ExcelValidationResult> ValidateStreamAsync(Stream stream, ExcelImportOptions<T> options = null)
        {
            var result = new ExcelValidationResult { FileInfo = new ExcelFileInfo() };
            options ??= new ExcelImportOptions<T>();
            
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using var package = new ExcelPackage(stream);
                // 验证工作表
                result.FileInfo.WorksheetCount = package.Workbook.Worksheets.Count;
                result.FileInfo.WorksheetNames = package.Workbook.Worksheets.Select(ws => ws.Name).ToList();
                var worksheet = GetWorksheet(package, options);
                if (worksheet == null)
                {
                    result.Errors.Add(new ExcelValidationError
                    {
                        ErrorType = ExcelErrorType.WorksheetNotFound,
                        ErrorMessage = $"找不到工作表: {options.WorksheetName ?? $"索引{options.WorksheetIndex}"}"
                    });
                    return result;
                }
                // 验证数据范围
                var dimension = worksheet.Dimension;
                if (dimension == null)
                {
                    result.Warnings.Add(new ExcelValidationWarning
                    {
                        WarningType = ExcelWarningType.EmptyRowSkipped,
                        WarningMessage = "工作表为空"
                    });
                    result.IsValid = true;
                    return result;
                }
                result.FileInfo.DataRows = dimension.Rows;
                result.FileInfo.DataColumns = dimension.Columns;
                // 验证列映射
                var columnMappings = ValidateAndMapColumns(worksheet, options);
                if (columnMappings == null || !columnMappings.Any())
                {
                    result.Errors.Add(new ExcelValidationError
                    {
                        ErrorType = ExcelErrorType.MissingRequiredColumn,
                        ErrorMessage = "无法映射必需的列",
                        Details = null as string
                    });
                }
                result.IsValid = !result.Errors.Any();
                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "验证Excel流时发生错误");
                result.IsValid = false;
                result.Errors.Add(new ExcelValidationError
                {
                    ErrorType = ExcelErrorType.SystemError,
                    ErrorMessage = ex.Message,
                    Details = null
                });
                return await Task.FromResult(result);
            }
        }
        
        /// <summary>
        /// 生成导入模板
        /// </summary>
        public async Task<bool> GenerateTemplateAsync(string filePath, ExcelTemplateOptions options = null)
        {
            try
            {
                using var stream = await GetTemplateStreamAsync(options);
                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await stream.CopyToAsync(fileStream);
                return true;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "生成Excel模板时发生错误: {FilePath}", filePath);
                return false;
            }
        }
        
        /// <summary>
        /// 获取导入模板流
        /// </summary>
        public async Task<Stream> GetTemplateStreamAsync(ExcelTemplateOptions options = null)
        {
            options ??= new ExcelTemplateOptions();
            
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                
                var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add(options.WorksheetName);
                
                // 获取列配置
                var columnConfigs = GetColumnConfigs(options);
                
                // 创建标题行
                CreateHeaderRow(worksheet, columnConfigs);
                
                // 添加数据验证
                if (options.IncludeDataValidation)
                {
                    AddDataValidation(worksheet, columnConfigs);
                }
                
                // 添加示例数据
                if (options.IncludeSampleData)
                {
                    AddSampleData(worksheet, columnConfigs, options.SampleDataRows);
                }
                
                // 添加说明
                if (options.IncludeInstructions)
                {
                    AddInstructions(worksheet, columnConfigs);
                }
                
                // 设置列宽和格式
                FormatWorksheet(worksheet, columnConfigs);
                
                var stream = new MemoryStream();
                await package.SaveAsAsync(stream);
                stream.Position = 0;
                
                return stream;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "生成Excel模板流时发生错误");
                throw;
            }
        }
        
        #region 私有方法
        
        /// <summary>
        /// 初始化属性映射
        /// </summary>
        private void InitializePropertyMappings()
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite);
            
            foreach (var property in properties)
            {
                var columnAttr = property.GetCustomAttribute<ExcelColumnAttribute>();
                if (columnAttr != null)
                {
                    _columnAttributes[property] = columnAttr;
                    _propertyMappings[columnAttr.Name] = property;
                }
                else
                {
                    _propertyMappings[property.Name] = property;
                }
            }
        }
        
        /// <summary>
        /// 获取工作表
        /// </summary>
        private ExcelWorksheet GetWorksheet(ExcelPackage package, ExcelImportOptions<T> options)
        {
            if (!string.IsNullOrEmpty(options.WorksheetName))
            {
                return package.Workbook.Worksheets[options.WorksheetName];
            }
            
            if (options.WorksheetIndex >= 0 && options.WorksheetIndex < package.Workbook.Worksheets.Count)
            {
                return package.Workbook.Worksheets[options.WorksheetIndex];
            }
            
            return package.Workbook.Worksheets.Count > 0 ? package.Workbook.Worksheets[0] : throw new InvalidOperationException("未找到任何工作表");
        }
        
        /// <summary>
        /// 验证并映射列
        /// </summary>
        private Dictionary<int, PropertyInfo> ValidateAndMapColumns(ExcelWorksheet worksheet, ExcelImportOptions<T>? options)
        {
            var columnMappings = new Dictionary<int, PropertyInfo>();
            var headerRow = options.HeaderRowIndex;
            
            if (worksheet.Dimension == null || headerRow > worksheet.Dimension.Rows)
            {
                return new Dictionary<int, PropertyInfo>();
            }
            
            // 读取标题行
            var headers = new Dictionary<int, string>();
            for (int col = 1; col <= worksheet.Dimension.Columns; col++)
            {
                var headerValue = worksheet.Cells[headerRow, col].Text;
                if (!string.IsNullOrEmpty(headerValue))
                {
                    headers[col] = (headerValue ?? string.Empty).Trim();
                }
            }
            
            // 映射列到属性
            foreach (var header in headers)
            {
                var columnName = header.Value;
                PropertyInfo property = null;
                
                // 首先检查自定义映射
                if (options?.ColumnMappings != null && options.ColumnMappings.ContainsKey(columnName))
                {
                    var propertyName = options.ColumnMappings[columnName];
                    if (!string.IsNullOrEmpty(propertyName) && _propertyMappings.TryGetValue(propertyName, out var mappedProperty) && mappedProperty != null)
                    {
                        property = mappedProperty;
                    }
                }
                else
                {
                    // 使用默认映射
                    if (_propertyMappings.TryGetValue(columnName, out var mappedProperty) && mappedProperty != null)
                    {
                        property = mappedProperty;
                    }
                }
                
                if (property != null)
                {
                    columnMappings[header.Key] = property;
                }
            }
            
            // 验证必需列
            if (options?.RequiredColumns != null && options.RequiredColumns.Any())
            {
                var mappedColumns = columnMappings.Values
                    .Where(p => p != null && p.Name != null)
                    .Select(p => p.Name!)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
                var missingColumns = options.RequiredColumns.Where(col => !mappedColumns.Contains(col)).ToList();
                
                if (missingColumns.Any())
                {
                    _logger?.Warning("缺少必需列: {MissingColumns}", string.Join(", ", missingColumns));
                    if (options.StrictMode)
                    {
                        return new Dictionary<int, PropertyInfo>();
                    }
                }
            }
            
            return columnMappings;
        }
        
        /// <summary>
        /// 从工作表导入数据
        /// </summary>
        private void ImportDataFromWorksheet(ExcelWorksheet worksheet, Dictionary<int, PropertyInfo> columnMappings, 
            ExcelImportOptions<T>? options, ExcelImportResult<T> result)
        {
            options ??= new ExcelImportOptions<T>();
            var startRow = options.DataStartRowIndex;
            var endRow = worksheet.Dimension?.Rows ?? 0;
            
            if (options.MaxRows > 0)
            {
                endRow = Math.Min(endRow, startRow + options.MaxRows - 1);
            }
            
            for (int row = startRow; row <= endRow; row++)
            {
                try
                {
                    // 检查是否为空行
                    if (options.SkipEmptyRows && IsEmptyRow(worksheet, row, columnMappings.Keys))
                    {
                        result.Warnings.Add(new ExcelImportWarning
                        {
                            RowIndex = row,
                            WarningType = ExcelWarningType.EmptyRowSkipped,
                            WarningMessage = "跳过空行"
                        });
                        result.Summary.SkippedRows++;
                        continue;
                    }
                    
                    var item = new T();
                    var hasError = false;
                    
                    // 填充属性值
                    foreach (var mapping in columnMappings)
                    {
                        var columnIndex = mapping.Key;
                        var property = mapping.Value;
                        
                        try
                        {
                            var cellValue = worksheet.Cells[row, columnIndex].Value;
                            var convertedValue = ConvertCellValue(cellValue, property.PropertyType);
                            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            
                            if (convertedValue == null)
                            {
                                if (targetType.IsValueType && targetType != typeof(string))
                                {
                                    var defaultValue = Activator.CreateInstance(targetType);
                                    property.SetValue(item, defaultValue ?? Activator.CreateInstance(targetType));
                                }
                                else if (targetType.IsClass || targetType == typeof(string))
                                {
                                    property.SetValue(item, null);
                                }
                            }
                            else
                            {
                                try
                                {
                                    var converted = Convert.ChangeType(convertedValue, targetType);
                            if (converted != null)
                            {
                                property.SetValue(item, converted);
                            }
                            else if (targetType.IsValueType && targetType != typeof(string))
                            {
                                property.SetValue(item, Activator.CreateInstance(targetType));
                            }
                            else
                            {
                                property.SetValue(item, null);
                            }
                                }
                                catch
                                {
                                    var defaultValue = targetType.IsValueType && targetType != typeof(string)
                                        ? Activator.CreateInstance(targetType)
                                        : null;
                                    property.SetValue(item, defaultValue ?? Activator.CreateInstance(targetType));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            hasError = true;
                            result.Errors.Add(new ExcelImportError<T>
                            {
                                RowIndex = row,
                                ErrorType = ExcelErrorType.DataTypeError,
                                ErrorMessage = $"列 '{property.Name}' 数据转换失败: {ex.Message}",
                                ColumnName = property.Name,
                                OriginalValue = worksheet.Cells[row, columnIndex].Value,
                                OriginalData = item,
                                Exception = ex
                            });
                            
                            if (options.ErrorHandlingStrategy == ExcelErrorHandlingStrategy.StopOnError)
                            {
                                return;
                            }
                        }
                    }
                    
                    // 数据验证
                    if (!hasError && options.DataValidator != null)
                    {
                        var validationResult = options.DataValidator(item, row);
                        if (!validationResult.IsValid)
                        {
                            hasError = true;
                            result.Errors.Add(new ExcelImportError<T>
                            {
                                RowIndex = row,
                                ErrorType = ExcelErrorType.ValidationFailed,
                                ErrorMessage = validationResult.ErrorMessage ?? "数据验证失败",
                                OriginalData = item
                            });
                        }
                        else if (!string.IsNullOrEmpty(validationResult.WarningMessage))
                        {
                            result.Warnings.Add(new ExcelImportWarning
                            {
                                RowIndex = row,
                                WarningType = ExcelWarningType.DataConverted,
                                WarningMessage = validationResult.WarningMessage
                            });
                        }
                    }
                    
                    // 属性验证
                    if (!hasError)
                    {
                        var validationErrors = ValidateDataAnnotations(item, row);
                        if (validationErrors.Any())
                        {
                            hasError = true;
                            result.Errors.AddRange(validationErrors);
                        }
                    }
                    
                    if (!hasError || options.ErrorHandlingStrategy == ExcelErrorHandlingStrategy.ContinueOnError)
                    {
                        if (!hasError)
                        {
                            result.SuccessData.Add(item);
                        }
                    }
                    else if (options.ErrorHandlingStrategy == ExcelErrorHandlingStrategy.StopOnError)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    _logger?.Error(ex, "处理第 {Row} 行时发生错误", row);
                    result.Errors.Add(new ExcelImportError<T>
                    {
                        RowIndex = row,
                        ErrorType = ExcelErrorType.SystemError,
                        ErrorMessage = $"处理行数据时发生错误: {ex.Message}",
                        Exception = ex
                    });
                    
                    if (options.ErrorHandlingStrategy == ExcelErrorHandlingStrategy.StopOnError)
                    {
                        return;
                    }
                }
            }
        }
        
        /// <summary>
        /// 检查是否为空行
        /// </summary>
        private bool IsEmptyRow(ExcelWorksheet worksheet, int row, IEnumerable<int> columnIndexes)
        {
            return columnIndexes.All(col => string.IsNullOrWhiteSpace(worksheet.Cells[row, col].Text));
        }
        
        /// <summary>
        /// 转换单元格值
        /// </summary>
        private object? ConvertCellValue(object? cellValue, Type targetType)
        {
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if (cellValue == null)
            {
                if (underlyingType.IsValueType && Nullable.GetUnderlyingType(targetType) == null)
                {
                    // 非可空值类型，直接返回默认值
                    return Activator.CreateInstance(underlyingType);
                }
                return null;
            }
            if (underlyingType == typeof(string))
            {
                return cellValue.ToString() ?? string.Empty;
            }
            if (underlyingType == typeof(DateTime))
            {
                if (cellValue is DateTime dateTime)
                {
                    return dateTime;
                }
                if (DateTime.TryParse(cellValue.ToString(), out var parsedDate))
                {
                    return parsedDate;
                }
            }
            if (underlyingType.IsEnum)
            {
                if (Enum.TryParse(underlyingType, cellValue.ToString(), true, out var enumValue))
                {
                    return enumValue;
                }
            }
            return Convert.ChangeType(cellValue, underlyingType, CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// 验证数据注解
        /// </summary>
        private List<ExcelImportError<T>> ValidateDataAnnotations(T item, int rowIndex)
        {
            var errors = new List<ExcelImportError<T>>();
            var validationContext = new ValidationContext(item);
            var validationResults = new List<ValidationResult>();
            
            if (!Validator.TryValidateObject(item, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    errors.Add(new ExcelImportError<T>
                    {
                        RowIndex = rowIndex,
                        ErrorType = ExcelErrorType.ValidationFailed,
                        ErrorMessage = validationResult.ErrorMessage ?? "验证失败",
                        ColumnName = validationResult.MemberNames?.FirstOrDefault() ?? string.Empty,
                        OriginalData = item
                    });
                }
            }
            
            return errors;
        }
        
        /// <summary>
        /// 获取列配置
        /// </summary>
        private List<ExcelColumnConfig> GetColumnConfigs(ExcelTemplateOptions options)
        {
            var configs = new List<ExcelColumnConfig>();
            
            if (options.ColumnConfigs.Any())
            {
                return options.ColumnConfigs;
            }
            
            // 从类型属性生成配置
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite);
            
            foreach (var property in properties)
            {
                var columnAttr = property.GetCustomAttribute<ExcelColumnAttribute>();
                var displayAttr = property.GetCustomAttribute<DisplayNameAttribute>();
                var requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                
                var config = new ExcelColumnConfig
                {
                    ColumnName = property.Name,
                    DisplayName = columnAttr?.DisplayName ?? displayAttr?.DisplayName ?? property.Name,
                    IsRequired = requiredAttr != null || columnAttr?.IsRequired == true,
                    DataType = property.PropertyType,
                    Width = columnAttr?.Width ?? 15,
                    ValidationDescription = columnAttr?.ValidationDescription ?? string.Empty
                };
                
                // 设置示例值
                if (columnAttr?.SampleValue != null)
                {
                    config.SampleValue = columnAttr.SampleValue;
                }
                else
                {
                    config.SampleValue = GetDefaultSampleValue(property.PropertyType);
                }
                
                configs.Add(config);
            }
            
            return configs;
        }
        
        /// <summary>
        /// 获取默认示例值
        /// </summary>
        private string GetDefaultSampleValue(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;
            
            if (underlyingType == typeof(string)) return "示例文本";
            if (underlyingType == typeof(int)) return "123";
            if (underlyingType == typeof(decimal)) return "123.45";
            if (underlyingType == typeof(double)) return "123.45";
            if (underlyingType == typeof(DateTime)) return DateTime.Now.ToString("yyyy-MM-dd");
            if (underlyingType == typeof(bool)) return "true";
            if (underlyingType.IsEnum) return Enum.GetValues(underlyingType).GetValue(0)?.ToString() ?? string.Empty;
            
            return string.Empty;
        }
        
        /// <summary>
        /// 创建标题行
        /// </summary>
        private void CreateHeaderRow(ExcelWorksheet worksheet, List<ExcelColumnConfig> columnConfigs)
        {
            for (int i = 0; i < columnConfigs.Count; i++)
            {
                var config = columnConfigs[i];
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = config.DisplayName;
                
                // 设置样式
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                
                // 必需字段标记
                if (config.IsRequired)
                {
                    cell.Value = $"{config.DisplayName}*";
                    cell.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                }
            }
        }
        
        /// <summary>
        /// 添加数据验证
        /// </summary>
        private void AddDataValidation(ExcelWorksheet worksheet, List<ExcelColumnConfig> columnConfigs)
        {
            for (int i = 0; i < columnConfigs.Count; i++)
            {
                var config = columnConfigs[i];
                var columnLetter = GetColumnLetter(i + 1);
                
                if (config.DropdownOptions?.Any() == true)
                {
                    var validation = worksheet.DataValidations.AddListValidation($"{columnLetter}2:{columnLetter}1000");
                    foreach (var option in config.DropdownOptions)
                    {
                        validation.Formula.Values.Add(option);
                    }
                    validation.ShowErrorMessage = true;
                    validation.ErrorTitle = "无效值";
                    validation.Error = $"请从列表中选择有效值: {string.Join(", ", config.DropdownOptions)}";
                }
            }
        }
        
        /// <summary>
        /// 添加示例数据
        /// </summary>
        private void AddSampleData(ExcelWorksheet worksheet, List<ExcelColumnConfig> columnConfigs, int sampleRows)
        {
            for (int row = 2; row <= sampleRows + 1; row++)
            {
                for (int col = 0; col < columnConfigs.Count; col++)
                {
                    var config = columnConfigs[col];
                    var cell = worksheet.Cells[row, col + 1];
                    cell.Value = config.SampleValue;
                    
                    // 设置格式
                    if (!string.IsNullOrEmpty(config.Format))
                    {
                        cell.Style.Numberformat.Format = config.Format;
                    }
                }
            }
        }
        
        /// <summary>
        /// 添加说明
        /// </summary>
        private void AddInstructions(ExcelWorksheet worksheet, List<ExcelColumnConfig> columnConfigs)
        {
            var instructionRow = columnConfigs.Count + 3;
            
            worksheet.Cells[instructionRow, 1].Value = "导入说明:";
            worksheet.Cells[instructionRow, 1].Style.Font.Bold = true;
            
            var instructions = new List<string>
            {
                "1. 请不要修改标题行",
                "2. 标记*的列为必填项",
                "3. 请按照示例格式填写数据",
                "4. 日期格式: yyyy-MM-dd",
                "5. 删除示例数据后再导入"
            };
            
            for (int i = 0; i < instructions.Count; i++)
            {
                worksheet.Cells[instructionRow + i + 1, 1].Value = instructions[i];
            }
        }
        
        /// <summary>
        /// 格式化工作表
        /// </summary>
        private void FormatWorksheet(ExcelWorksheet worksheet, List<ExcelColumnConfig> columnConfigs)
        {
            // 设置列宽
            for (int i = 0; i < columnConfigs.Count; i++)
            {
                var config = columnConfigs[i];
                worksheet.Column(i + 1).Width = config.Width;
            }
            
            // 自动调整行高
            worksheet.Cells.AutoFitColumns();
        }
        
        /// <summary>
        /// 获取列字母
        /// </summary>
        private string GetColumnLetter(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }
        
        #endregion
    }
    
    /// <summary>
    /// Excel列特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// 是否必需
        /// </summary>
        public bool IsRequired { get; set; }
        
        /// <summary>
        /// 列宽
        /// </summary>
        public double Width { get; set; } = 15;
        
        /// <summary>
        /// 示例值
        /// </summary>
        public object? SampleValue { get; set; }
        
        /// <summary>
        /// 验证规则描述
        /// </summary>
        public string? ValidationDescription { get; set; }
        
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string? Format { get; set; }
        
        public ExcelColumnAttribute(string name)
        {
            Name = name;
            DisplayName = name;
        }
    }
}