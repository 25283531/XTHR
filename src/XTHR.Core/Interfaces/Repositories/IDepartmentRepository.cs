using System.Threading.Tasks;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department, int>
    {
        Task<Department> GetByNameAsync(string name);
    }
}