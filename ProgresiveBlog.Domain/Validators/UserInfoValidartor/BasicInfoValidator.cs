using FluentValidation;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Domain.Validators.UserInfoValidartor
{
    public class BasicInfoValidator: AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(e=>e.FirstName)
                .NotEmpty().WithMessage("The FirstName is required");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("The lastName is required");
            RuleFor(e => e.EmailAddress).NotEmpty().EmailAddress().WithMessage("Please Enter EmailAddress");
            RuleFor(e => e.DateOfBirth).NotEmpty();

        }
    }
}
