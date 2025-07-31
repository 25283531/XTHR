using System;

namespace XTHR.Core.Entities
{
    public class PerformanceReview : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public DateTime ReviewPeriod { get; set; }
        public decimal Score { get; set; }
        public string Notes { get; set; }
    }
}