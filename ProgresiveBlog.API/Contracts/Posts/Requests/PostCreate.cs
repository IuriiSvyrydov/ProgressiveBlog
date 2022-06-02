namespace ProgresiveBlog.API.Contracts.Posts.Requests
{
    public class PostCreate
    {
        
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
    }
}
