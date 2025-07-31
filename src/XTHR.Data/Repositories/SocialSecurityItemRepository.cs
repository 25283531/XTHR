using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class SocialSecurityItemRepository : EfCoreBaseRepository<SocialSecurityItem, int>, ISocialSecurityItemRepository
    {
        public SocialSecurityItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SocialSecurityItem?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(item => item.Name == name);
        }
    }
}