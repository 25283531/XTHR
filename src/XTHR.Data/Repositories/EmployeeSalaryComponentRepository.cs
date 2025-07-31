using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class EmployeeSalaryComponentRepository : BaseRepository<EmployeeSalaryComponent, int>, IEmployeeSalaryComponentRepository
    {
        public EmployeeSalaryComponentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EmployeeSalaryComponent>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.EmployeeSalaryComponents
                .Where(c => c.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<EmployeeSalaryComponent> GetByEmployeeAndComponentAsync(int employeeId, int componentId)
        {
            return await _context.EmployeeSalaryComponents
                .FirstOrDefaultAsync(c => c.EmployeeId == employeeId && c.SalaryComponentId == componentId);
        }

        public async Task BatchUpdateAsync(IEnumerable<EmployeeSalaryComponent> components)
        {
            _context.EmployeeSalaryComponents.UpdateRange(components);
            await _context.SaveChangesAsync();
        }
    }
}