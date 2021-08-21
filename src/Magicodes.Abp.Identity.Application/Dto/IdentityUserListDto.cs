using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Magicodes.Abp.Identity.Application.Dto
{
    public class IdentityUserListDto : IdentityUserDto
    {
        public string RoleNames { get; set; }
    }
}
