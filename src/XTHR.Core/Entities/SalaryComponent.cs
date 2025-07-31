namespace XTHR.Core.Entities
{
    public class SalaryComponent : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeduction { get; set; }
    }
}