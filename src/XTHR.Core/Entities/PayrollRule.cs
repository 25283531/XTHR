namespace XTHR.Core.Entities
{
    public class PayrollRule
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public string Condition { get; set; }
        public string Formula { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }
    }
}