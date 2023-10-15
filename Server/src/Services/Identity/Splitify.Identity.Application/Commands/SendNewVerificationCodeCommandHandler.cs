using MediatR;
using Microsoft.Extensions.Logging;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Identity.Domain;
using Splitify.Shared.Services.Identity;
using Splitify.Shared.Services.Misc;

namespace Splitify.Identity.Application.Commands
{
    public class SendNewVerificationCodeCommandHandler
        : IRequestHandler<SendNewVerificationCodeCommand, Result>
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ILogger<SendNewVerificationCodeCommandHandler> _logger;

        public SendNewVerificationCodeCommandHandler(
            IDateTimeService dateTimeService,
            IUserRepository userRepository,
            IUserService userService,
            ILogger<SendNewVerificationCodeCommandHandler> logger)
        {
            _dateTimeService = dateTimeService;
            _userRepository = userRepository;
            _userService = userService;
            _logger = logger;
        }

        public async Task<Result> Handle(SendNewVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start sending a new verification code");
            
            var userId = _userService.GetUserId();
            var user = await _userRepository.FindAsync(userId, cancellationToken);

            if (user is null)
            {
                return Result.Failure(DomainError.ResourceNotFound());
            }

            var result = user.SendNewVerificationCode(_dateTimeService);
            
            if (result.IsSuccess)
            {
                await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            return result;
        }
    }
}
