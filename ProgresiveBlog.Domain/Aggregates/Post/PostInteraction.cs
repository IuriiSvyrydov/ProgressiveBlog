

using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Domain.Aggregates.Post
{
    public class PostInteraction
    {
        private  PostInteraction(){}
        public Guid PostInteractionId { get; set; }
        public Guid ?UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public Guid PostId { get; set; }
        public InteractionType InteractionType { get; set; }

        public static PostInteraction CreatePostInteraction(Guid postId, Guid userProfileId,InteractionType type)
        {
            return new PostInteraction
            {
                PostId = postId,
                InteractionType = type,
                UserProfileId = userProfileId


            };
        }
    }
}
