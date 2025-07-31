using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public interface ISalaryStructureService
    {
        Task<IEnumerable<SalaryComponentDto>> GetSalaryComponentsAsync();
        Task<SalaryComponentDto> AddSalaryComponentAsync(SalaryComponentCreateDto componentDto);
        Task<bool> UpdateSalaryComponentAsync(int componentId, SalaryComponentUpdateDto componentDto);
        Task<IEnumerable<EmployeeSalaryStructureDto>> GetEmployeeSalaryStructuresAsync(string? departmentName = null, string? employeeCode = null, string? employeeName = null);
        Task<bool> UpdateEmployeeSalaryStructureAsync(int employeeId, IEnumerable<EmployeeSalaryComponentUpdateDto> components);
        Task<byte[]> GenerateImportTemplateAsync();
    }
}