using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface ISocialSecurityAccountRepository : IBaseRepository<SocialSecurityAccount, int>
    {
        Task<IEnumerable<SocialSecurityAccount>> GetByEmployeeIdAsync(int employeeId);

        Task<SocialSecurityAccount> GetByEmployeeAndItemAsync(int employeeId, string itemName);

        new Task AddRangeAsync(IEnumerable<SocialSecurityAccount> accounts);

        void UpdateRange(IEnumerable<SocialSecurityAccount> accounts);
    }
}