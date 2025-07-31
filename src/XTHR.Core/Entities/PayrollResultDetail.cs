using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    public class PayrollResultDetail : BaseEntity<int>
    {

        public int PayrollResultId { get; set; }
        public string ComponentName { get; set; }
        public decimal Value { get; set; }
        public bool IsDeduction { get; set; }
    }
}