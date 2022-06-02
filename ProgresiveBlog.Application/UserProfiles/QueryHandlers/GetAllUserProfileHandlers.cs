using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.UserProfiles.Queries;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfileHandlers: IRequestHandler<GetUAllUserProfile,OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly PostDbContext _context;

        public GetAllUserProfileHandlers(PostDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetUAllUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<UserProfile>>();
            result.Payload =  await _context.UsersProfiles.ToArrayAsync();
            return result;
        }
    }
}
