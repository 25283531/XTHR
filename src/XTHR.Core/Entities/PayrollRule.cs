using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    public class PayrollRule : BaseEntity<int>
    {

        public string RuleName { get; set; }
        public string Condition { get; set; }
        public string Formula { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }
    }
}