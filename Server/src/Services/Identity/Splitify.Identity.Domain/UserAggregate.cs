using Resulty;
using Splitify.BuildingBlocks.Domain;
using System.Security.Cryptography;
using System.Text;

namespace Splitify.Identity.Domain
{
    public class UserAggregate : Entity
    {
        public const int ConfirmationCodeLifetimeMinutes = 10;

        public const int ResetPasswordCodeLifetimeMinutes = 10;

        public string Email { get; }

        public byte[] Password { get; private set; }

        public byte[] Salt { get; private set; }

        public bool Confirmed { get; private set; }

        public ConfirmationCode ConfirmationCode { get; private set; }

        public UserAggregate(
            string id,
            string email,
            byte[] password,
            byte[] salt,
            DateTime createdAt,
            DateTime updatedAt,
            ConfirmationCode confirmationCode,
            bool confirmed)
            : base(id, createdAt, updatedAt)
        {
            Email = email;
            Password = password;
            Salt = salt;
            Confirmed = confirmed;
            ConfirmationCode = confirmationCode;
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

        public string GetRole()
        {
            if (Confirmed)
            {
                return "user";
            }
            else
            {
                return "visitor";
            }
        }

        //public static UserProfile Create(string id, string email, string password, AuthProvider authProvider, DateTime utcNow)
        //{
        //    byte[] saltBytes = Array.Empty<byte>();
        //    byte[] passwordBytes = Array.Empty<byte>();

        //    if (!string.IsNullOrWhiteSpace(password))
        //    {
        //        saltBytes = GenerateSalt();
        //        passwordBytes = HashPassword(password, saltBytes);
        //    }

        //    var confirmationCode = GenerateConfirmationCode();
        //    return new(id, email.ToLowerInvariant(), passwordBytes, saltBytes, authProvider, utcNow, DateTime.MinValue, utcNow.AddMinutes(1), confirmationCode, false, string.Empty, DateTime.MinValue);
        //}

        
    }
}