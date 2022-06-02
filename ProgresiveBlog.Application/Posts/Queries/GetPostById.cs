using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Queries
{
    public class GetPostById: IRequest<OperationResult<Post>>
    {
        public Guid PostId { get; set; }
    }
}
