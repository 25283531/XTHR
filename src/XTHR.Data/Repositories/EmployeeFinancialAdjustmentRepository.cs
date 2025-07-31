using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class EmployeeFinancialAdjustmentRepository : BaseRepository<EmployeeFinancialAdjustment, int>, IEmployeeFinancialAdjustmentRepository
    {
        public EmployeeFinancialAdjustmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EmployeeFinancialAdjustment>> GetByEmployeeAndPeriodAsync(int employeeId, DateTime start, DateTime end)
        {
            return await _context.EmployeeFinancialAdjustments
                .Where(a => a.EmployeeId == employeeId && a.AdjustmentDate >= start && a.AdjustmentDate <= end)
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<EmployeeFinancialAdjustment> adjustments)
        {
            await _context.EmployeeFinancialAdjustments.AddRangeAsync(adjustments);
            await _context.SaveChangesAsync();
        }
    }
}