using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.Interfaces.Services;
using XTHR.Core.Models;
using XTHR.Data.Services; // 假设实现类在此命名空间下

namespace XTHR.Examples
{
    /// <summary>
    /// 钉钉考勤导入器使用示例
    /// </summary>
    public class DingTalkAttendanceImporterUsageExample
    {
        private readonly IDingTalkAttendanceImporter _importer;
        private readonly IEmployeeRepository _employeeRepository; // 模拟仓储

        public DingTalkAttendanceImporterUsageExample()
        {
            // 在实际应用中，这些服务会通过依赖注入提供
            _employeeRepository = new MockEmployeeRepository();
            _importer = new DingTalkAttendanceImporter(new ExcelImporter<DingTalkAttendanceDto>(), _employeeRepository);
        }

        /// <summary>
        /// 运行所有示例
        /// </summary>
        public async Task RunAllExamplesAsync()
        {n            Console.WriteLine("--- 开始运行钉钉考勤导入器示例 ---");

            await BasicImportAsync();
            await ImportWithCustomOptionsAsync();
            await GenerateTemplateAsync();
            await ValidateFileOnlyAsync();

            Console.WriteLine("--- 所有钉钉考勤导入器示例运行完毕 ---");
        }

        /// <summary>
        /// 示例1：基础导入
        /// </summary>
        public async Task BasicImportAsync()
        {
            Console.WriteLine("\n--- 示例1: 基础导入 ---");
            var filePath = CreateTestExcelFile("basic_import.xlsx");

            try
            {
                var result = await _importer.ImportFromFileAsync(filePath);

                if (result.Success)
                {
                    Console.WriteLine($"成功导入 {result.SuccessCount} 条记录。");
                    // 处理成功的数据: result.Data
                }
                else
                {n                    Console.WriteLine("导入失败。");
                    Console.WriteLine(result.GetErrorSummary());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导入过程中发生异常: {ex.Message}");
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 示例2：使用自定义选项导入
        /// </summary>
        public async Task ImportWithCustomOptionsAsync()
        {
            Console.WriteLine("\n--- 示例2: 使用自定义选项导入 ---");
            var filePath = CreateTestExcelFile("custom_options_import.xlsx");

            var options = new DingTalkImportOptions
            {
                EmployeeMatchingStrategy = EmployeeMatchingStrategy.ByName,
                LateThresholdMinutes = 10,
                EarlyLeaveThresholdMinutes = 5,
                CustomFieldMapping = new Dictionary<string, string>
                {
                    { "自定义加班事由", "OvertimeReason" }
                }
            };

            try
            {
                var result = await _importer.ImportFromStreamAsync(File.OpenRead(filePath), options);

                if (result.Success)
                {
                    Console.WriteLine($"成功导入 {result.SuccessCount} 条记录 (使用姓名匹配策略)。");
                }
                else
                {
                    Console.WriteLine("导入失败。");
                    Console.WriteLine(result.GetErrorSummary());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导入过程中发生异常: {ex.Message}");
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 示例3：生成导入模板
        /// </summary>
        public async Task GenerateTemplateAsync()
        {
            Console.WriteLine("\n--- 示例3: 生成导入模板 ---");
            var templatePath = "dingtalk_attendance_template.xlsx";

            var options = new DingTalkTemplateOptions
            {
                IncludeCustomFields = true,
                CustomFields = new List<string> { "自定义加班事由", "项目编号" }
            };

            try
            {
                await _importer.GenerateTemplateAsync(templatePath, options);
                Console.WriteLine($"成功生成导入模板: {templatePath}");
                // File.Delete(templatePath); // 可选：清理
            }
            catch (Exception ex)
            {
                Console.WriteLine($"生成模板失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 示例4：仅验证文件，不导入
        /// </summary>
        public async Task ValidateFileOnlyAsync()
        {
            Console.WriteLine("\n--- 示例4: 仅验证文件 ---");
            var filePath = CreateTestExcelFile("validation_test.xlsx", includeErrors: true);

            try
            {
                var validationResult = await _importer.ValidateFileAsync(filePath);

                if (validationResult.Success)
                {
                    Console.WriteLine("文件验证通过，可以进行导入。");
                }
                else
                {
                    Console.WriteLine("文件验证失败。");
                    Console.WriteLine(validationResult.GetErrorSummary());
                    // 可以选择生成错误报告
                    if (validationResult.HasErrors())
                    {
                        var errorReportPath = "error_report.xlsx";
                        await validationResult.ExportErrorsToExcelAsync(errorReportPath);
                        Console.WriteLine($"错误报告已生成: {errorReportPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"验证过程中发生异常: {ex.Message}");
            }
            finally
            {
                File.Delete(filePath);
            }
        }

        #region 辅助方法

        private string CreateTestExcelFile(string fileName, bool includeErrors = false)
        {
            // 此处简化，实际应使用 EPPlus 或类似库创建真实的Excel文件
            var path = Path.Combine(Path.GetTempPath(), fileName);
            using (var writer = File.CreateText(path))
            {
                writer.WriteLine("姓名,工号,考勤日期,上班时间,下班时间,考勤状态");
                writer.WriteLine("张三,001,2023-10-01,09:00,18:00,正常");
                writer.WriteLine("李四,002,2023-10-01,09:15,18:00,迟到");
                if (includeErrors)
                {
                    writer.WriteLine("王五,,2023-10-01,09:00,17:30,早退"); // 工号缺失
                    writer.WriteLine("赵六,004,2023-10-01,09:00,18:00,无效状态"); // 状态错误
                }
            }
            return path;
        }

        #endregion
    }

    /// <summary>
    /// 模拟员工仓储，用于示例
    /// </summary>
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, EmployeeNumber = "001", Name = "张三" },
            new Employee { Id = 2, EmployeeNumber = "002", Name = "李四" },
            new Employee { Id = 3, EmployeeNumber = "003", Name = "王五" },
            new Employee { Id = 4, EmployeeNumber = "004", Name = "赵六" },
            new Employee { Id = 5, EmployeeNumber = "005", Name = "孙七" },
        };

        public Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
        {
            return Task.FromResult(_employees.FirstOrDefault(e => e.EmployeeNumber == employeeNumber));
        }

        public Task<IEnumerable<Employee>> FindByNameAsync(string name)
        {
            return Task.FromResult(_employees.Where(e => e.Name == name));
        }

        // 其他 IEmployeeRepository 和 IBaseRepository 方法的模拟实现...
        public Task<Employee> GetByIdAsync(int id) => Task.FromResult(_employees.FirstOrDefault(e => e.Id == id));
        public Task<IEnumerable<Employee>> GetAllAsync() => Task.FromResult(_employees.AsEnumerable());
        public Task AddAsync(Employee entity) { _employees.Add(entity); return Task.CompletedTask; }
        public Task UpdateAsync(Employee entity) => Task.CompletedTask;
        public Task DeleteAsync(int id) { _employees.RemoveAll(e => e.Id == id); return Task.CompletedTask; }

        // ... 其他未实现的方法，为简化示例，返回默认值
    }
}