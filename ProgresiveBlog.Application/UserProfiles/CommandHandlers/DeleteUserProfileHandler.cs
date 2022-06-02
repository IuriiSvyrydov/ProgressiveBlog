using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.UserProfiles.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.UserProfiles.CommandHandlers;

public class DeleteUserProfileHandler: IRequestHandler<DeleteUserProfile,OperationResult<UserProfile>>
{
    private readonly PostDbContext _context;

    public DeleteUserProfileHandler(PostDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfile request, CancellationToken cancellationToken)
    {
        var result  = new OperationResult<UserProfile>();
        var userProfile = await _context.UsersProfiles
            .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);
        if (userProfile == null)
        {
            result.IsError = true;
            var error = new Error { Code = ErrorCode.NotFound, Message = $"No user Profile with ID{request.UserProfileId}found" };
            result.Errors.Add(error);
            return result;
        }

        _context.UsersProfiles.Remove(userProfile);
        await _context.SaveChangesAsync();
        result.Payload = userProfile;
        return result;
    }
}