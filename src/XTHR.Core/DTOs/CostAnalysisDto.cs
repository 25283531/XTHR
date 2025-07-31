using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    public class CostAnalysisDto
    {
        public string Dimension { get; set; } // e.g., Department Name, Year, Month
        public decimal TotalGrossPay { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalNetPay { get; set; }
        public int EmployeeCount { get; set; }
    }
}