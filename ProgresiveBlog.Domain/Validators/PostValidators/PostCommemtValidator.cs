using FluentValidation;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Domain.Validators.PostValidators
{
    public class PostCommemtValidator: AbstractValidator<PostComment>
    {
        public PostCommemtValidator()
        {
            RuleFor(pc => pc.Text)
           .NotNull().WithMessage("Comment text should not be null")
           .NotEmpty().WithMessage("Comment text should not be empty")
           .MaximumLength(1000)
           .MinimumLength(1);
        }
    }
}
