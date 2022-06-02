using ProgresiveBlog.API.Contracts.Identity;
using ProgresiveBlog.Application.Identity.Commands;

namespace ProgresiveBlog.API.MappingProfiles
{
    public class IdentityProfile: Profile
    {
        public IdentityProfile()
        {
            CreateMap<UserRegistration, RegisterIdentity>().ReverseMap();
            CreateMap<Login, LoginCommand>();
        }
    }
}
