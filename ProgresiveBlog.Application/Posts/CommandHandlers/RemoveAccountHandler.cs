using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Identity;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Application.Posts.CommandHandlers;

public class RemoveAccountHandler: IRequestHandler<RemoveAccount, OperationResult<bool>>
{
    private readonly PostDbContext _context;

    public RemoveAccountHandler(PostDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<bool>> Handle(RemoveAccount request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<bool>();
        try
        {
            var identityUser = await _context
                .Users.FirstOrDefaultAsync(u => u.Id == request.IdentityUserId.ToString());
            if (identityUser != null)
            {
                result.AddError(ErrorCode.IdentityUserAlreadyExists,IdentityErrorMessage.IdentityUserAlreadyExist);
                return result;
            }

            var userProfile =
                await _context.UsersProfiles.FirstOrDefaultAsync(up =>
                    up.IdentityId == request.IdentityUserId.ToString());
            if (userProfile==null)
            {
                result.AddError(ErrorCode.UnAuthorizedAccountRemoval,
                    IdentityErrorMessage.UnauthorizeedRemovalAccount);
                return result;
            }

            _context.UsersProfiles.Remove(userProfile);
            _context.Users.Remove(identityUser);
            await _context.SaveChangesAsync();
            result.Payload = true;
        }
        catch (Exception e)
        {
            result.AddUnKnownError(e.Message);
        }

        return result ;
    }
}