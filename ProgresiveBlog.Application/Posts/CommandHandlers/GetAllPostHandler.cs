using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Queries;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers
{
    public class GetAllPostHandler : IRequestHandler<GetAllPosts, OperationResult<List<Post>>>
    {
        private readonly PostDbContext _dbContext;
        public GetAllPostHandler(PostDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OperationResult<List<Post>>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Post>>();
            try
            {
                
               var posts  = await _dbContext.Posts.ToListAsync();
                result.Payload = posts;
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Code = Enums.ErrorCode.UnknownError,
                    Message = $"{ex.Message}"
                };
                result.IsError = true;
                result.Errors.Add(error);
                //return result;
            }
           
            return result;
        }
    }
}
