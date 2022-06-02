using ProgresiveBlog.Domain.Aggregates.Post;
using PostInteraction = ProgresiveBlog.Domain.Aggregates.Post.PostInteraction;

namespace ProgresiveBlog.API.MappingProfiles
{
    public class PostMappingProfile: Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostResponse>();
            CreateMap<PostComment, PostCommentResponse>();
            CreateMap<PostInteraction, Contracts.Posts.Requests.PostInteraction>()
                .ForMember(dest => dest.InteractionType, 
                    opt =>
                   opt.MapFrom(src => src.InteractionType.ToString()))
                .ForMember(dest=>dest.Author,
                    opt=>opt
                        .MapFrom(src=>src.UserProfile));
        }
    }
}
