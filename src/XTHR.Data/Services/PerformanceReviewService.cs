using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using XTHR.Core.DTOs;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class PerformanceReviewService : IPerformanceReviewService
    {
        private readonly IPerformanceReviewRepository _performanceReviewRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PerformanceReviewService(
            IPerformanceReviewRepository performanceReviewRepository,
            IEmployeeRepository employeeRepository)
        {
            _performanceReviewRepository = performanceReviewRepository;
            _employeeRepository = employeeRepository;
        }

        public Task<byte[]> GenerateImportTemplateAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("绩效导入模板");

                // 创建表头
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("员工工号");
                headerRow.CreateCell(1).SetCellValue("考核周期 (YYYY-MM)");
                headerRow.CreateCell(2).SetCellValue("绩效得分");
                headerRow.CreateCell(3).SetCellValue("备注");

                workbook.Write(memoryStream);
                return Task.FromResult(memoryStream.ToArray());
            }
        }

        public async Task<IEnumerable<PerformanceReviewDto>> GetPerformanceReviewsAsync(string employeeCode = null)
        {
            var reviews = new List<PerformanceReview>();
            if (string.IsNullOrEmpty(employeeCode))
            {
                reviews.AddRange(await _performanceReviewRepository.GetAllAsync());
            }
            else
            {
                var employee = await _employeeRepository.GetByCodeAsync(employeeCode);
                if (employee != null)
                {
                    reviews.AddRange(await _performanceReviewRepository.GetByEmployeeIdAsync(employee.Id));
                }
            }

            var result = new List<PerformanceReviewDto>();
            foreach (var review in reviews)
            {
                var employee = await _employeeRepository.GetByIdAsync(review.EmployeeId);
                result.Add(new PerformanceReviewDto
                {
                    Id = review.Id,
                    EmployeeCode = employee?.EmployeeCode,
                    EmployeeName = employee?.Name,
                    ReviewPeriod = review.ReviewPeriod,
                    Score = review.Score,
                    Notes = review.Notes
                });
            }

            return result;
        }

        public async Task<ImportResultDto> ImportPerformanceReviewsAsync(Stream stream)
        {
            var result = new ImportResultDto();
            var workbook = new XSSFWorkbook(stream);
            var sheet = workbook.GetSheetAt(0);

            result.TotalRows = sheet.LastRowNum;

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;

                try
                {
                    var employeeCode = row.GetCell(0)?.ToString().Trim();
                    var reviewPeriodStr = row.GetCell(1)?.ToString().Trim();
                    var scoreStr = row.GetCell(2)?.ToString().Trim();
                    var notes = row.GetCell(3)?.ToString().Trim();

                    if (string.IsNullOrEmpty(employeeCode))
                    {
                        result.Errors.Add($"第 {i + 1} 行: 员工工号不能为空。");
                        continue;
                    }

                    var employee = await _employeeRepository.GetByCodeAsync(employeeCode);
                    if (employee == null)
                    {
                        result.Errors.Add($"第 {i + 1} 行: 找不到工号为 '{employeeCode}' 的员工。");
                        continue;
                    }

                    if (!DateTime.TryParse(reviewPeriodStr, out var reviewPeriod))
                    {
                        result.Errors.Add($"第 {i + 1} 行: 考核周期格式不正确 (应为 YYYY-MM)。");
                        continue;
                    }

                    if (!decimal.TryParse(scoreStr, out var score))
                    {
                        result.Errors.Add($"第 {i + 1} 行: 绩效得分格式不正确。");
                        continue;
                    }

                    if (score <= 0)
                    {
                        result.Errors.Add($"第 {i + 1} 行: 员工 '{employee.Name}' ({employeeCode}) 的绩效得分为 {score}，请核对是否正确。");
                    }

                    var existingReview = await _performanceReviewRepository.GetByEmployeeAndPeriodAsync(employee.Id, reviewPeriod);
                    if (existingReview != null)
                    {
                        existingReview.Score = score;
                        existingReview.Notes = notes;
                        await _performanceReviewRepository.UpdateAsync(existingReview);
                    }
                    else
                    {
                        var newReview = new PerformanceReview
                        {
                            EmployeeId = employee.Id,
                            ReviewPeriod = reviewPeriod,
                            Score = score,
                            Notes = notes
                        };
                        await _performanceReviewRepository.AddAsync(newReview);
                    }

                    result.ImportedRows++;
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"第 {i + 1} 行: 导入失败 - {ex.Message}");
                }
            }

            result.Success = result.Errors.Count == 0;
            return result;
        }
    }
}