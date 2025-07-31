using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IEmployeeFinancialAdjustmentRepository : IBaseRepository<EmployeeFinancialAdjustment, int>
    {
        Task<IEnumerable<EmployeeFinancialAdjustment>> GetByEmployeeAndPeriodAsync(int employeeId, DateTime start, DateTime end);
        new Task AddRangeAsync(IEnumerable<EmployeeFinancialAdjustment> adjustments);
    }
}