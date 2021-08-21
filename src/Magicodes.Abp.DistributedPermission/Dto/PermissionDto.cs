using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Magicodes.Abp.DistributedPermission.Dto
{
    public class PermissionDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public MultiTenancySides MultiTenancySide { get; set; }

        public ICollection<PermissionDto> Children { get; set; }

        public bool IsEnabled { get; set; }

    }
}
