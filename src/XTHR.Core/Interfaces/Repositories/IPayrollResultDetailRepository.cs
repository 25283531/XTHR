using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IPayrollResultDetailRepository : IBaseRepository<PayrollResultDetail, int>
    {
        Task<IEnumerable<PayrollResultDetail>> GetByPayrollResultIdAsync(int payrollResultId);
        Task BatchInsertAsync(IEnumerable<PayrollResultDetail> details);
    }
}