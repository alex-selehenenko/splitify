using MediatR;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Domain;

namespace Splitify.Identity.Application.Queries
{
    public class GetResetPasswordTokenQueryHandler
        : IRequestHandler<GetResetPasswordTokenQuery, Result>
    {
        private readonly IUserRepository _userRepository;

        public GetResetPasswordTokenQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(GetResetPasswordTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .FindByResetPasswordTokenAsync(request.Token, cancellationToken);

            if (user is null)
            {
                return Result.Failure(DomainError.ResourceNotFound());
            }

            return !user.ResetPasswordToken.IsExpired()
                ? Result.Success()
                : Result.Failure(DomainError.ValidationError(detail: "Token is invalid or expired"));
        }
    }
}
