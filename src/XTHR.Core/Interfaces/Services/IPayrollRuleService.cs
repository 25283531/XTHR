using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Services
{
    public interface IPayrollRuleService
    {
        Task<IEnumerable<PayrollRule>> GetRulesAsync();
        Task<PayrollRule> GetRuleByIdAsync(int id);
        Task AddRuleAsync(PayrollRule rule);
        Task UpdateRuleAsync(PayrollRule rule);
        Task DeleteRuleAsync(int id);
    }
}