using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Commands
{
    public class UpdatePostText: IRequest<OperationResult<Post>>
    {
        public string NewText { get; set; }
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
