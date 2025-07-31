using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IPayrollResultRepository : IBaseRepository<PayrollResult, int>
    {
        Task<PayrollResult> GetByEmployeeAndPeriodAsync(int employeeId, DateTime period);
    }
}