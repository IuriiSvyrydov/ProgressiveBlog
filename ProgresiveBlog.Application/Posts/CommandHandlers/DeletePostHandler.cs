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
    public class DeletePostHandler: IRequestHandler<PostDelete,OperationResult<Post>>
    {
        private readonly PostDbContext _dbContext;

        public DeletePostHandler(PostDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Post>> Handle(PostDelete request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();
            try
            {

                var post = await _dbContext.Posts
                    .FirstOrDefaultAsync(up => up.PostId == request.PostId);
                if (post == null)
                {
                    result.IsError = true;
                    var error = new Error
                        {Code = ErrorCode.NotFound, Message = $"No Post with ID{request.PostId}found"};
                    result.Errors.Add(error);
                    return result;
                }

                if (post.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.DeletePostNotPossible,PostErrorMessage.DeletePostNotPossible);
                    return result;
                }
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
                result.Payload = post;
                return result;
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
            catch (Exception ex)
            {
                var error = new Error
                {
                    Code = ErrorCode.UnknownError,
                    Message = $"{ex.Message}",
                };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}
