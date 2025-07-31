using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using XTHR.Core.DTOs;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class SalaryStructureService : ISalaryStructureService
    {
        private readonly ISalaryComponentRepository _salaryComponentRepository;
        private readonly IEmployeeSalaryComponentRepository _employeeSalaryComponentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public SalaryStructureService(
            ISalaryComponentRepository salaryComponentRepository,
            IEmployeeSalaryComponentRepository employeeSalaryComponentRepository,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _salaryComponentRepository = salaryComponentRepository;
            _employeeSalaryComponentRepository = employeeSalaryComponentRepository;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<SalaryComponentDto> AddSalaryComponentAsync(SalaryComponentCreateDto componentDto)
        {
            var component = new SalaryComponent
            {
                Name = componentDto.Name,
                Description = componentDto.Description,
                IsEnabled = true
            };

            var newComponent = await _salaryComponentRepository.AddAsync(component);

            return new SalaryComponentDto
            {
                Id = newComponent.Id,
                Name = newComponent.Name,
                Description = newComponent.Description,
                IsEnabled = newComponent.IsEnabled
            };
        }

        public async Task<IEnumerable<EmployeeSalaryStructureDto>> GetEmployeeSalaryStructuresAsync(string departmentName = null, string employeeCode = null, string employeeName = null)
        {
            // 1. 获取员工
            IEnumerable<Employee> employees;
            if (!string.IsNullOrEmpty(departmentName))
            {
                var department = await _departmentRepository.GetByNameAsync(departmentName);
                employees = department != null ? await _employeeRepository.GetByDepartmentIdAsync(department.Id) : new List<Employee>();
            }
            else if (!string.IsNullOrEmpty(employeeCode))
            {
                var employee = await _employeeRepository.GetByCodeAsync(employeeCode);
                employees = employee != null ? new List<Employee> { employee } : new List<Employee>();
            }
            else if (!string.IsNullOrEmpty(employeeName))
            {
                employees = await _employeeRepository.GetByNameAsync(employeeName);
            }
            else
            {
                employees = await _employeeRepository.GetAllAsync();
            }

            // 2. 获取所有启用的工资项
            var enabledComponents = await _salaryComponentRepository.GetEnabledComponentsAsync();

            var result = new List<EmployeeSalaryStructureDto>();

            foreach (var emp in employees)
            {
                var department = await _departmentRepository.GetByIdAsync(emp.DepartmentId);
                var structureDto = new EmployeeSalaryStructureDto
                {
                    EmployeeId = emp.Id,
                    EmployeeCode = emp.EmployeeCode,
                    EmployeeName = emp.Name,
                    DepartmentName = department?.Name,
                    Components = new List<EmployeeSalaryComponentDto>()
                };

                var employeeComponents = await _employeeSalaryComponentRepository.GetByEmployeeIdAsync(emp.Id);

                foreach (var component in enabledComponents)
                {
                    var empComp = employeeComponents.FirstOrDefault(ec => ec.SalaryComponentId == component.Id);
                    structureDto.Components.Add(new EmployeeSalaryComponentDto
                    {
                        ComponentId = component.Id,
                        ComponentName = component.Name,
                        Value = empComp?.Value ?? 0
                    });
                }
                result.Add(structureDto);
            }

            return result;
        }

        public async Task<IEnumerable<SalaryComponentDto>> GetSalaryComponentsAsync()
        {
            var components = await _salaryComponentRepository.GetAllAsync();
            return components.Select(c => new SalaryComponentDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IsEnabled = c.IsEnabled
            });
        }

        public async Task<bool> UpdateEmployeeSalaryStructureAsync(int employeeId, IEnumerable<EmployeeSalaryComponentUpdateDto> components)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return false;

            var componentsToUpdate = new List<EmployeeSalaryComponent>();

            foreach (var compDto in components)
            {
                var existing = await _employeeSalaryComponentRepository.GetByEmployeeAndComponentAsync(employeeId, compDto.ComponentId);
                if (existing != null)
                {
                    existing.Value = compDto.Value;
                    componentsToUpdate.Add(existing);
                }
                else
                {
                    componentsToUpdate.Add(new EmployeeSalaryComponent
                    {
                        EmployeeId = employeeId,
                        SalaryComponentId = compDto.ComponentId,
                        Value = compDto.Value
                    });
                }
            }

            await _employeeSalaryComponentRepository.BatchUpdateAsync(componentsToUpdate);
            return true;
        }

        public async Task<bool> UpdateSalaryComponentAsync(int componentId, SalaryComponentUpdateDto componentDto)
        {
            var component = await _salaryComponentRepository.GetByIdAsync(componentId);
            if (component == null) return false;

            component.Name = componentDto.Name;
            component.Description = componentDto.Description;
            component.IsEnabled = componentDto.IsEnabled;

            await _salaryComponentRepository.UpdateAsync(component);
            return true;
        }

        public async Task<byte[]> GenerateImportTemplateAsync()
        {
            var components = await _salaryComponentRepository.GetEnabledComponentsAsync();

            using (var memoryStream = new MemoryStream())
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("工资导入模板");

                // 创建表头
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("员工工号");
                int cellIndex = 1;
                foreach (var component in components)
                {
                    headerRow.CreateCell(cellIndex++).SetCellValue(component.Name);
                }

                workbook.Write(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}