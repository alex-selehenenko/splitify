using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.Identity.Domain.Events;
using Splitify.Identity.Domain.Factories;
using Splitify.Shared.Services.Misc;

namespace Splitify.Identity.Domain
{
    public class UserAggregate : Entity, IAggregateRoot
    {
        public string Email { get; }

        public UserPassword Password { get; private set; }

        public bool Verified { get; private set; }

        public VerificationCode VerificationCode { get; private set; }

        public ResetPasswordToken ResetPasswordToken { get; private set; }

        public UserAggregate(
            string id,
            string email,
            UserPassword password,
            DateTime createdAt,
            DateTime updatedAt,
            VerificationCode verificationCode,
            ResetPasswordToken resetPasswordToken)
            : this(id, email, password, createdAt, updatedAt, verificationCode, false)
        {
            ResetPasswordToken = resetPasswordToken;
            AddDomainEvent(new UserCreatedDomainEvent(id, email, verificationCode.Code, createdAt));
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

        public Result Verify(string verificationCode)
        {
            var validationResult = VerificationCode.ValidateCode(verificationCode);
            if (validationResult.IsFailure)
            {
                return validationResult;
            }

            Verified = true;

            return Result.Success();
        }

        public Result Login(string password)
        {
            return Password.ValidatePassword(password);
        }

        public Result SendResetPasswordToken(string resetBaseUrl, IDateTimeService dateTimeService)
        {
            var result = ResetPasswordTokenFactory.Create(Id, dateTimeService);
            if (result.IsSuccess)
            {
                ResetPasswordToken = result.Value;
            }

            var resetUrl = resetBaseUrl + ResetPasswordToken.Token;
            AddDomainEvent(new SendResetPasswordTokenDomainEvent(Email, resetUrl, dateTimeService.UtcNow));
            
            return result;
        }

        public Result SetNewPassword(string password, IDateTimeService dateTimeService)
        {
            var result = UserPasswordFactory.Create(password, dateTimeService);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            Password = result.Value;
            ResetPasswordToken = ResetPasswordTokenFactory.Create(Id, dateTimeService).Value;

            return Result.Success();
        }

        public UserRole GetRole()
        {
            return Verified ? UserRole.Verified : UserRole.Registered;
        }        
    }
}