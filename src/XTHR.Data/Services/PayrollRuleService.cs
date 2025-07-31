using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class PayrollRuleService : IPayrollRuleService
    {
        private readonly IPayrollRuleRepository _payrollRuleRepository;

        public PayrollRuleService(IPayrollRuleRepository payrollRuleRepository)
        {
            _payrollRuleRepository = payrollRuleRepository;
        }

        public async Task<IEnumerable<PayrollRule>> GetRulesAsync()
        {
            return await _payrollRuleRepository.GetAllAsync();
        }

        public async Task<PayrollRule> GetRuleByIdAsync(int id)
        {
            return await _payrollRuleRepository.GetByIdAsync(id);
        }

        public async Task AddRuleAsync(PayrollRule rule)
        {
            await _payrollRuleRepository.AddAsync(rule);
        }

        public async Task UpdateRuleAsync(PayrollRule rule)
        {
            await _payrollRuleRepository.UpdateAsync(rule);
        }

        public async Task DeleteRuleAsync(int id)
        {
            var rule = await _payrollRuleRepository.GetByIdAsync(id);
            if (rule != null)
            {
                await _payrollRuleRepository.DeleteAsync(rule);
            }
        }
    }
}