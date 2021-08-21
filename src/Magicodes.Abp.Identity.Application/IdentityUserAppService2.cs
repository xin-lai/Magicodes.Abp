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

        [Authorize(IdentityPermissions.Users.Update)]
        public virtual async Task BatchUpdateAsync(IdentityUserBatchUpdateDto input)
        {
            await IdentityOptions.SetAsync();

            foreach (var item in input.Rows)
            {
                var user = await UserManager.GetByIdAsync(item.Id);
                user.ConcurrencyStamp = item.ConcurrencyStamp;

                (await UserManager.SetUserNameAsync(user, item.UserName)).CheckErrors();

                await UpdateUserByInput(user, item);
                item.MapExtraPropertiesTo(user);

                (await UserManager.UpdateAsync(user)).CheckErrors();
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected virtual async Task UpdateUserByInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
        {
            if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
            }

            if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
            {
                (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
            }

            (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();

            user.Name = input.Name;
            user.Surname = input.Surname;
            (await UserManager.UpdateAsync(user)).CheckErrors();

            if (input.RoleNames != null)
            {
                (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
            }
        }
    }
}
