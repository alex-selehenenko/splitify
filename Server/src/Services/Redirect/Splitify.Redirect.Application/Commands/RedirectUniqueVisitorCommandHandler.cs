using MediatR;
using Microsoft.Extensions.Logging;
using Resulty;
using Splitify.Redirect.Application.Errors;
using Splitify.Redirect.Application.Models;
using Splitify.Redirect.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectUniqueVisitorCommandHandler : IRequestHandler<RedirectUniqueVisitorCommand, Result<DestinationModel>>
    {
        private readonly IRedirectionRepository _redirectionRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<RedirectUniqueVisitorCommandHandler> _logger;

        public RedirectUniqueVisitorCommandHandler(
            IRedirectionRepository redirectionRepository,
            IDateTimeService dateTimeService,
            ILogger<RedirectUniqueVisitorCommandHandler> logger)
        {
            _redirectionRepository = redirectionRepository ?? throw new ArgumentNullException(nameof(redirectionRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<DestinationModel>> Handle(RedirectUniqueVisitorCommand request, CancellationToken cancellationToken)
        {
            var result = await FindRedirectionAsync(request.RedirectionId)
                .ThenWithTransformAsync(GetDestinationForUniqueVisitorAsync);

            if (result.IsFailure)
            {
                return Result.Failure<DestinationModel>(result.Error);
            }

            var destination = result.Value;
            return Result.Success(new DestinationModel(destination.Url, destination.Id));
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
