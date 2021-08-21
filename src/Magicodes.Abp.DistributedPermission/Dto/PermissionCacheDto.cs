using System;
using System.Collections.Generic;
using System.Text;

namespace Magicodes.Abp.DistributedPermission.Dto
{
    public class PermissionCacheDto
    {
        public ICollection<PermissionGroupDto> PermissionGroups { get; set; }
    }
}
