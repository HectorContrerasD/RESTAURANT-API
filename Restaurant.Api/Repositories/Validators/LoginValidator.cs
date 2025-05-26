using FluentValidation;
using Restaurant.Api.Payloads;

namespace Restaurant.Api.Repositories.Validators
{
    public class LoginValidator:AbstractValidator<LoginPayload>
    {
        public LoginValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
