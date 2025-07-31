using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    public class PerformanceReviewDto
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ReviewPeriod { get; set; }
        public decimal Score { get; set; }
        public string Notes { get; set; }
    }

    public class ImportResultDto
    {
        public bool Success { get; set; }
        public int TotalRows { get; set; }
        public int ImportedRows { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}