using System.Threading.Tasks;
using XTHR.Common.Entities;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    public interface ISocialSecurityItemRepository : IBaseRepository<SocialSecurityItem, int>
    {
        Task<SocialSecurityItem> GetByNameAsync(string name);
    }
}