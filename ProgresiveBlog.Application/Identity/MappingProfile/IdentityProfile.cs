using AutoMapper;
using ProgresiveBlog.Application.Identity.Dtos;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.Identity.MappingProfile
{
    public class IdentityProfile: Profile
    {
        public IdentityProfile()
        {
            CreateMap<UserProfile,IdentityUserProfileDto>()
                .ForMember(dest=>dest.CurrentCity,opt=>opt.MapFrom(src=>src.BasicInfo.CurrentCity))
                .ForMember(dest=>dest.DateOfBirth,opt=>opt.MapFrom(src=>src.BasicInfo.DateOfBirth))
                .ForMember(dest=>dest.EmailAddress,opt=>opt.MapFrom(src=>src.BasicInfo.EmailAddress))
                .ForMember(dest=>dest.FirstName,opt=>opt.MapFrom(src=>src.BasicInfo.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.BasicInfo.LastName))
                .ForMember(dest=>dest.PhoneNumber,opt=>opt.MapFrom(src=>src.BasicInfo.PhoneNumber))
                ;
        }
    }
}
