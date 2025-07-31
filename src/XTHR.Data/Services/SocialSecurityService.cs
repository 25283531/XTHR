using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Data.Services
{
    public class SocialSecurityService : ISocialSecurityService
    {
        private readonly ISocialSecurityItemRepository _socialSecurityItemRepository;
        private readonly ISocialSecurityAccountRepository _socialSecurityAccountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public SocialSecurityService(
            ISocialSecurityItemRepository socialSecurityItemRepository,
            ISocialSecurityAccountRepository socialSecurityAccountRepository,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _socialSecurityItemRepository = socialSecurityItemRepository;
            _socialSecurityAccountRepository = socialSecurityAccountRepository;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<SocialSecurityItemDto> AddSocialSecurityItemAsync(SocialSecurityItemCreateDto dto)
        {
            var item = new Core.Entities.SocialSecurityItem
            {
                ItemName = dto.ItemName,
                DefaultContributionBase = dto.DefaultContributionBase,
                CompanyContributionRatio = dto.CompanyContributionRatio,
                PersonalContributionRatio = dto.PersonalContributionRatio
            };

            await _socialSecurityItemRepository.AddAsync(item);

            return new SocialSecurityItemDto
            {
                Id = item.Id,
                ItemName = item.ItemName,
                DefaultContributionBase = item.DefaultContributionBase,
                CompanyContributionRatio = item.CompanyContributionRatio,
                PersonalContributionRatio = item.PersonalContributionRatio
            };
        }

        public async Task<IEnumerable<EmployeeSocialSecurityDto>> GetEmployeeSocialSecuritiesAsync(string departmentName, string employeeFilter)
        {
            var employees = await _employeeRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(departmentName))
            {
                var department = await _departmentRepository.GetByNameAsync(departmentName);
                if (department != null)
                {
                    employees = employees.Where(e => e.DepartmentId == department.Id);
                }
            }

            if (!string.IsNullOrWhiteSpace(employeeFilter))
            {
                employees = employees.Where(e => e.EmployeeNumber.Contains(employeeFilter) || e.Name.Contains(employeeFilter));
            }

            var result = new List<EmployeeSocialSecurityDto>();
            foreach (var employee in employees)
            {
                var employeeDto = new EmployeeSocialSecurityDto
                {
                    EmployeeId = employee.Id,
                    EmployeeNumber = employee.EmployeeNumber,
                    EmployeeName = employee.Name,
                    DepartmentName = (await _departmentRepository.GetByIdAsync(employee.DepartmentId))?.Name
                };

                var accounts = await _socialSecurityAccountRepository.GetByEmployeeIdAsync(employee.Id);
                foreach (var account in accounts)
                {
                    employeeDto.Items.Add(new SocialSecurityDetailDto
                    {
                        ItemName = account.ItemName,
                        ContributionBase = account.ContributionBase,
                        CompanyContributionAmount = account.CompanyContributionAmount,
                        PersonalContributionAmount = account.PersonalContributionAmount
                    });
                }
                result.Add(employeeDto);
            }

            return result;
        }

        public async Task<IEnumerable<SocialSecurityItemDto>> GetSocialSecurityItemsAsync()
        {
            var items = await _socialSecurityItemRepository.GetAllAsync();
            return items.Select(item => new SocialSecurityItemDto
            {
                Id = item.Id,
                ItemName = item.ItemName,
                DefaultContributionBase = item.DefaultContributionBase,
                CompanyContributionRatio = item.CompanyContributionRatio,
                PersonalContributionRatio = item.PersonalContributionRatio
            });
        }

        public async Task<bool> UpdateSocialSecurityItemAsync(int id, SocialSecurityItemUpdateDto dto)
        {
            var item = await _socialSecurityItemRepository.GetByIdAsync(id);
            if (item == null) return false;

            item.ItemName = dto.ItemName;
            item.DefaultContributionBase = dto.DefaultContributionBase;
            item.CompanyContributionRatio = dto.CompanyContributionRatio;
            item.PersonalContributionRatio = dto.PersonalContributionRatio;

            await _socialSecurityItemRepository.UpdateAsync(item);
            return true;
        }

        public async Task<ImportResultDto> ImportSocialSecurityAsync(Stream stream)
        {
            var result = new ImportResultDto();
            var socialSecurityItems = (await _socialSecurityItemRepository.GetAllAsync()).ToList();
            var itemNames = socialSecurityItems.Select(i => i.ItemName).ToList();

            var accountsToUpdate = new List<Core.Entities.SocialSecurityAccount>();
            var accountsToAdd = new List<Core.Entities.SocialSecurityAccount>();

            using (var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook(stream))
            {
                var sheet = workbook.GetSheetAt(0);
                var headerRow = sheet.GetRow(0);
                var headerMap = new Dictionary<string, int>();
                for (int i = 0; i < headerRow.LastCellNum; i++)
                {
                    var headerText = headerRow.GetCell(i).StringCellValue;
                    var itemName = headerText.Replace(" (缴纳基数)", "");
                    if (itemNames.Contains(itemName) || itemName == "员工工号")
                    {
                        headerMap[itemName] = i;
                    }
                }

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    var employeeNumberCell = row.GetCell(headerMap["员工工号"]);
                    if (employeeNumberCell == null || string.IsNullOrWhiteSpace(employeeNumberCell.ToString()))
                    {
                        result.Errors.Add($"第 {i + 1} 行: 员工工号为空。");
                        continue;
                    }
                    var employeeNumber = employeeNumberCell.ToString();
                    var employee = await _employeeRepository.GetByEmployeeNumberAsync(employeeNumber);
                    if (employee == null)
                    {
                        result.Errors.Add($"第 {i + 1} 行: 员工工号 '{employeeNumber}' 不存在。");
                        continue;
                    }

                    foreach (var item in socialSecurityItems)
                    {
                        var cell = row.GetCell(headerMap[item.ItemName]);
                        decimal contributionBase = 0;
                        if (cell != null && cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                        {
                            contributionBase = (decimal)cell.NumericCellValue;
                        }
                        else if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (!decimal.TryParse(cell.ToString(), out contributionBase))
                            {
                                result.Warnings.Add($"第 {i + 1} 行，员工 '{employeeNumber}': 社保项目 '{item.ItemName}' 的值不是有效的数字，已按0处理。");
                                contributionBase = 0;
                            }
                        }

                        var existingAccount = await _socialSecurityAccountRepository.GetByEmployeeAndItemAsync(employee.Id, item.ItemName);
                        if (existingAccount != null)
                        {
                            existingAccount.ContributionBase = contributionBase;
                            existingAccount.CompanyContributionAmount = contributionBase * item.CompanyContributionRatio;
                            existingAccount.PersonalContributionAmount = contributionBase * item.PersonalContributionRatio;
                            accountsToUpdate.Add(existingAccount);
                        }
                        else
                        {
                            accountsToAdd.Add(new Core.Entities.SocialSecurityAccount
                            {
                                EmployeeId = employee.Id,
                                ItemName = item.ItemName,
                                ContributionBase = contributionBase,
                                CompanyContributionRatio = item.CompanyContributionRatio,
                                PersonalContributionRatio = item.PersonalContributionRatio,
                                CompanyContributionAmount = contributionBase * item.CompanyContributionRatio,
                                PersonalContributionAmount = contributionBase * item.PersonalContributionRatio
                            });
                        }
                    }
                }
            }

            if (result.Errors.Any()) return result;

            if (accountsToAdd.Any())
            {
                await _socialSecurityAccountRepository.AddRangeAsync(accountsToAdd);
            }
            if (accountsToUpdate.Any())
            {
                _socialSecurityAccountRepository.UpdateRange(accountsToUpdate);
            }

            result.Success = true;
            return result;
        }

        public async Task<byte[]> GenerateImportTemplateAsync()
        {
            var socialSecurityItems = await _socialSecurityItemRepository.GetAllAsync();

            using (var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook())
            {
                var sheet = workbook.CreateSheet("社保信息导入模板");

                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("员工工号");

                int columnIndex = 1;
                foreach (var item in socialSecurityItems)
                {
                    headerRow.CreateCell(columnIndex++).SetCellValue(item.ItemName + " (缴纳基数)");
                }

                using (var memoryStream = new MemoryStream())
                {
                    workbook.Write(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}