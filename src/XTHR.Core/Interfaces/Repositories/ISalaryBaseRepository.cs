using System.Threading.Tasks;
using XTHR.Common.Models;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface ISalaryBaseRepository : IBaseRepository<SalaryBase, int>
    {
        Task<SalaryBase> GetCurrentSalaryBaseAsync(int employeeId);
    }
}