using FluentValidation;
using Splitify.Identity.Api.Validators.Models;
using Splitify.Identity.Application.Commands;

namespace Splitify.Identity.Api.Validators
{
    public class PasswordValidator : AbstractValidator<PasswordModel>
    {
        public PasswordValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(p => p.Password)
                    .NotEmpty().WithMessage("Password can not be empty")
                    .MinimumLength(8).WithMessage("Password length must be at least 8.")
                    .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
                    .Matches(@"^(?=.*[!@#$%^&*()_+\[\]{}:;<>,.?~\\|-/])$").WithMessage("Password must contain at least one of these special characters.");
        }
    }
}
