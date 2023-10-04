using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.Identity.Domain.Events;
using Splitify.Identity.Domain.Factories;
using Splitify.Shared.Services.Misc.Implementation;

namespace Splitify.Identity.Domain
{
    public class UserAggregate : Entity, IAggregateRoot
    {
        public string Email { get; }

        public UserPassword Password { get; }

        public bool Verified { get; private set; }

        public VerificationCode VerificationCode { get; private set; }

        public UserAggregate(
            string id,
            string email,
            UserPassword password,
            DateTime createdAt,
            DateTime updatedAt,
            VerificationCode verificationCode)
            : this(id, email, password, createdAt, updatedAt, verificationCode, false)
        {
            AddDomainEvent(
                new UserCreatedDomainEvent(id, email, verificationCode.Code, createdAt));
        }

        public UserAggregate(
            string id,
            string email,
            UserPassword password,
            DateTime createdAt,
            DateTime updatedAt,
            VerificationCode verififcationCode,
            bool verified)
            : base(id, createdAt, updatedAt)
        {
            Email = email;
            Password = password;
            Verified = verified;
            VerificationCode = verififcationCode;
        }

        public UserAggregate(
            string id,
            string email,
            DateTime createdAt,
            DateTime updatedAt,
            bool verified)
            : base(id, createdAt, updatedAt)
        {
            Email = email;
            Password = default;
            Verified = verified;
            VerificationCode = default;
        }

        public Result SendConfirmationCode()
        {

            return Result.Success();
        }

        public Result ValidatePassword(string inputPassword)
        {
            return Result.Success();
        }

        public Result Confirm(string confirmationCode)
        {

            return Result.Success();
        }

        public Result ValidateResetPasswordCode(string resetPasswordCode)
        {
            return Result.Success();
        }

        public Result CompleteResetPassword(
            string resetPasswordCode,
            string password)
        {

            return Result.Success();
        }

        public Result SendResetPasswordCode()
        {

            return Result.Success();
        }

        public UserRole GetRole()
        {
            return Verified ? UserRole.Verified : UserRole.Registered;
        }        
    }
}