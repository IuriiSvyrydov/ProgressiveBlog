using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Application.Enums;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;
using ProgresiveBlog.Domain.Exceptions.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers;

public class AddPostCommentHandler: IRequestHandler<AddPostComment,OperationResult<PostComment>>
{
    private readonly  PostDbContext _dbContext;

    public AddPostCommentHandler(PostDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OperationResult<PostComment>> Handle(AddPostComment request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<PostComment>();
        try
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId);
            if (post is null)
            {
                result.IsError = true;
                var error = new Error() {Code = ErrorCode.NotFound, Message = $"No post with ID{request.PostId} found"};
                result.Errors.Add(error);
            }

            var comment = PostComment.CreatePostComment(request.PostId, request.Text, request.UserProfileId);
            post.AddPostComment(comment); 
            _dbContext.Posts.Update(post);
             await _dbContext.SaveChangesAsync();
             
        }
        catch (PostIsNotValidException ex)
        {
            result.IsError = true;
            ex.Errors.ForEach(ex =>
            {
                var error = new Error
                {
                    Code = Enums.ErrorCode.ValidationError,
                    Message = $"{ex}"
                };
                result.Errors.Add(error);
            });
        }
        catch (Exception ex)
        {
            result.IsError = true;
            
                var error = new Error
                {
                    Code = Enums.ErrorCode.NotFound,
                    Message = $"{ex}"
                };
                result.Errors.Add(error);
        }

        return result;

    }
}