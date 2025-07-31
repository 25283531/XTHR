using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISalaryBaseRepository _salaryBaseRepository;
        private readonly ISocialSecurityRepository _socialSecurityRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeManagementService(
            IEmployeeRepository employeeRepository,
            ISalaryBaseRepository salaryBaseRepository,
            ISocialSecurityRepository socialSecurityRepository,
            IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _salaryBaseRepository = salaryBaseRepository;
            _socialSecurityRepository = socialSecurityRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<EmployeeSummaryDto>> GetEmployeesAsync(string departmentName = null)
        {
            IEnumerable<Core.Entities.Employee> employees;
            if (string.IsNullOrEmpty(departmentName))
            {
                employees = await _employeeRepository.GetByStatusAsync(true);
            }
            else
            {
                var department = await _departmentRepository.GetByNameAsync(departmentName);
                if (department == null)
                {
                    return new List<EmployeeSummaryDto>();
                }
                employees = await _employeeRepository.GetByDepartmentAsync(department.Id, true);
            }

            var employeeDtos = new List<EmployeeSummaryDto>();
            foreach (var emp in employees)
            {
                var department = await _departmentRepository.GetByIdAsync(emp.DepartmentId);
                employeeDtos.Add(new EmployeeSummaryDto
                {
                    EmployeeCode = emp.EmployeeNumber,
                    Name = emp.Name,
                    Department = department?.Name
                });
            }
            return employeeDtos;
        }

        public async Task<EmployeeDetailsDto> GetEmployeeDetailsAsync(string employeeCode)
        {
            var employee = await _employeeRepository.GetByEmployeeNumberAsync(employeeCode);
            if (employee == null)
            {
                return null;
            }

            var salary = await _salaryBaseRepository.GetByEmployeeCodeAsync(employeeCode);
            var socialSecurity = await _socialSecurityRepository.GetByEmployeeCodeAsync(employeeCode);
            var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId);

            return new EmployeeDetailsDto
            {
                EmployeeCode = employee.EmployeeNumber,
                Name = employee.Name,
                Department = department?.Name,
                Position = employee.Position,
                HireDate = employee.HireDate,
                SalaryInfo = salary != null ? new SalaryDto { BaseSalary = salary.BaseSalary, PerformanceBonus = salary.PerformanceBonus } : null,
                SocialSecurityInfo = socialSecurity != null ? new SocialSecurityDto { IsEnrolled = socialSecurity.IsEnrolled, ContributionBase = socialSecurity.ContributionBase } : null
            };
        }

        public async Task<bool> UpdateEmployeeSalaryAsync(string employeeCode, SalaryUpdateDto salaryUpdate)
        {
            var employee = await _employeeRepository.GetByEmployeeNumberAsync(employeeCode);
            if (employee == null) return false;

            var salary = await _salaryBaseRepository.GetByEmployeeCodeAsync(employeeCode);
            if (salary != null)
            {
                salary.BaseSalary = salaryUpdate.BaseSalary;
                salary.PerformanceBonus = salaryUpdate.PerformanceBonus;
                return await _salaryBaseRepository.UpdateAsync(salary);
            }
            else
            {
                var newSalary = new Common.Models.SalaryBase
                {
                    EmployeeId = employee.Id,
                    BaseSalary = salaryUpdate.BaseSalary,
                    PerformanceBonus = salaryUpdate.PerformanceBonus
                };
                return await _salaryBaseRepository.AddAsync(newSalary) > 0;
            }
        }

        public async Task<bool> UpdateEmployeeSocialSecurityAsync(string employeeCode, SocialSecurityUpdateDto socialSecurityUpdate)
        {
            var employee = await _employeeRepository.GetByEmployeeNumberAsync(employeeCode);
            if (employee == null) return false;

            var socialSecurity = await _socialSecurityRepository.GetByEmployeeCodeAsync(employeeCode);
            if (socialSecurity != null)
            {
                socialSecurity.IsEnrolled = socialSecurityUpdate.IsEnrolled;
                socialSecurity.ContributionBase = socialSecurityUpdate.ContributionBase;
                return await _socialSecurityRepository.UpdateAsync(socialSecurity);
            }
            else
            {
                var newSocialSecurity = new Common.Models.SocialSecurity
                {
                    EmployeeId = employee.Id,
                    IsEnrolled = socialSecurityUpdate.IsEnrolled,
                    ContributionBase = socialSecurityUpdate.ContributionBase
                };
                return await _socialSecurityRepository.AddAsync(newSocialSecurity) > 0;
            }
        }
    }
}