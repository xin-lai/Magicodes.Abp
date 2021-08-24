using System.Collections.Generic;
using System.Text;

namespace Magicodes.Abp.Identity.Application.Dto
{
    public class IdentityUserBatchUpdateDto
    {
        public List<IdentityUserUpdateDto2> Rows { get; set; }
    }
}
