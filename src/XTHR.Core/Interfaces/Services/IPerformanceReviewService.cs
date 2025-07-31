using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public interface IPerformanceReviewService
    {
        Task<IEnumerable<PerformanceReviewDto>> GetPerformanceReviewsAsync(string? employeeCode = null);
        Task<ImportResultDto> ImportPerformanceReviewsAsync(Stream stream);
        Task<byte[]> GenerateImportTemplateAsync();
    }
}