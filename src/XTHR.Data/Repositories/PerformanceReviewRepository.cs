using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class PerformanceReviewRepository : BaseRepository<PerformanceReview, int>, IPerformanceReviewRepository
    {
        public PerformanceReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PerformanceReview> GetByEmployeeAndPeriodAsync(int employeeId, DateTime reviewPeriod)
        {
            return await _context.PerformanceReviews
                .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.ReviewPeriod.Year == reviewPeriod.Year && r.ReviewPeriod.Month == reviewPeriod.Month);
        }

        public async Task<IEnumerable<PerformanceReview>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.PerformanceReviews
                .Where(r => r.EmployeeId == employeeId)
                .ToListAsync();
        }
    }
}