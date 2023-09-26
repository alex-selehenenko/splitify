using MediatR;
using Microsoft.Extensions.Logging;
using Resulty;
using Splitify.Redirect.Application.Errors;
using Splitify.Redirect.Application.Models;
using Splitify.Redirect.Application.Services;
using Splitify.Redirect.Domain;
using Splitify.Shared.Services.Misc;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectUniqueVisitorCommandHandler : IRequestHandler<RedirectUniqueVisitorCommand, Result<RedirectModel>>
    {
        private readonly IRedirectionRepository _redirectionRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRedirectTokenService _redirectTokenService;
        private readonly ILogger<RedirectUniqueVisitorCommandHandler> _logger;

        public RedirectUniqueVisitorCommandHandler(
            IRedirectionRepository redirectionRepository,
            IDateTimeService dateTimeService,
            IRedirectTokenService redirectTokenService,
            ILogger<RedirectUniqueVisitorCommandHandler> logger)
        {
            _redirectionRepository = redirectionRepository ?? throw new ArgumentNullException(nameof(redirectionRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _redirectTokenService = redirectTokenService ?? throw new ArgumentNullException(nameof(redirectTokenService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<RedirectModel>> Handle(RedirectUniqueVisitorCommand request, CancellationToken cancellationToken)
        {
            var result = await FindRedirectionAsync(request.RedirectionId)
                .ThenWithTransformAsync(GetDestinationForUniqueVisitorAsync);

            if (result.IsFailure)
            {
                return Result.Failure<RedirectModel>(result.Error);
            }

            var destination = result.Value;
            var token = _redirectTokenService.CreateToken(request.RedirectionId, destination.Id);

            return Result.Success(new RedirectModel(destination.Url, token));
        }

        private async Task<Result<Redirection>> FindRedirectionAsync(string id)
        {
            var redirection = await _redirectionRepository.FindAsync(id);

            return redirection is not null
                ? Result.Success(redirection)
                : Result.Failure<Redirection>(ApplicationError.ResourceNotFoundError(detail: $"Redirection doesn't exist - {id}"));
        }

        private async Task<Result<Destination>> GetDestinationForUniqueVisitorAsync(Result<Redirection> result)
        {
            var destinationResult = result.Value.GetDestinationForUniqueVisitor(_dateTimeService);

            if (destinationResult.IsFailure)
            {
                return Result.Failure<Destination>(destinationResult.Error);
            }

            await _redirectionRepository.UnitOfWork.SaveChangesAsync();
            
            return destinationResult;
        }
    }
}
