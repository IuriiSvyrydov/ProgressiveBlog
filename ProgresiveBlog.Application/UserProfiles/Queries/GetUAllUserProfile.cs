using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.UserProfiles.Queries
{
    public class GetUAllUserProfile: IRequest<OperationResult<IEnumerable<UserProfile>>>
    {
    }
}
