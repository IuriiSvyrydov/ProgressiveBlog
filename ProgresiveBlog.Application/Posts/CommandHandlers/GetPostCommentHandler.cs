using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Queries;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers
{
    public class GetPostCommentHandler :
        IRequestHandler<GetPostComments, OperationResult<List<PostComment>>>
    {
        private readonly PostDbContext _context;

        public GetPostCommentHandler(PostDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<List<PostComment>>> Handle(GetPostComments request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<PostComment>>();
            try
            {
                var post = await _context.Posts
                    .Include(c=>c.PostComments)

                    .FirstOrDefaultAsync(p => p.PostId == request.PostId);
                result.Payload = post.PostComments.ToList();





            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Code = ErrorCode.UnknownError,
                    Message = $"{e.Message}"
                };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}