using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IEmployeeSalaryComponentRepository : IBaseRepository<EmployeeSalaryComponent, int>
    {
        Task<IEnumerable<EmployeeSalaryComponent>> GetByEmployeeIdAsync(int employeeId);
        Task<EmployeeSalaryComponent> GetByEmployeeAndComponentAsync(int employeeId, int componentId);
        Task BatchUpdateAsync(IEnumerable<EmployeeSalaryComponent> components);
    }
}