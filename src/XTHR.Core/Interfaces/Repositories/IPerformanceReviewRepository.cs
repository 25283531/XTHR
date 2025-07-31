using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IPerformanceReviewRepository : IBaseRepository<PerformanceReview, int>
    {
        Task<PerformanceReview> GetByEmployeeAndPeriodAsync(int employeeId, DateTime reviewPeriod);
        Task<IEnumerable<PerformanceReview>> GetByEmployeeIdAsync(int employeeId);
    }
}