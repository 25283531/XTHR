using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface ISalaryComponentRepository : IBaseRepository<SalaryComponent, int>
    {
        Task<IEnumerable<SalaryComponent>> GetEnabledComponentsAsync();
    }
}