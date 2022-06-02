using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;
using ProgresiveBlog.Domain.Exceptions.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers
{
    public class UpdatePostTextHandler: IRequestHandler<UpdatePostText,OperationResult<Post>>
    {
        private readonly PostDbContext _dbContext;

        public UpdatePostTextHandler(PostDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Post>> Handle(UpdatePostText request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();
            try
            {
                var post = await _dbContext.Posts
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId);
                if (post == null)
                {
                    result.IsError = true;
                    var error = new Error {Code = ErrorCode.NotFound, Message = $"No post with ID{request.PostId}"};
                    result.Errors.Add(error);
                    return result;
                }

                if (post.UserProfileId!=request.UserProfileId)
                {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.PostUpdateNotPossible,
                        Message = $"Post update not possible" };
                    result.Errors.Add(error);
                    return result;
                }
                post.UpdateText(request.NewText);
               await _dbContext.SaveChangesAsync();
               result.Payload = post;
              
            }
            catch (PostIsNotValidException ex)
            {
                result.IsError = true;
                ex.Errors.ForEach(er =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };
                    result.Errors.Add(error);
                });
            }
            catch(Exception ex)
            {
            
                var error = new Error
                {
                    Code = ErrorCode.PostUpdateNotPossible,
                    Message = $"{ex.Message}",
                };
                result.IsError = true;
                result.Errors.Add(error);
            }
            
            return result;
        }
    }
}
