using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace XTHR.Common.Interfaces
{
    /// <summary>
    /// Excel导入器接口
    /// </summary>
    /// <typeparam name="T">导入的数据类型</typeparam>
    public interface IExcelImporter<T> where T : class
    {
        /// <summary>
        /// 从Excel文件导入数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        Task<ExcelImportResult<T>> ImportFromFileAsync(string filePath, ExcelImportOptions<T> options = null);
        
        /// <summary>
        /// 从Excel流导入数据
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        Task<ExcelImportResult<T>> ImportFromStreamAsync(Stream stream, ExcelImportOptions<T> options = null);
        
        /// <summary>
        /// 验证Excel文件格式
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>验证结果</returns>
        Task<ExcelValidationResult> ValidateFileAsync(string filePath, ExcelImportOptions<T> options = null);
        
        /// <summary>
        /// 验证Excel流格式
        /// </summary>
        /// <param name="stream">Excel文件流</param>
        /// <param name="options">导入选项</param>
        /// <returns>验证结果</returns>
        Task<ExcelValidationResult> ValidateStreamAsync(Stream stream, ExcelImportOptions<T> options = null);
        
        /// <summary>
        /// 生成导入模板
        /// </summary>
        /// <param name="filePath">模板文件保存路径</param>
        /// <param name="options">模板选项</param>
        /// <returns>生成结果</returns>
        Task<bool> GenerateTemplateAsync(string filePath, ExcelTemplateOptions options = null);
        
        /// <summary>
        /// 获取导入模板流
        /// </summary>
        /// <param name="options">模板选项</param>
        /// <returns>模板文件流</returns>
        Task<Stream> GetTemplateStreamAsync(ExcelTemplateOptions options = null);
    }
    
    /// <summary>
    /// Excel导入选项
    /// </summary>
    public class ExcelImportOptions<T> where T : class
    {
        /// <summary>
        /// 工作表名称（默认为第一个工作表）
        /// </summary>
        public string? WorksheetName { get; set; }
        
        /// <summary>
        /// 工作表索引（从0开始，默认为0）
        /// </summary>
        public int WorksheetIndex { get; set; } = 0;
        
        /// <summary>
        /// 标题行索引（从1开始，默认为1）
        /// </summary>
        public int HeaderRowIndex { get; set; } = 1;
        
        /// <summary>
        /// 数据开始行索引（从1开始，默认为2）
        /// </summary>
        public int DataStartRowIndex { get; set; } = 2;
        
        /// <summary>
        /// 最大导入行数（0表示不限制）
        /// </summary>
        public int MaxRows { get; set; } = 0;
        
        /// <summary>
        /// 是否跳过空行
        /// </summary>
        public bool SkipEmptyRows { get; set; } = true;
        
        /// <summary>
        /// 是否严格模式（严格验证列映射）
        /// </summary>
        public bool StrictMode { get; set; } = false;
        
        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; } = true;
        
        /// <summary>
        /// 自定义列映射（Excel列名 -> 属性名）
        /// </summary>
        public Dictionary<string, string> ColumnMappings { get; set; } = new Dictionary<string, string>();
        
        /// <summary>
        /// 必需的列名
        /// </summary>
        public List<string> RequiredColumns { get; set; } = new List<string>();
        
        /// <summary>
        /// 数据验证委托
        /// </summary>
        public Func<T, int, ExcelRowValidationResult>? DataValidator { get; set; }
        
        /// <summary>
        /// 错误处理策略
        /// </summary>
        public ExcelErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ExcelErrorHandlingStrategy.ContinueOnError;
    }
    
    /// <summary>
    /// Excel模板选项
    /// </summary>
    public class ExcelTemplateOptions
    {
        /// <summary>
        /// 工作表名称
        /// </summary>
        public string WorksheetName { get; set; } = "数据导入";
        
        /// <summary>
        /// 是否包含示例数据
        /// </summary>
        public bool IncludeSampleData { get; set; } = true;
        
        /// <summary>
        /// 示例数据行数
        /// </summary>
        public int SampleDataRows { get; set; } = 3;
        
        /// <summary>
        /// 是否包含数据验证
        /// </summary>
        public bool IncludeDataValidation { get; set; } = true;
        
        /// <summary>
        /// 是否包含说明
        /// </summary>
        public bool IncludeInstructions { get; set; } = true;
        
        /// <summary>
        /// 自定义列配置
        /// </summary>
        public List<ExcelColumnConfig> ColumnConfigs { get; set; } = new List<ExcelColumnConfig>();
    }
    
    /// <summary>
    /// Excel列配置
    /// </summary>
    public class ExcelColumnConfig
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string? ColumnName { get; set; }
        
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        
        /// <summary>
        /// 是否必需
        /// </summary>
        public bool IsRequired { get; set; }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; } = typeof(string);
        
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
        /// 下拉选项（用于数据验证）
        /// </summary>
        public List<string> DropdownOptions { get; set; } = new List<string>();
        
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string? Format { get; set; }
    }
    
    /// <summary>
    /// Excel导入结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ExcelImportResult<T> where T : class
    {
        /// <summary>
        /// 导入是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows { get; set; }
        
        /// <summary>
        /// 成功导入的数据
        /// </summary>
        public List<T> SuccessData { get; set; } = new List<T>();
        
        /// <summary>
        /// 失败的行数据
        /// </summary>
        public List<ExcelImportError<T>> Errors { get; set; } = new List<ExcelImportError<T>>();
        
        /// <summary>
        /// 警告信息
        /// </summary>
        public List<ExcelImportWarning> Warnings { get; set; } = new List<ExcelImportWarning>();
        
        /// <summary>
        /// 导入摘要
        /// </summary>
        public ExcelImportSummary Summary { get; set; } = new ExcelImportSummary();
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// Excel验证结果
    /// </summary>
    public class ExcelValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 验证错误
        /// </summary>
        public List<ExcelValidationError> Errors { get; set; } = new List<ExcelValidationError>();
        
        /// <summary>
        /// 验证警告
        /// </summary>
        public List<ExcelValidationWarning> Warnings { get; set; } = new List<ExcelValidationWarning>();
        
        /// <summary>
        /// 文件信息
        /// </summary>
        public ExcelFileInfo? FileInfo { get; set; }
    }
    
    /// <summary>
    /// Excel导入错误
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ExcelImportError<T> where T : class
    {
        /// <summary>
        /// 行号（从1开始）
        /// </summary>
        public int RowIndex { get; set; }
        
        /// <summary>
        /// 错误类型
        /// </summary>
        public ExcelErrorType ErrorType { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
        
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;
        
        /// <summary>
        /// 原始值
        /// </summary>
        public object? OriginalValue { get; set; }
        
        /// <summary>
        /// 原始行数据
        /// </summary>
        public T? OriginalData { get; set; }
        
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception? Exception { get; set; }
    }
    
    /// <summary>
    /// Excel导入警告
    /// </summary>
    public class ExcelImportWarning
    {
        /// <summary>
        /// 行号（从1开始）
        /// </summary>
        public int RowIndex { get; set; }
        
        /// <summary>
        /// 警告类型
        /// </summary>
        public ExcelWarningType WarningType { get; set; }
        
        /// <summary>
        /// 警告消息
        /// </summary>
        public string WarningMessage { get; set; } = string.Empty;
        
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// Excel导入摘要
    /// </summary>
    public class ExcelImportSummary
    {
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRows { get; set; }
        
        /// <summary>
        /// 成功行数
        /// </summary>
        public int SuccessRows { get; set; }
        
        /// <summary>
        /// 错误行数
        /// </summary>
        public int ErrorRows { get; set; }
        
        /// <summary>
        /// 跳过行数
        /// </summary>
        public int SkippedRows { get; set; }
        
        /// <summary>
        /// 警告行数
        /// </summary>
        public int WarningRows { get; set; }
        
        /// <summary>
        /// 处理耗时
        /// </summary>
        public TimeSpan ProcessingTime { get; set; }
    }
    
    /// <summary>
    /// Excel行验证结果
    /// </summary>
    public class ExcelRowValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }
        
        /// <summary>
        /// 警告消息
        /// </summary>
        public string? WarningMessage { get; set; }
    }
    
    /// <summary>
    /// Excel验证错误
    /// </summary>
    public class ExcelValidationError
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public ExcelErrorType ErrorType { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 相关信息
        /// </summary>
        public string? Details { get; set; }
    }
    
    /// <summary>
    /// Excel验证警告
    /// </summary>
    public class ExcelValidationWarning
    {
        /// <summary>
        /// 警告类型
        /// </summary>
        public ExcelWarningType WarningType { get; set; }
        
        /// <summary>
        /// 警告消息
        /// </summary>
        public string WarningMessage { get; set; }
        
        /// <summary>
        /// 相关信息
        /// </summary>
        public string Details { get; set; }
    }
    
    /// <summary>
    /// Excel文件信息
    /// </summary>
    public class ExcelFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string? FileName { get; set; }
        
        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// 工作表数量
        /// </summary>
        public int WorksheetCount { get; set; }
        
        /// <summary>
        /// 工作表名称列表
        /// </summary>
        public List<string> WorksheetNames { get; set; } = new List<string>();
        
        /// <summary>
        /// 数据行数
        /// </summary>
        public int DataRows { get; set; }
        
        /// <summary>
        /// 数据列数
        /// </summary>
        public int DataColumns { get; set; }
    }
    
    /// <summary>
    /// Excel错误类型
    /// </summary>
    public enum ExcelErrorType
    {
        /// <summary>
        /// 文件格式错误
        /// </summary>
        FileFormat,
        
        /// <summary>
        /// 工作表不存在
        /// </summary>
        WorksheetNotFound,
        
        /// <summary>
        /// 缺少必需列
        /// </summary>
        MissingRequiredColumn,
        
        /// <summary>
        /// 数据类型错误
        /// </summary>
        DataTypeError,
        
        /// <summary>
        /// 数据验证失败
        /// </summary>
        ValidationFailed,
        
        /// <summary>
        /// 空值错误
        /// </summary>
        NullValue,
        
        /// <summary>
        /// 格式错误
        /// </summary>
        FormatError,
        
        /// <summary>
        /// 范围错误
        /// </summary>
        RangeError,
        
        /// <summary>
        /// 重复值错误
        /// </summary>
        DuplicateValue,
        
        /// <summary>
        /// 系统错误
        /// </summary>
        SystemError
    }
    
    /// <summary>
    /// Excel警告类型
    /// </summary>
    public enum ExcelWarningType
    {
        /// <summary>
        /// 数据被截断
        /// </summary>
        DataTruncated,
        
        /// <summary>
        /// 数据被转换
        /// </summary>
        DataConverted,
        
        /// <summary>
        /// 空行被跳过
        /// </summary>
        EmptyRowSkipped,
        
        /// <summary>
        /// 额外列被忽略
        /// </summary>
        ExtraColumnIgnored,
        
        /// <summary>
        /// 格式不一致
        /// </summary>
        FormatInconsistent
    }
    
    /// <summary>
    /// Excel错误处理策略
    /// </summary>
    public enum ExcelErrorHandlingStrategy
    {
        /// <summary>
        /// 遇到错误时停止
        /// </summary>
        StopOnError,
        
        /// <summary>
        /// 遇到错误时继续
        /// </summary>
        ContinueOnError,
        
        /// <summary>
        /// 跳过错误行
        /// </summary>
        SkipErrorRows
    }
}