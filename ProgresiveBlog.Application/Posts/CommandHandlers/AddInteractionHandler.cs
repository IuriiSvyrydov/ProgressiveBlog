using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers
{
    internal class AddInteractionHandler: IRequestHandler<AddInteraction, OperationResult<PostInteraction>>
    {
        private readonly PostDbContext _context;

        public AddInteractionHandler(PostDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PostInteraction>> Handle(AddInteraction request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostInteraction>();
            try
            {

                var post = await _context.Posts.Include(i => i.PostInteractions)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId);
                if (post == null)
                {
                    result.AddError(ErrorCode.NotFound,PostErrorMessage.PostNotFound);
                }

                var interaction =
                    PostInteraction.CreatePostInteraction(request.PostId, request.UserProfileId, request.Type);
                post.AddPostInteraction(interaction);
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                result.Payload = interaction;
            }
            catch (Exception e)
            {
                

            }

            return result;
        }
    }
}
