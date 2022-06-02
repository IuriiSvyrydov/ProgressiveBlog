using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Queries
{
    public class GetAllPosts: IRequest<OperationResult<List<Post>>>
    {
    }
}
