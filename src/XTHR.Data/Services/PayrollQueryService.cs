using System;
using System.Collections.Generic;
using System.Linq;
using XTHR.Core.Entities;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class PayrollQueryService : IPayrollQueryService
    {
        private readonly IPayrollResultRepository _payrollResultRepository;
        private readonly IPayrollResultDetailRepository _payrollResultDetailRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public PayrollQueryService(
            IPayrollResultRepository payrollResultRepository,
            IPayrollResultDetailRepository payrollResultDetailRepository,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _payrollResultRepository = payrollResultRepository;
            _payrollResultDetailRepository = payrollResultDetailRepository;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<PayrollHistoryDto>> GetPayrollHistoryAsync(DateTime startPeriod, DateTime endPeriod, int? employeeId = null, int? departmentId = null)
        {
            var results = new List<PayrollHistoryDto>();
            var payrollResults = (await _payrollResultRepository.GetAllAsync())
                                 .Where(r => r.Period >= startPeriod && r.Period <= endPeriod);

            if (employeeId.HasValue)
            {
                payrollResults = payrollResults.Where(r => r.EmployeeId == employeeId.Value);
            }

            // Department filtering would require Employee entity to have DepartmentId
            // Assuming Employee entity has a DepartmentId property
            if (departmentId.HasValue)
            {
                var employeesInDept = (await _employeeRepository.GetAllAsync()).Where(e => e.DepartmentId == departmentId.Value).Select(e => e.Id);
                payrollResults = payrollResults.Where(r => employeesInDept.Contains(r.EmployeeId));
            }

            var employeeIds = payrollResults.Select(r => r.EmployeeId).Distinct().ToList();
            var allEmployees = (await _employeeRepository.GetAllAsync()).Where(e => employeeIds.Contains(e.Id)).ToDictionary(e => e.Id);

            var payrollResultIds = payrollResults.Select(r => r.Id).ToList();
            var allDetails = (await _payrollResultDetailRepository.GetAllAsync())
                                .Where(d => payrollResultIds.Contains(d.PayrollResultId))
                                .GroupBy(d => d.PayrollResultId)
                                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var result in payrollResults)
            {
                allEmployees.TryGetValue(result.EmployeeId, out var employee);
                allDetails.TryGetValue(result.Id, out var details);

                results.Add(new PayrollHistoryDto
                {
                    PayrollResultId = result.Id,
                    EmployeeId = result.EmployeeId,
                    EmployeeName = employee?.Name ?? "N/A",
                    Period = result.Period,
                    GrossPay = result.GrossPay,
                    TotalDeductions = result.TotalDeductions,
                    NetPay = result.NetPay,
                    Details = details?.Select(d => new PayrollResultDetailDto
                    {
                        ComponentName = d.ComponentName,
                        Value = d.Value,
                        IsDeduction = d.IsDeduction
                    }).ToList() ?? new List<PayrollResultDetailDto>()
                });
            }

            return results;
        }

        public async Task<IEnumerable<CostAnalysisDto>> GetCostAnalysisAsync(DateTime startPeriod, DateTime endPeriod, CostAnalysisDimension dimension)
        {
            var payrollResults = (await _payrollResultRepository.GetAllAsync())
                                 .Where(r => r.Period >= startPeriod && r.Period <= endPeriod);

            switch (dimension)
            {
                case CostAnalysisDimension.ByDepartment:
                    return await AnalyzeByDepartment(payrollResults);
                case CostAnalysisDimension.ByMonth:
                    return AnalyzeByTime(payrollResults, "yyyy-MM");
                case CostAnalysisDimension.ByYear:
                    return AnalyzeByTime(payrollResults, "yyyy");
                default:
                    return new List<CostAnalysisDto>();
            }
        }

        private async Task<IEnumerable<CostAnalysisDto>> AnalyzeByDepartment(IEnumerable<PayrollResult> payrollResults)
        {
            var employeeIds = payrollResults.Select(r => r.EmployeeId).Distinct();
            var employees = (await _employeeRepository.GetAllAsync()).Where(e => employeeIds.Contains(e.Id)).ToDictionary(e => e.Id);
            var departments = (await _departmentRepository.GetAllAsync()).ToDictionary(d => d.Id);

            return payrollResults
                .GroupBy(r => employees.ContainsKey(r.EmployeeId) ? employees[r.EmployeeId].DepartmentId : -1)
                .Select(g => new CostAnalysisDto
                {
                    Dimension = departments.ContainsKey(g.Key) ? departments[g.Key].Name : "未分配",
                    TotalGrossPay = g.Sum(r => r.GrossPay),
                    TotalDeductions = g.Sum(r => r.TotalDeductions),
                    TotalNetPay = g.Sum(r => r.NetPay),
                    EmployeeCount = g.Select(r => r.EmployeeId).Distinct().Count()
                });
        }

        private IEnumerable<CostAnalysisDto> AnalyzeByTime(IEnumerable<PayrollResult> payrollResults, string format)
        {
            return payrollResults
                .GroupBy(r => r.Period.ToString(format))
                .Select(g => new CostAnalysisDto
                {
                    Dimension = g.Key,
                    TotalGrossPay = g.Sum(r => r.GrossPay),
                    TotalDeductions = g.Sum(r => r.TotalDeductions),
                    TotalNetPay = g.Sum(r => r.NetPay),
                    EmployeeCount = g.Select(r => r.EmployeeId).Distinct().Count()
                })
                .OrderBy(dto => dto.Dimension);
        }
    }
}