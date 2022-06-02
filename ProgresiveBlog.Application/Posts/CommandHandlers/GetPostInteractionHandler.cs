using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Queries;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers;

public class GetPostInteractionHandler : IRequestHandler<GetPostInteraction,OperationResult<List<PostInteraction>>>
{
    private readonly PostDbContext _context;

    public GetPostInteractionHandler(PostDbContext context)
    {
        _context = context;
    }
    public async Task<OperationResult<List<PostInteraction>>> Handle(GetPostInteraction request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<List<PostInteraction>>();
        try
        {
            
            var post = await _context.Posts.Include(pi => pi.PostInteractions)
                .ThenInclude(up=>up.UserProfile)
                .FirstOrDefaultAsync(p => p.PostId == request.PostId);
            if (post == null)
            {
                result.AddError(ErrorCode.NotFound, PostErrorMessage.PostNotFound);
                return result;
            }

            result.Payload = post.PostInteractions.ToList();
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError,e.Message);
        }

        return result;
    }
}