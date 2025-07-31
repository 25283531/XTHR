using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class DepartmentRepository : BaseRepository<Department, int>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Department> GetByNameAsync(string name)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Name == name);
        }
    }
}