
using ProgresiveBlog.Application.Identity.Dtos;

namespace ProgresiveBlog.API.MappingProfiles
{
    public class IdentityProfile: Profile
    {
        public IdentityProfile()
        {
            CreateMap<UserRegistration, RegisterIdentity>().ReverseMap();
            CreateMap<Login, LoginCommand>();
            CreateMap<IdentityUserProfileDto,IdentityUserProfile>().ReverseMap();
        }
    }
}
