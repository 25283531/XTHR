using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class SocialSecurityAccountRepository : BaseRepository<SocialSecurityAccount, int>, ISocialSecurityAccountRepository
    {
        public SocialSecurityAccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SocialSecurityAccount>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.SocialSecurityAccounts
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<SocialSecurityAccount> GetByEmployeeAndItemAsync(int employeeId, string itemName)
        {
            return await _context.SocialSecurityAccounts
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.ItemName == itemName);
        }

        public async Task AddRangeAsync(IEnumerable<SocialSecurityAccount> accounts)
        {
            await _context.SocialSecurityAccounts.AddRangeAsync(accounts);
            await _context.SaveChangesAsync();
        }

        public void UpdateRange(IEnumerable<SocialSecurityAccount> accounts)
        {
            _context.SocialSecurityAccounts.UpdateRange(accounts);
            _context.SaveChanges();
        }
    }
}