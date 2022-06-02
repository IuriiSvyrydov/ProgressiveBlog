using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Queries
{
    public class GetPostInteraction:  IRequest<OperationResult<List<PostInteraction>>>
    {
        public Guid PostId { get; set; }
    }
}
