using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.Posts.Commands;

public class RemoveAccount: IRequest<OperationResult<bool>>
{
    public Guid IdentityUserId { get; set; }
    public Guid RequesterGuid { get; set; }
}