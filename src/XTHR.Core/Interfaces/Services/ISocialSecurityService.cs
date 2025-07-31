using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XTHR.Core.DTOs;

namespace XTHR.Core.Interfaces.Services
{
    public interface ISocialSecurityService
    {
        Task<IEnumerable<SocialSecurityItemDto>> GetSocialSecurityItemsAsync();
        Task<SocialSecurityItemDto> AddSocialSecurityItemAsync(SocialSecurityItemCreateDto dto);
        Task<bool> UpdateSocialSecurityItemAsync(int id, SocialSecurityItemUpdateDto dto);

        Task<IEnumerable<EmployeeSocialSecurityDto>> GetEmployeeSocialSecuritiesAsync(string departmentName, string employeeFilter);

        Task<ImportResultDto> ImportSocialSecurityAsync(Stream stream);

        Task<byte[]> GenerateImportTemplateAsync();
    }
}