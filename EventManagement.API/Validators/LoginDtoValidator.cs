using FluentValidation;
using EventManagement.API.DTOs.Auth;

namespace EventManagement.API.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email adress");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");

        }
    }
}
