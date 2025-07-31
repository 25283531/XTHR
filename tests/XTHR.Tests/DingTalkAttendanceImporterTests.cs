using System.IO;
using System.Threading.Tasks;
using Xunit;
using Moq;
using XTHR.Core.Interfaces.Services;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Services;
using XTHR.Core.DTOs;
using System.Collections.Generic;
using XTHR.Core.Models;
using System.Linq;

namespace XTHR.Tests
{
    public class DingTalkAttendanceImporterTests
    {
        private readonly Mock<IExcelImporter<DingTalkAttendanceDto>> _mockExcelImporter;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
        private readonly DingTalkAttendanceImporter _importer;

        public DingTalkAttendanceImporterTests()
        {
            _mockExcelImporter = new Mock<IExcelImporter<DingTalkAttendanceDto>>();
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();
            _importer = new DingTalkAttendanceImporter(_mockExcelImporter.Object, _mockEmployeeRepo.Object);

            // Setup mock repository
            _mockEmployeeRepo.Setup(r => r.GetByEmployeeNumberAsync("001")).ReturnsAsync(new Employee { Id = 1, EmployeeNumber = "001", Name = "张三" });
            _mockEmployeeRepo.Setup(r => r.FindByNameAsync("李四")).ReturnsAsync(new List<Employee> { new Employee { Id = 2, EmployeeNumber = "002", Name = "李四" } });
        }

        [Fact]
        public async Task ImportFromFileAsync_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dtos = new List<DingTalkAttendanceDto> { new DingTalkAttendanceDto { Name = "张三", EmployeeNumber = "001" } };
            var importResult = new ExcelImportResult<DingTalkAttendanceDto>();
            importResult.Data.AddRange(dtos);

            _mockExcelImporter.Setup(i => i.ImportFromFileAsync(It.IsAny<string>(), It.IsAny<ExcelImportOptions<DingTalkAttendanceDto>>()))
                              .ReturnsAsync(importResult);

            // Act
            var result = await _importer.ImportFromFileAsync("dummy.xlsx");

            // Assert
            Assert.True(result.Success);
            Assert.Equal(1, result.SuccessCount);
        }

        [Fact]
        public async Task ImportFromStreamAsync_WithMatchingByName_ShouldUseNameRepositoryMethod()
        {
            // Arrange
            var dtos = new List<DingTalkAttendanceDto> { new DingTalkAttendanceDto { Name = "李四" } }; // No employee number
            var importResult = new ExcelImportResult<DingTalkAttendanceDto>();
            importResult.Data.AddRange(dtos);

            _mockExcelImporter.Setup(i => i.ImportFromStreamAsync(It.IsAny<Stream>(), It.IsAny<ExcelImportOptions<DingTalkAttendanceDto>>()))
                              .ReturnsAsync(importResult);

            var options = new DingTalkImportOptions { EmployeeMatchingStrategy = EmployeeMatchingStrategy.ByName };

            // Act
            await _importer.ImportFromStreamAsync(new MemoryStream(), options);

            // Assert
            _mockEmployeeRepo.Verify(r => r.FindByNameAsync("李四"), Times.Once);
            _mockEmployeeRepo.Verify(r => r.GetByEmployeeNumberAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ValidateFileAsync_WithInvalidData_ShouldReturnErrors()
        {
            // Arrange
            var dtos = new List<DingTalkAttendanceDto> { new DingTalkAttendanceDto { Name = "未知员工", EmployeeNumber = "999" } };
            var importResult = new ExcelImportResult<DingTalkAttendanceDto>();
            importResult.Data.AddRange(dtos);

            _mockExcelImporter.Setup(i => i.ValidateFileAsync(It.IsAny<string>(), It.IsAny<ExcelImportOptions<DingTalkAttendanceDto>>()))
                              .ReturnsAsync(importResult);
            _mockEmployeeRepo.Setup(r => r.GetByEmployeeNumberAsync("999")).ReturnsAsync((Employee)null);

            // Act
            var result = await _importer.ValidateFileAsync("dummy.xlsx");

            // Assert
            Assert.False(result.Success);
            Assert.True(result.HasErrors());
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("未找到工号为 '999' 的员工"));
        }

        [Fact]
        public async Task GenerateTemplateAsync_ShouldCallExcelImporter()
        {
            // Arrange
            var path = "template.xlsx";

            // Act
            await _importer.GenerateTemplateAsync(path);

            // Assert
            _mockExcelImporter.Verify(i => i.GenerateTemplateAsync(path, It.IsAny<ExcelTemplateOptions<DingTalkAttendanceDto>>()), Times.Once);
        }
    }
}