using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XTHR.Common.Interfaces;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    /// <summary>
    /// 钉钉考勤导入器接口
    /// </summary>
    public interface IDingTalkAttendanceImporter
    {
        /// <summary>
        /// 从文件导入钉钉考勤数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        Task<ExcelImportResult<DingTalkAttendanceDto>> ImportFromFileAsync(string filePath, DingTalkImportOptions options = null!);

        /// <summary>
        /// 从流导入钉钉考勤数据
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        Task<ExcelImportResult<DingTalkAttendanceDto>> ImportFromStreamAsync(Stream stream, DingTalkImportOptions options = null!);

        /// <summary>
        /// 验证钉钉考勤文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="options">验证选项</param>
        /// <returns>验证结果</returns>
        Task<ExcelValidationResult> ValidateFileAsync(string filePath, DingTalkImportOptions options = null!);

        /// <summary>
        /// 生成钉钉考勤导入模板
        /// </summary>
        /// <param name="filePath">模板文件保存路径</param>
        /// <param name="options">模板选项</param>
        /// <returns>是否成功生成</returns>
        Task<bool> GenerateTemplateAsync(string filePath, DingTalkTemplateOptions options = null!);

        /// <summary>
        /// 获取钉钉考勤导入模板流
        /// </summary>
        /// <param name="options">模板选项</param>
        /// <returns>模板文件流</returns>
        Task<Stream> GetTemplateStreamAsync(DingTalkTemplateOptions options = null!);
    }

    /// <summary>
    /// 钉钉考勤导入选项
    /// </summary>
    public class DingTalkImportOptions : ExcelImportOptions<DingTalkAttendanceDto>
    {
        /// <summary>
        /// 员工匹配规则
        /// </summary>
        public EmployeeMatchingStrategy EmployeeMatchingStrategy { get; set; } = EmployeeMatchingStrategy.CodeThenName;

        /// <summary>
        /// 自定义字段映射
        /// </summary>
        public Dictionary<string, string> CustomFieldMapping { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 迟到阈值（分钟）
        /// </summary>
        public int LateThresholdMinutes { get; set; } = 30;

        /// <summary>
        /// 早退阈值（分钟）
        /// </summary>
        public int EarlyLeaveThresholdMinutes { get; set; } = 30;
    }

    /// <summary>
    /// 钉钉考勤模板选项
    /// </summary>
    public class DingTalkTemplateOptions : ExcelTemplateOptions
    {
        /// <summary>
        /// 包含自定义字段
        /// </summary>
        public bool IncludeCustomFields { get; set; } = false;
    }

    /// <summary>
    /// 员工匹配策略
    /// </summary>
    public enum EmployeeMatchingStrategy
    {
        /// <summary>
        /// 仅使用工号
        /// </summary>
        CodeOnly,

        /// <summary>
        /// 仅使用姓名
        /// </summary>
        NameOnly,

        /// <summary>
        /// 优先使用工号，失败后使用姓名
        /// </summary>
        CodeThenName,

        /// <summary>
        /// 优先使用姓名，失败后使用工号
        /// </summary>
        NameThenCode
    }
}