using System.ComponentModel.DataAnnotations;

namespace ProgresiveBlog.API.Contracts.Posts.Requests
{
    public class PostUpdate
    {
        [Required]
        public string Text { get; set; }
    }
}
