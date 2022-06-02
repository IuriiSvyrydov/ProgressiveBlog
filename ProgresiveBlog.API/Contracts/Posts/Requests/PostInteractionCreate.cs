using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.API.Contracts.Posts.Requests;

public class PostInteractionCreate
{
    [Required]
    public InteractionType  Type { get; set; }

}