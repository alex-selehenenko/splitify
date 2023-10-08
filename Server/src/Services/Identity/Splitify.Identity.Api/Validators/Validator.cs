using Microsoft.AspNetCore.Identity;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Api.Validators.Models;

namespace Splitify.Identity.Api.Validators
{
    public abstract class Validator
    {
        public static Result ValidatePassword(string password)
        {
            var validator = new PasswordValidator();
            var result = validator.Validate(new PasswordModel(password));

            if (!result.IsValid)
            {
                var error = DomainError.ValidationError(detail:
                    result?.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid email or password");

                return Result.Failure(error);
            }

            return Result.Success();
        }

        public static Result ValidateEmail(string email)
        {
            var validator = new EmailValidator();
            var result = validator.Validate(new EmailModel(email));

            if (!result.IsValid)
            {
                var error = DomainError.ValidationError(detail:
                    result?.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid email");

                return Result.Failure(error);
            }

            return Result.Success();
        }
    }
}
