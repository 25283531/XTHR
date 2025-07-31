namespace XTHR.Core.Entities
{
    public class PayrollResultDetail
    {
        public int Id { get; set; }
        public int PayrollResultId { get; set; }
        public string ComponentName { get; set; }
        public decimal Value { get; set; }
        public bool IsDeduction { get; set; }
    }
}