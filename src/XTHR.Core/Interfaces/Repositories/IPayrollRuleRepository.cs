using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IPayrollRuleRepository : IBaseRepository<PayrollRule, int>
    {
        Task<IEnumerable<PayrollRule>> GetEnabledRulesAsync();
    }
}