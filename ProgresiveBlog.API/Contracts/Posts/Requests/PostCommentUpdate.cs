using System.ComponentModel.DataAnnotations;

namespace ProgresiveBlog.API.Contracts.Posts.Requests;

public class PostCommentUpdate
{
    [Required]
    public string Text { get; set; }
    
}