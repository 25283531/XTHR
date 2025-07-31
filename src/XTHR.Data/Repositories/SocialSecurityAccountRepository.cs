using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class SocialSecurityAccountRepository : EfCoreBaseRepository<SocialSecurityAccount, int>, ISocialSecurityAccountRepository
    {
        public SocialSecurityAccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SocialSecurityAccount?> GetByAccountNoAsync(string accountNo)
        {
            return await _dbSet.FirstOrDefaultAsync(account => account.AccountNo == accountNo);
        }
    }
}