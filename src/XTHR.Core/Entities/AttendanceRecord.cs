using System;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    public class AttendanceRecord : BaseEntity<int>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } // e.g., "正常", "迟到", "早退", "旷工"
        public string Remarks { get; set; }
    }
}