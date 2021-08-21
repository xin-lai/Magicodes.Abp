using AutoMapper;
using Magicodes.Abp.Identity.Application.Dto;
using Volo.Abp.Identity;

namespace Magicodes.Abp.Identity.Application
{
    public class ApplicationAutoMapperProfile : Profile
    {
        public ApplicationAutoMapperProfile()
        {
            CreateMap<IdentityUser, IdentityUserListDto>();
            CreateMap<IdentityUserDto, IdentityUserListDto>();
        }
    }
}
