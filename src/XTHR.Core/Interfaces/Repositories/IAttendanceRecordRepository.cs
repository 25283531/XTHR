using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface IAttendanceRecordRepository : IBaseRepository<AttendanceRecord, int>
    {
        Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndPeriodAsync(int employeeId, DateTime start, DateTime end);
    }
}