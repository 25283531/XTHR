using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using XTHR.Common.Interfaces;
using XTHR.Common.Services;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Services;
using XTHR.Data.Repositories;

namespace XTHR.Data.Services
{
    /// <summary>
    /// 钉钉考勤导入器实现
    /// </summary>
    public class DingTalkAttendanceImporter : IDingTalkAttendanceImporter
    {
        private readonly IExcelImporter<DingTalkAttendanceDto> _excelImporter;
        private readonly IEmployeeRepository _employeeRepository; // 假设存在员工仓储
        private readonly ILogger _logger;

        public DingTalkAttendanceImporter(IExcelImporter<DingTalkAttendanceDto> excelImporter, IEmployeeRepository employeeRepository, ILogger logger)
        {
            _excelImporter = excelImporter;
            _employeeRepository = employeeRepository;
            _logger = logger.ForContext<DingTalkAttendanceImporter>();
        }

        public async Task<ExcelImportResult<DingTalkAttendanceDto>> ImportFromFileAsync(string filePath, DingTalkImportOptions options = null)
        {
            _logger.Information("开始从文件导入钉钉考勤数据: {FilePath}", filePath);
            var importOptions = BuildImportOptions(options);
            var result = await _excelImporter.ImportFromFileAsync(filePath, importOptions);
            return await PostProcessImportAsync(result, options);
        }

        public async Task<ExcelImportResult<DingTalkAttendanceDto>> ImportFromStreamAsync(Stream stream, DingTalkImportOptions options = null)
        {
            _logger.Information("开始从流导入钉钉考勤数据");
            var importOptions = BuildImportOptions(options);
            var result = await _excelImporter.ImportFromStreamAsync(stream, importOptions);
            return await PostProcessImportAsync(result, options);
        }

        public async Task<ExcelValidationResult> ValidateFileAsync(string filePath, DingTalkImportOptions options = null)
        {
            _logger.Information("开始验证钉钉考勤文件: {FilePath}", filePath);
            var importOptions = BuildImportOptions(options);
            return await _excelImporter.ValidateFileAsync(filePath, importOptions);
        }

        public async Task<bool> GenerateTemplateAsync(string filePath, DingTalkTemplateOptions options = null)
        {
            _logger.Information("开始生成钉钉考勤导入模板: {FilePath}", filePath);
            var templateOptions = BuildTemplateOptions(options);
            return await _excelImporter.GenerateTemplateAsync(filePath, templateOptions);
        }

        public async Task<Stream> GetTemplateStreamAsync(DingTalkTemplateOptions options = null)
        {
            _logger.Information("开始获取钉钉考勤导入模板流");
            var templateOptions = BuildTemplateOptions(options);
            return await _excelImporter.GetTemplateStreamAsync(templateOptions);
        }

        /// <summary>
        /// 构建导入选项
        /// </summary>
        private ExcelImportOptions BuildImportOptions(DingTalkImportOptions options)
        {
            var importOptions = options ?? new DingTalkImportOptions();

            // 应用自定义字段映射
            if (options?.CustomFieldMapping != null && options.CustomFieldMapping.Any())
            {
                importOptions.ColumnMappings = options.CustomFieldMapping;
            }

            // 添加自定义验证逻辑
            importOptions.DataValidator = (dto, rowIndex) => ValidateRow(dto, rowIndex, options);

            return importOptions;
        }

        /// <summary>
        /// 构建模板选项
        /// </summary>
        private ExcelTemplateOptions BuildTemplateOptions(DingTalkTemplateOptions options)
        {
            var templateOptions = options ?? new DingTalkTemplateOptions();
            // 这里可以根据需要添加更多模板定制逻辑
            return templateOptions;
        }

        /// <summary>
        /// 导入后处理
        /// </summary>
        private async Task<ExcelImportResult<DingTalkAttendanceDto>> PostProcessImportAsync(ExcelImportResult<DingTalkAttendanceDto> result, DingTalkImportOptions options)
        {
            if (!result.IsSuccess || !result.SuccessData.Any())
            {
                return result;
            }

            _logger.Information("开始对导入的考勤数据进行后处理");

            // 这里可以添加更多后处理逻辑，例如：
            // 1. 转换考勤状态
            // 2. 计算实际工时
            // 3. 保存到数据库

            foreach (var item in result.SuccessData)
            {
                // 示例：转换考勤状态
                item.Status = ConvertAttendanceStatus(item.Status);
            }

            _logger.Information("考勤数据后处理完成");

            return result;
        }

        /// <summary>
        /// 验证单行数据
        /// </summary>
        private ExcelRowValidationResult ValidateRow(DingTalkAttendanceDto dto, int rowIndex, DingTalkImportOptions options)
        {
            var validationResult = new ExcelRowValidationResult { IsValid = true };
            var errors = new List<string>();
            var warnings = new List<string>();

            // 1. 员工匹配验证
            var employee = MatchEmployee(dto, options?.EmployeeMatchingStrategy ?? EmployeeMatchingStrategy.CodeThenName);
            if (employee == null)
            {
                errors.Add("员工匹配失败，系统中不存在对应的员工信息");
            }

            // 2. 考勤状态验证
            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                errors.Add("考勤状态不能为空");
            }

            // 3. 迟到/早退验证
            if (dto.LateMinutes.HasValue && dto.LateMinutes > (options?.LateThresholdMinutes ?? 30))
            {
                warnings.Add($"迟到时间超过阈值({options?.LateThresholdMinutes ?? 30}分钟)");
            }

            if (dto.EarlyLeaveMinutes.HasValue && dto.EarlyLeaveMinutes > (options?.EarlyLeaveThresholdMinutes ?? 30))
            {
                warnings.Add($"早退时间超过阈值({options?.EarlyLeaveThresholdMinutes ?? 30}分钟)");
            }

            if (errors.Any())
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = string.Join("; ", errors);
            }

            if (warnings.Any())
            {
                validationResult.WarningMessage = string.Join("; ", warnings);
            }

            return validationResult;
        }

        /// <summary>
        /// 匹配员工
        /// </summary>
        private object MatchEmployee(DingTalkAttendanceDto dto, EmployeeMatchingStrategy strategy)
        {
            // 这里需要实现真实的员工匹配逻辑
            // 这是一个模拟实现
            switch (strategy)
            {
                case EmployeeMatchingStrategy.CodeOnly:
                    return _employeeRepository.FindByCode(dto.EmployeeCode);
                case EmployeeMatchingStrategy.NameOnly:
                    return _employeeRepository.FindByName(dto.EmployeeName);
                case EmployeeMatchingStrategy.CodeThenName:
                    return _employeeRepository.FindByCode(dto.EmployeeCode) ?? _employeeRepository.FindByName(dto.EmployeeName);
                case EmployeeMatchingStrategy.NameThenCode:
                    return _employeeRepository.FindByName(dto.EmployeeName) ?? _employeeRepository.FindByCode(dto.EmployeeCode);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换考勤状态
        /// </summary>
        private string ConvertAttendanceStatus(string originalStatus)
        {
            if (string.IsNullOrWhiteSpace(originalStatus)) return "未知";

            return originalStatus switch
            {
                "正常" => "正常",
                "迟到" => "迟到",
                "早退" => "早退",
                "旷工" => "缺勤",
                "缺卡" => "缺勤",
                "请假" => "请假",
                "出差" => "出差",
                "外出" => "外出",
                "加班" => "加班",
                _ => originalStatus // 保留未知状态
            };
        }
    }
}