using FluentValidation;
using Splitify.Identity.Api.Validators.Models;

namespace Splitify.Identity.Api.Validators
{
    public class EmailValidator : AbstractValidator<EmailModel>
    {
        public EmailValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can not be empty")
                .EmailAddress().WithMessage("Email is invalid");
        }
    }
}
