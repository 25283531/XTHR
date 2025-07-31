namespace XTHR.Core.DTOs
{
    public class PayrollRuleDto
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public string Condition { get; set; }
        public string Formula { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }
    }

    public class PayrollRuleCreateDto
    {
        public string RuleName { get; set; }
        public string Condition { get; set; }
        public string Formula { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }
    }

    public class PayrollRuleUpdateDto
    {
        public int Id { get; set; }
        public string RuleName { get; set; }
        public string Condition { get; set; }
        public string Formula { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }
    }
}