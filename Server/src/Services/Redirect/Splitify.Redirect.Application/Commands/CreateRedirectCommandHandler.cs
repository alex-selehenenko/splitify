using MediatR;
using Resulty;
using Splitify.Redirect.Domain;
using Splitify.Redirect.Domain.Factories;
using Splitify.Shared.Services.Misc;

namespace Splitify.Redirect.Application.Commands
{
    public class CreateRedirectCommandHandler : IRequestHandler<CreateRedirectCommand, Result>
    {
        private readonly IRedirectRepository _redirectRepository;
        private readonly IDateTimeService _dateTimeService;

        public CreateRedirectCommandHandler(
            IRedirectRepository redirectRepository,
            IDateTimeService dateTimeService)
        {
            _redirectRepository = redirectRepository
                ?? throw new ArgumentNullException(nameof(redirectRepository));

            _dateTimeService = dateTimeService
                ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Result> Handle(CreateRedirectCommand request, CancellationToken cancellationToken)
        {
            var destinations = new List<Destination>();
            foreach (var destination in request.Destinations)
            {
                var creationResult = DestinationFactory.Create(destination.Id, destination.Url, _dateTimeService.UtcNow);
                if (creationResult.IsFailure)
                {
                    return Result.Failure(creationResult.Error);
                }

                destinations.Add(creationResult.Value);
            }

            var redirectCreationResult = RedirectFactory.Create(request.RedirectId, destinations, _dateTimeService.UtcNow);

            if (redirectCreationResult.IsFailure)
            {
                return Result.Failure(redirectCreationResult.Error);
            }

            _redirectRepository.Add(redirectCreationResult.Value);
            await _redirectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
