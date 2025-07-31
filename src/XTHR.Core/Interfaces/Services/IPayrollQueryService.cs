using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public enum CostAnalysisDimension
    {
        ByDepartment,
        ByMonth,
        ByYear
    }

    public interface IPayrollQueryService
    {
        Task<IEnumerable<PayrollHistoryDto>> GetPayrollHistoryAsync(DateTime startPeriod, DateTime endPeriod, int? employeeId = null, int? departmentId = null);
        Task<IEnumerable<CostAnalysisDto>> GetCostAnalysisAsync(DateTime startPeriod, DateTime endPeriod, CostAnalysisDimension dimension);
    }
}