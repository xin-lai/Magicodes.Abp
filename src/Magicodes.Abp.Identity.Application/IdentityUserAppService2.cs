using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Magicodes.Abp.Identity.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Magicodes.Abp.Identity.Application
{
    public class IdentityUserAppService2 : IdentityAppServiceBase, IIdentityUserAppService2
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IOptions<IdentityOptions> IdentityOptions { get; }

        public IdentityUserAppService2(
            IdentityUserManager userManager,
            IIdentityUserRepository userRepository,
            IIdentityRoleRepository roleRepository,
            IOptions<IdentityOptions> identityOptions)
        {
            UserManager = userManager;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            IdentityOptions = identityOptions;
        }

        [Authorize(IdentityPermissions.Users.Default)]
        public async Task<PagedResultDto<IdentityUserListDto>> GetListAsync(GetIdentityUsersInput input)
        {
            var count = await UserRepository.GetCountAsync(input.Filter);
            var list = await UserRepository.GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, input.Filter, includeDetails: false);
            var outList = new List<IdentityUserListDto>(list.Count);
            list.ForEach(user =>
           {
               var dto = ObjectMapper.Map<IdentityUser, IdentityUserListDto>(user);
               var roles = UserRepository.GetRoleNamesAsync(user.Id).Result;
               dto.RoleNames = roles?.JoinAsString(",");
               outList.Add(dto);
           });
            return new PagedResultDto<IdentityUserListDto>(
                count,
                outList
            );
        }
    }
}
