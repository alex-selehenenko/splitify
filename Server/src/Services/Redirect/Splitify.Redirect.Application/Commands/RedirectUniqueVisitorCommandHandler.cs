using MediatR;
using Resulty;
using Splitify.Redirect.Application.Errors;
using Splitify.Redirect.Application.Models;
using Splitify.Redirect.Domain;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class RedirectUniqueVisitorCommandHandler : IRequestHandler<RedirectUniqueVisitorCommand, Result<DestinationModel>>
    {
        private readonly IRedirectRepository _redirectRepository;
        private readonly IDateTimeService _dateTimeService;

        public RedirectUniqueVisitorCommandHandler(
            IRedirectRepository redirectRepository,
            IDateTimeService dateTimeService)
        {
            _redirectRepository = redirectRepository ?? throw new ArgumentNullException(nameof(redirectRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Result<DestinationModel>> Handle(RedirectUniqueVisitorCommand request, CancellationToken cancellationToken)
        {
            var result = await FindRedirectionAsync(request.RedirectId)
                .ThenWithTransformAsync(GetDestinationForUniqueVisitorAsync);

            if (result.IsFailure)
            {
                return Result.Failure<DestinationModel>(result.Error);
            }

            var destination = result.Value;
            return Result.Success(new DestinationModel(destination.Url, destination.Id));
        }

        private async Task<Result<RedirectAggregate>> FindRedirectionAsync(string id)
        {
            var redirect = await _redirectRepository.FindAsync(id);

            return redirect is not null
                ? Result.Success(redirect)
                : Result.Failure<RedirectAggregate>(ApplicationError.ResourceNotFoundError(detail: $"Redirection doesn't exist - {id}"));
        }

        private async Task<Result<Destination>> GetDestinationForUniqueVisitorAsync(Result<RedirectAggregate> result)
        {
            var destinationResult = result.Value.GetDestinationForUniqueVisitor(_dateTimeService);

            if (destinationResult.IsFailure)
            {
                return Result.Failure<Destination>(destinationResult.Error);
            }

            await _redirectRepository.UnitOfWork.SaveChangesAsync();
            
            return destinationResult;
        }
    }
}
