using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Magicodes.Abp.DistributedPermission.Dto
{
    public class PermissionGroupDto : PermissionGroupDefinition
    {
        public PermissionGroupDto()
        {

        }

        public new List<PermissionDto> Permissions { get; set; }
    }
}
