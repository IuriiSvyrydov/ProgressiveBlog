using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers;

public class DeletePostInteractionHandlers: IRequestHandler<DeletePostInteractions,OperationResult<PostInteraction>>
{
    private readonly PostDbContext _context;

    public DeletePostInteractionHandlers(PostDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<PostInteraction>> Handle(DeletePostInteractions request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<PostInteraction>();
        try
        {
            var post = await _context.Posts.Include(i => i.PostInteractions)
                .FirstOrDefaultAsync(p => p.PostId == request.PostId);
            if (post is null)
            {
                result.AddError(ErrorCode.NotFound,string.Format(PostErrorMessage.PostNotFound,request.PostId));
                return result;
            }

            var postInteraction =   post.PostInteractions
                .FirstOrDefault(p => p.PostInteractionId == request.InteractionId);
            if (postInteraction is null)
            {
                result.AddError(ErrorCode.NotFound,PostErrorMessage.PostInteractionNotFound); 
                return result;
            }

            if (postInteraction.UserProfileId != request.UserProfileId)
            {
                result.AddError(ErrorCode.RemoveInteractionNotAuthorize,PostErrorMessage.InteractionremovalNotAuthorize);
                return result;
            }
            post.RemovePostInteraction(postInteraction);
            _context.Posts.Update(post);
            await _context.SaveChangesAsync(cancellationToken);
            result.Payload = postInteraction;
        }
        catch (Exception ex)
        {
            result.AddUnKnownError(ex.Message);
        }
        return result;
    }
}