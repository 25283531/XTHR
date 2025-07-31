using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    public class SalaryComponentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class SalaryComponentCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SalaryComponentUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class EmployeeSalaryStructureDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public List<EmployeeSalaryComponentDto> Components { get; set; }
    }

    public class EmployeeSalaryComponentDto
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public decimal Value { get; set; }
    }

    public class EmployeeSalaryComponentUpdateDto
    {
        public int ComponentId { get; set; }
        public decimal Value { get; set; }
    }
}