using FluentValidation;
using ProgresiveBlog.Domain.Aggregates.Post;
namespace ProgresiveBlog.Domain.Validators.PostValidators
{
    public class PostValidator: AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(p => p.TextContent)
          .NotNull().WithMessage("Post text content can't be null")
          .NotEmpty().WithMessage("Post text content can't be empty");
        }
    }
}
