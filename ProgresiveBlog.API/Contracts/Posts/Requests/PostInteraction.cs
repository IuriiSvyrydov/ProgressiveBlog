namespace ProgresiveBlog.API.Contracts.Posts.Requests;

public class PostInteraction
{
    public Guid InteractionId { get; set; }
    public string InteractionType { get; set; }
    public InteractionUser Author { get; set; }
}