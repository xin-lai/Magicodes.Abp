using System;
using System.Threading.Tasks;
using Magicodes.Abp.Identity.Application;
using Magicodes.Abp.Identity.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Volo.Abp.Identity
{
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("User")]
    [Route("api/v2/identity/users")]
    public class IdentityUserController2 : AbpController, IIdentityUserAppService2
    {
        protected IIdentityUserAppService2 UserAppService { get; }

        public IdentityUserController2(IIdentityUserAppService2 userAppService)
        {
            UserAppService = userAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<IdentityUserListDto>> GetListAsync(GetIdentityUsersInput input)
        {
            return UserAppService.GetListAsync(input);
        }

        [HttpPut("batch")]
        public virtual Task BatchUpdateAsync(IdentityUserBatchUpdateDto input)
        {
            return UserAppService.BatchUpdateAsync(input);
        }
    }
}
