using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public interface IPayrollCalculationService
    {
        Task<PayrollCalculationResultDto> CalculatePayrollAsync(int employeeId, string formula, DateTime period);
        Task<IEnumerable<string>> GetAvailableFieldsAsync();
    }
}