using System;
using Volo.Abp.Identity;

namespace Magicodes.Abp.Identity.Application.Dto
{
    public class IdentityUserUpdateDto2: IdentityUserUpdateDto
    {
        public Guid Id { get; set; }
    }
}
