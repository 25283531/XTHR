using System;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    public class PayrollResult : BaseEntity<int>
    {
        public int EmployeeId { get; set; }
        public DateTime CalculationDate { get; set; }
        public DateTime Period { get; set; }
        public decimal GrossPay { get; set; } // 应发工资
        public decimal NetPay { get; set; } // 实发工资
        public decimal TotalDeductions { get; set; } // 总扣除
        public string Formula { get; set; }
        public string CalculationLog { get; set; }
    }
}