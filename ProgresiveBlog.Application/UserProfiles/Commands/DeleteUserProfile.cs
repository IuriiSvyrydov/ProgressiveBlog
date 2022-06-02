using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.UserProfiles.Commands;

public class DeleteUserProfile: IRequest<OperationResult<UserProfile>>
{
    public Guid UserProfileId { get; set; }
}