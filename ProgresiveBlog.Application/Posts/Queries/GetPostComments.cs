using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Queries;

public class GetPostComments: IRequest<OperationResult<List<PostComment>>>
{
    public Guid PostId { get; set; }   
}