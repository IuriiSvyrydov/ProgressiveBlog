using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Queries;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers
{
    public class GetAllPostByIdHandler : IRequestHandler<GetPostById, OperationResult<Post>>
    {
        private readonly PostDbContext _context;
        public GetAllPostByIdHandler(PostDbContext context)
        {
            _context = context;
        }
        public async Task<OperationResult<Post>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();
            var posts = await _context.Posts.FirstOrDefaultAsync(up => up.PostId == request.PostId);
            if (posts == null)
            {
                result.IsError = true;
                var error = new Error { Code = ErrorCode.NotFound, Message = $"No Posts with ID{request.PostId}found" };
                result.Errors.Add(error);
                return result;
            }
            result.Payload = posts;
            return result;
        }
    }
}
