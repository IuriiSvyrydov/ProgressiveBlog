using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.Commands;

public class AddPostComment: IRequest<OperationResult<PostComment>>
{
    public Guid PostId { get; set; }
    public Guid UserProfileId { get; set; }
    public string Text { get; set; }
}