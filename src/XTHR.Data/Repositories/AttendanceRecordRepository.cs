using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XTHR.Core.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class AttendanceRecordRepository : BaseRepository<AttendanceRecord, int>, IAttendanceRecordRepository
    {
        public AttendanceRecordRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndPeriodAsync(int employeeId, DateTime start, DateTime end)
        {
            return await _context.AttendanceRecords
                .Where(r => r.EmployeeId == employeeId && r.RecordDate >= start && r.RecordDate <= end)
                .ToListAsync();
        }
    }
}