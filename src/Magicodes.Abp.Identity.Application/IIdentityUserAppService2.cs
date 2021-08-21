using Magicodes.Abp.Identity.Application.Dto;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Magicodes.Abp.Identity.Application
{
    public interface IIdentityUserAppService2
    {
        Task<PagedResultDto<IdentityUserListDto>> GetListAsync(GetIdentityUsersInput input);
    }
}