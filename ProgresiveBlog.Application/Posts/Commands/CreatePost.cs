using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Commands
{
    public class CreatePost: IRequest<OperationResult<Post>>
    {
        public Guid UserProfileId { get; set; }
        public string Content { get; set; }
    }
}
