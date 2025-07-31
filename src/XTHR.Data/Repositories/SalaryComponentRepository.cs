using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class SalaryComponentRepository : BaseRepository<SalaryComponent, int>, ISalaryComponentRepository
    {
        public SalaryComponentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SalaryComponent>> GetEnabledComponentsAsync()
        {
            return await _context.SalaryComponents.Where(c => c.IsEnabled).ToListAsync();
        }
    }
}