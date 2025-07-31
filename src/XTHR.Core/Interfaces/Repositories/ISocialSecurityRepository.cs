using System.Threading.Tasks;
using XTHR.Common.Models;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface ISocialSecurityRepository : IBaseRepository<SocialSecurity, int>
    {
        Task<SocialSecurity> GetByEmployeeCodeAsync(string employeeCode);
    }
}