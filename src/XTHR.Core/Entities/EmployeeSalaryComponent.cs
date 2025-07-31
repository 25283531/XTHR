using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    public class EmployeeSalaryComponent : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public int SalaryComponentId { get; set; }
        public decimal Value { get; set; }
    }
}