using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class FinancialAdjustmentService : IFinancialAdjustmentService
    {
        private readonly IEmployeeFinancialAdjustmentRepository _adjustmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public FinancialAdjustmentService(IEmployeeFinancialAdjustmentRepository adjustmentRepository, IEmployeeRepository employeeRepository)
        {
            _adjustmentRepository = adjustmentRepository;
            _employeeRepository = employeeRepository;
        }

        public Task<byte[]> GenerateImportTemplateAsync()
        {
            using (var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook())
            {
                var sheet = workbook.CreateSheet("奖金扣款导入模板");

                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("员工工号");
                headerRow.CreateCell(1).SetCellValue("调整类型 (奖金/扣款)");
                headerRow.CreateCell(2).SetCellValue("金额");
                headerRow.CreateCell(3).SetCellValue("调整周期 (YYYY-MM)");
                headerRow.CreateCell(4).SetCellValue("描述");

                using (var memoryStream = new MemoryStream())
                {
                    workbook.Write(memoryStream);
                    return Task.FromResult(memoryStream.ToArray());
                }
            }
        }

        public async Task<ImportResultDto> ImportAdjustmentsAsync(Stream stream)
        {
            var result = new ImportResultDto();
            var adjustments = new List<Core.Entities.EmployeeFinancialAdjustment>();

            using (var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream))
            {
                var sheet = workbook.GetSheetAt(0);
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    try
                    {
                        var employeeNumber = row.GetCell(0)?.ToString();
                        if (string.IsNullOrWhiteSpace(employeeNumber))
                        {
                            result.Errors.Add($"第 {i + 1} 行: 员工工号为空。");
                            continue;
                        }

                        var employee = await _employeeRepository.GetByEmployeeNumberAsync(employeeNumber);
                        if (employee == null)
                        {
                            result.Errors.Add($"第 {i + 1} 行: 员工工号 '{employeeNumber}' 不存在。");
                            continue;
                        }

                        var typeStr = row.GetCell(1)?.ToString();
                        if (!Enum.TryParse<Core.Entities.AdjustmentType>(typeStr, true, out var type))
                        {
                            if (typeStr == "奖金") type = Core.Entities.AdjustmentType.Bonus;
                            else if (typeStr == "扣款") type = Core.Entities.AdjustmentType.Deduction;
                            else
                            {
                                result.Errors.Add($"第 {i + 1} 行: 调整类型 '{typeStr}' 无效，必须是 '奖金' 或 '扣款'。");
                                continue;
                            }
                        }

                        var amountCell = row.GetCell(2);
                        if (amountCell == null || !decimal.TryParse(amountCell.ToString(), out var amount))
                        {
                            result.Errors.Add($"第 {i + 1} 行: 金额无效。");
                            continue;
                        }

                        var periodCell = row.GetCell(3);
                        if (periodCell == null || !DateTime.TryParse(periodCell.ToString() + "-01", out var period))
                        {
                            result.Errors.Add($"第 {i + 1} 行: 调整周期格式无效，应为 YYYY-MM。");
                            continue;
                        }

                        var description = row.GetCell(4)?.ToString() ?? string.Empty;

                        adjustments.Add(new Core.Entities.EmployeeFinancialAdjustment
                        {
                            EmployeeId = employee.Id,
                            Type = type,
                            Amount = amount,
                            AdjustmentPeriod = period,
                            Description = description
                        });
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add($"处理第 {i + 1} 行时发生错误: {ex.Message}");
                    }
                }
            }

            if (result.Errors.Any())
            {
                return result;
            }

            await _adjustmentRepository.AddRangeAsync(adjustments);
            result.Success = true;
            return result;
        }
    }
}