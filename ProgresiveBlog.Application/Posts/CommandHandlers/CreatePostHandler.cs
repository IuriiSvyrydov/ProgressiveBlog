using MediatR;
using ProgresiveBlog.Application.Models;
using ProgresiveBlog.Application.Posts.Commands;
using ProgresiveBlog.Dal;
using ProgresiveBlog.Domain.Aggregates.Post;
using ProgresiveBlog.Domain.Exceptions.Post;

namespace ProgresiveBlog.Application.Posts.CommandHandlers
{
    public class CreatePostHandler : IRequestHandler<CreatePost, OperationResult<Post>>
    {
        private readonly PostDbContext _dbContext;
        public CreatePostHandler(PostDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OperationResult<Post>> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();
            try
            {
                
                var post = Post.CreatePost(request.UserProfileId, request.Content);
                _dbContext.Posts.Add(post);
                await _dbContext.SaveChangesAsync();
                result.Payload = post;
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
            return result;
        }
    }
}
