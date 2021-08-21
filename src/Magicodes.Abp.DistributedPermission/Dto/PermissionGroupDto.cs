using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Magicodes.Abp.DistributedPermission.Dto
{
    public class PermissionGroupDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public MultiTenancySides MultiTenancySide { get; set; }

        public ICollection<PermissionDto> Permissions { get; set; }

    }
}
