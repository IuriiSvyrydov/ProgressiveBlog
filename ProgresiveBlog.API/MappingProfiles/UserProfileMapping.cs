

namespace ProgresiveBlog.API.MappingProfiles
{
    public class UserProfileMapping: Profile
    {
        public UserProfileMapping()
        {
           // CreateMap<UserProfileCreateUpdate, CreateUserCommand>().ReverseMap();
            CreateMap<UserProfileCreateUpdate, UpdateUserProfile>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>().ReverseMap();
            CreateMap<UserProfile, InteractionUser>()
                .ForMember(dest => dest.FullName,
                    opt => opt
                        .MapFrom(src => src.BasicInfo.FirstName + " " + src.BasicInfo.LastName))
                .ForMember(dest=>dest.City,opt=>opt.MapFrom(src=>src.BasicInfo.CurrentCity));
        }
    }
}
