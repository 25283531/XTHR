using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class SocialSecurityItemRepository : BaseRepository<SocialSecurityItem, int>, ISocialSecurityItemRepository
    {
        public SocialSecurityItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SocialSecurityItem> GetByNameAsync(string name)
        {
            return await _context.SocialSecurityItems.FirstOrDefaultAsync(i => i.Name == name);
        }
    }
}