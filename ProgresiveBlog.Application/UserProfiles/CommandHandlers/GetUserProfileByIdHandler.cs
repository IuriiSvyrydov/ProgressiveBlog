

using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.UserProfiles.Queries;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.UserProfiles.CommandHandlers
{
    public class GetUserProfileByIdHandler:IRequestHandler<GetUserProfileById,OperationResult<UserProfile>>
    {
        private readonly PostDbContext _context;

        public GetUserProfileByIdHandler(PostDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();
            var profile =  await _context.UsersProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);
            if (profile == null)
            {
                result.IsError = true;
                var error = new Error { Code = ErrorCode.NotFound, Message = $"No user Profile with ID{request.UserProfileId}found" };
                result.Errors.Add(error);
                return result;
            }
            result.Payload = profile;
            return result;
        }
    }
}
