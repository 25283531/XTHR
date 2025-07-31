using System.IO;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public interface IFinancialAdjustmentService
    {
        Task<ImportResultDto> ImportAdjustmentsAsync(Stream stream);
        Task<byte[]> GenerateImportTemplateAsync();
    }
}