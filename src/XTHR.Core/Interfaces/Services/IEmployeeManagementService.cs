using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public interface IEmployeeManagementService
    {
        Task<IEnumerable<EmployeeSummaryDto>> GetEmployeesAsync(string? department = null);
        Task<EmployeeDetailsDto> GetEmployeeDetailsAsync(string employeeCode);
        Task<bool> UpdateEmployeeSalaryAsync(string employeeCode, SalaryUpdateDto salaryUpdate);
        Task<bool> UpdateEmployeeSocialSecurityAsync(string employeeCode, SocialSecurityUpdateDto socialSecurityUpdate);
    }
}