namespace ProgresiveBlog.API.Contracts.Posts.Requests;

public class PostCommentCreate
{   [Required]
    public string Text { get; set; }

}